using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OtherUtilities
/// </summary>
public class OtherUtilities
{	

    /** Purpose : To get the username from the emailid of the user by removing the domain name from it
     */
    public static string GetUserNameFromEmail(string emailId)
    {
        string userName = "";
        userName = emailId.Substring(0,emailId.IndexOf(ApplicationConstants.EmailDomain));
        return userName;
    }
}