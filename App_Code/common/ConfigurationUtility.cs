using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Web;

/// <summary>
/// Summary description for ConfigurationUtility
/// </summary>
public static class ConfigurationUtility
{
    public static void GetConfigurationUsingSection()
    {
        var applicationSettings = ConfigurationManager.GetSection("URLSettings") as NameValueCollection;
        if (applicationSettings.Count == 0)
        {
            throw new CustomException("URL Settings are not defined");
        }
        else
        {
            ApplicationVariables.ResetPasswordLink = applicationSettings[ApplicationConstants.URLInConfig] + "ResetPassword.aspx?Key=";           
        }
        var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        ApplicationConstants.AISEmail = smtpSection.Network.UserName;
    }
}