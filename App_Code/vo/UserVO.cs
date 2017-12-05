using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserVO
/// </summary>
public class UserVO
{

    private string userName;
    private string password;
    private string emailID;
    private string hashSalt;
    private string resetPassword;
    private string role;
    private string active;
    private int studentID;
    private int staffID;
    private bool firstLogin;
    private string name;

    public UserVO()
    {
    }
    public UserVO(string userName, string password)
    {
        this.userName = userName;
        this.password = password;
    }
    public UserVO(string emailId,int staffId, int studentId, string role)
    {
        this.emailID = emailId;
        this.staffID = staffId;
        this.studentID = studentId;
        this.role = role;
    }
    public UserVO(int studentID, string emailID, string active)
    {
        this.studentID = studentID;
        this.emailID = emailID;
        this.active = active;
    }

    public UserVO(string userName, string password, string emailID, string salt, string resetKey, string role, string active, int studentID, int staffID)
    {
        this.userName = userName;
        this.password = password;
        this.emailID = emailID;
        this.hashSalt = salt;
        this.resetPassword = resetKey;
        this.role = role;
        this.active = active;
        this.studentID = studentID;
        this.staffID = staffID;
    }
   
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public string EmailID
    {
        get { return emailID; }
        set { emailID = value; }
    }
    public string HashSalt
    {
        get { return hashSalt; }
        set { hashSalt = value; }
    }
    public string ResetKey
    {
        get { return resetPassword; }
        set { resetPassword = value; }
    }
    public string Role
    {
        get { return role; }
        set { role = value; }
    }
    public string Active
    {
        get { return active; }
        set { active = value; }
    }
    public int StudentID
    {
        get { return studentID; }
        set { studentID = value; }
    }
    public int StaffID
    {
        get { return staffID; }
        set { staffID = value; }
    }
    public bool FirstLogin
    {
        get { return firstLogin; }
        set { firstLogin = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}