using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.Net.Configuration;

/// <summary>
/// Summary description for Email
/// </summary>
public class EmailUtility
{    
    public static void EmailResetPassword(string username,string emailId, string resetKey)
    {
        try
        {            
            ConfigurationUtility.GetConfigurationUsingSection();
            MailMessage message = new MailMessage(ApplicationConstants.AISEmail, emailId);
            StringBuilder body = new StringBuilder();
            body.Append(" <p> Did you forget your password? No problem! Please click the link below to reset your password. </p><br/> ");
            body.Append("<p><a href=\"" + ApplicationVariables.ResetPasswordLink + username + resetKey + "\">Please click here to reset your password</a>" + " </p>")
                .Append("<p>If you cannot access this link, copy and paste the entire URL into your browser.</p><br/><br/><br/>")                
                .Append("Regards,<br/>")
                .Append("Auckland Institute of Studies");
            message.Body = body.ToString();
            message.Subject = "AIS - ILMPMS| Reset Password";
            message.IsBodyHtml = true;
            SmtpClient smtpclient = new SmtpClient();
            smtpclient.Send(message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error logger");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
    }  
}