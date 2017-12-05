using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using common;

/// <summary>
/// Summary description for UserDaoImpl
/// </summary>
public class UserDaoImpl : IUserDao
{
    // Validate user with the given username and password
    public UserVO ValidateUser(UserVO inuser)
    {
        UserVO user = new UserVO();
        try
        {            
            string salt = GetSaltForUserName(inuser.UserName);
            DBConnection.conn.Open();
            if(salt=="")
            {
                throw new CustomException("Username or password is invalid");
            }
            else
            {
                string hashPassword = PasswordGenerator.GenerateHash(inuser.Password + salt);
                // the stored procedure
                SqlCommand cmd1 = new SqlCommand("spValidateUser", DBConnection.conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@UserName", inuser.UserName));
                cmd1.Parameters.Add(new SqlParameter("@Password", hashPassword));
                // execute the command
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        user.Name = reader1["Name"].ToString();
                        user.UserName = reader1["UserName"].ToString();
                        user.Role = reader1["Role"].ToString();
                        user.StudentID = int.Parse(reader1["StudentId"].ToString());
                        user.StaffID = int.Parse(reader1["StaffId"].ToString());
                        if (reader1["FirstLogin"].ToString() == "True")
                        {
                            user.FirstLogin = true;
                        }
                        else
                        {
                            user.FirstLogin = false;
                        }
                    }
                }
                else
                {
                    throw new CustomException("Username or password is invalid");
                }
            }           
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }       
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return user;
    }
    //Inactivate user 
    public bool DisableUser(string inuserName)
    {
        bool status = false;
        try
        {
            DBConnection.conn.Open();
            string query = "UPDATE dbo.IlmpUser SET Active='No' WHERE inuserName=@UserName AND Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@UserName", inuserName);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;  
    }
    // Add user details into database
    public string AddUser(UserVO inuser)
    {
        string status = "";
        try
        {
            DBConnection.conn.Open();
            inuser.UserName = OtherUtilities.GetUserNameFromEmail(inuser.EmailID);
            string salt = PasswordGenerator.GenerateSalt();
            string hashPassword = PasswordGenerator.GenerateHash(inuser.UserName + inuser.StudentID + salt);
            inuser.Active = ApplicationConstants.Active;
            if (inuser.Role == ApplicationConstants.StaffRole)
            {
                inuser.StudentID = 0;
            }
            else if (inuser.Role == ApplicationConstants.StudentRole)
            {
                inuser.StaffID = 0;
            }
            inuser.ResetKey = "";
            inuser.Password = hashPassword;
            inuser.HashSalt = salt;           
            //inuser.FirstLogin = true;
            string query = "INSERT INTO dbo.IlmpUser (UserName,StudentId,StaffId,Password,EmailId,HashSalt,ResetPassword,FirstLogin,Active, Role) "
                            + " VALUES (@UserName,@StudentId,@StaffId,@Password,@EmailId,@HashSalt,@ResetPassword, @FirstLogin, @Active,@Role) ";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@UserName", inuser.UserName);
            cmd.Parameters.AddWithValue("@StudentId", inuser.StudentID);
            cmd.Parameters.AddWithValue("@StaffId", inuser.StaffID);
            cmd.Parameters.AddWithValue("@EmailId", inuser.EmailID);
            cmd.Parameters.AddWithValue("@Password", hashPassword);
            cmd.Parameters.AddWithValue("@HashSalt", inuser.HashSalt);
            cmd.Parameters.AddWithValue("@ResetPassword", inuser.ResetKey);
            cmd.Parameters.AddWithValue("@FirstLogin", 1);
            cmd.Parameters.AddWithValue("@Active", inuser.Active);
            cmd.Parameters.AddWithValue("@Role", inuser.Role);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = inuser.UserName+" has been added successfully";
            }
            else
            {
                status = "Error in addition";
            }
        }
        catch (SqlException ex)
        {            
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }
    //Reset password for the given user
    public string ResetPassword(string userName)
    {
        string resetKey = "";
        try
        {
            Random r = new Random();
            int num = r.Next(10, 20);           
            resetKey = PasswordGenerator.RandomString(num);
            DBConnection.conn.Open();
            string query = "UPDATE dbo.IlmpUser SET ResetPassword=@ResetKey WHERE UserName=@UserName AND Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@ResetKey", resetKey);
            int result = cmd.ExecuteNonQuery();
            if (result < 0)            
            {
                resetKey = "";
                throw new CustomException("Invalid Username");
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }        
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return resetKey;
    }
    // Update the given password
    public bool ChangePassword(string userName, string newPassword)
    {
        bool status = false;
        try
        {
            string salt = GetSaltForUserName(userName);
            if (salt == "")
            {
                throw new CustomException("User is invalid");
            }
            else
            {
                DBConnection.conn.Open();
                string hashPassword = PasswordGenerator.GenerateHash(newPassword + salt);
                string query = "UPDATE dbo.IlmpUser SET Password=@Password, FirstLogin=0 WHERE UserName=@UserName and Active='Yes'";
                SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", hashPassword);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    status = true;
                }               
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }

    public bool ChangePassword(UserVO inuser)
    {
        bool status = false;
        try
        {
            string salt = GetSaltForUserName(inuser.UserName);
            if (salt == "")
            {
                throw new CustomException("User is invalid");
            }
            else
            {
                DBConnection.conn.Open();
                string hashPassword = PasswordGenerator.GenerateHash(inuser.Password + salt);
                string query = "UPDATE dbo.IlmpUser SET Password=@Password, ResetPassword='' WHERE UserName=@UserName AND ResetPassword=@ResetKey AND Active='Yes'";
                SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@UserName", inuser.UserName);
                cmd.Parameters.AddWithValue("@Password", hashPassword);
                cmd.Parameters.AddWithValue("@ResetKey", inuser.ResetKey);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    status = true;
                }
                else
                {
                    throw new CustomException(" Incorrect url. Please copy the correct url from your email ");
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }       
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }


    /**Purpose: to insert user table for student users when it is added using csv file. 
     * StudentId and Email will be given in the input UserVO list. 
     * Password will be a combination of username and studentid. Salt will be added to it and then hashed
     * User will be inActive till they change password during first login.
     */ 
    public int AddStudentUsers(List<UserVO> instudentUsers)
    {
        int successCount = 0;
        try
        {
            DBConnection.conn.Open();
            foreach (UserVO user in instudentUsers)
            {
                user.UserName = OtherUtilities.GetUserNameFromEmail(user.EmailID);
                string salt = PasswordGenerator.GenerateSalt();
                string hashPassword = PasswordGenerator.GenerateHash(user.UserName+ user.StudentID + salt);
                user.Active = ApplicationConstants.Active;
                user.Role = ApplicationConstants.StudentRole;
                user.ResetKey = " ";
                user.Password = hashPassword;
                user.HashSalt = salt;
                user.StaffID = 0;
                user.FirstLogin = true;
            }
            InsertDataUsingSqlBulkCopy(instudentUsers,DBConnection.conn);            
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw e;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return successCount;

    }
    //Add list of users into table
    private static void InsertDataUsingSqlBulkCopy(IEnumerable<UserVO> studentUsers, SqlConnection connection)
    {
        var bulkCopy = new SqlBulkCopy(connection);
        bulkCopy.DestinationTableName = "IlmpUser";
        bulkCopy.ColumnMappings.Add("UserName", "UserName");
        bulkCopy.ColumnMappings.Add("StudentId", "StudentId");
        bulkCopy.ColumnMappings.Add("StaffId", "StaffId");
        bulkCopy.ColumnMappings.Add("Password", "Password");
        bulkCopy.ColumnMappings.Add("EmailId", "EmailId");
        bulkCopy.ColumnMappings.Add("HashSalt", "HashSalt");
        bulkCopy.ColumnMappings.Add("FirstLogin", "FirstLogin");
        bulkCopy.ColumnMappings.Add("Active", "Active");
        bulkCopy.ColumnMappings.Add("Role", "Role");
        using (var dataReader = new ObjectDataReader<UserVO>(studentUsers))
        {
            bulkCopy.WriteToServer(dataReader); 
        }
    }
    // Get salt for the given username
    private string GetSaltForUserName(string userName)
    {
        string salt = "";
        try
        {
            DBConnection.conn.Open();            
            string query = "SELECT HashSalt from dbo.IlmpUser where LOWER(userName) = LOWER(@UserName) AND Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    salt = reader["HashSalt"].ToString();
                }
                reader.Close();
            }
            else
            {
                throw new CustomException("User not valid or active");
            }
            
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }        
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return salt;
    }
    // update user details
    public Boolean UpdateUser(UserVO inuser)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "UPDATE dbo.IlmpUser SET EmailID=@Email, Active=@Active WHERE StudentID=@StudentId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", inuser.StudentID);
            cmd.Parameters.AddWithValue("@Email", inuser.EmailID);
            cmd.Parameters.AddWithValue("@Active", inuser.Active);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }
    // delete user
    public Boolean DeleteUser(int studentId)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "DELETE FROM dbo.IlmpUser WHERE StudentID=@StudentId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.ExecuteNonQuery();
            int result = cmd.ExecuteNonQuery();
            if (result>0)
                status = true;
            
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }
    // Get email for the given user
    public string GetUserEmailForUserName(string userName)
    {
        string emailId = "";
        try
        {
            DBConnection.conn.Open();
            String query = "select EmailId from IlmpUser where UserName=@UserName";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    emailId = reader["EmailId"].ToString();
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }        
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return emailId;
    }
    
}