using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserBO
/// </summary>
public class UserBO
{
    IUserDao userDao = new UserDaoImpl();

    public UserVO ValidateUser(UserVO inuser)
    {
        UserVO user;
        try
        {
            user = userDao.ValidateUser(inuser);
        }
        catch (CustomException cex)
        {
            throw cex;
        }        
        return user;
    }

    public bool ChangePassword(string userName, string newPassword)
    {
        bool status = false;
        try
        {
            status = userDao.ChangePassword(userName, newPassword);
        }
        catch(CustomException ex)
        {
            throw ex;
        }       
        return status;
    }

    public bool ChangePassword(UserVO inuser)
    {
        bool status = false;
        try
        {
            status = userDao.ChangePassword(inuser);
        }
        catch (CustomException ex)
        {
            throw ex;
        }        
        return status;
    }

    public bool ResetPassword(string userName)
    {
        string resetkey = "";
        bool status = false;
        try
        {
            resetkey = userDao.ResetPassword(userName);
            string emailId = userDao.GetUserEmailForUserName(userName);
            if (resetkey != "")
            {
                EmailUtility.EmailResetPassword(userName, emailId, resetkey);
                status = true;
            }           
        }
        catch (CustomException ex)
        {
            throw ex;
        }      
        return status;
    }

    public string AddUser(UserVO inuser)
    {
        string status = "";
        try
        {
            status = userDao.AddUser(inuser);
        }
        catch (CustomException ex)
        {
            throw ex;
        }        
        return status;
    }

    public Boolean UpdateUser(UserVO inuser)
    {
        Boolean status = false;
        try
        {
            status = userDao.UpdateUser(inuser);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }

    public Boolean DeleteUser(int studentId)
    {
        Boolean status = false;
        try
        {
           status= userDao.DeleteUser(studentId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }
}