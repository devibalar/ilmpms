using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IUserDao
/// </summary>
public interface IUserDao
{
    UserVO ValidateUser(UserVO inuser);
    bool DisableUser(string inuserName);
    string AddUser(UserVO inuser);
    string ResetPassword(string userName);
    bool ChangePassword(string userName, string newPassword);
    bool ChangePassword(UserVO inuser);
    int AddStudentUsers(List<UserVO> instudentUsers);
    Boolean UpdateUser(UserVO inuser);
    Boolean DeleteUser(int studentId);
    string GetUserEmailForUserName(string userName);
}