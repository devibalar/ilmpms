﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResetPassword : System.Web.UI.Page
{
    UserBO userBO = new UserBO();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {     
        bool status = false;
        string newPwd = txtNewPassword.Text;
        try
        {
            if (Request.QueryString["key"] != null)
            {
                UserVO user = new UserVO();
                string key = Request.QueryString["key"].ToString();
                string temp = key;
                user.UserName = key.Substring(0, 7);
                user.ResetKey = key.Substring(7);
                user.Password = newPwd;
                status = userBO.ChangePassword(user);

                if (status)
                {
                    lblUserMessage.Text = "Password has been updated successfully";
                    lblUserMessage.ForeColor = System.Drawing.Color.Green;
                    lblUserMessage.Font.Bold = true;
                }
                else
                {
                    lblUserMessage.Text = "Error in updating password. Please try again later.";
                    lblUserMessage.ForeColor = System.Drawing.Color.Red;
                    lblUserMessage.Font.Bold = true;
                }
            }
            else
            {
                lblUserMessage.Text = "URL not valid";
                lblUserMessage.ForeColor = System.Drawing.Color.Red;
                lblUserMessage.Font.Bold = true;
            }
        }
        catch (CustomException cex)
        {
            lblUserMessage.Text = cex.Message;
            lblUserMessage.ForeColor = System.Drawing.Color.Red;
            lblUserMessage.Font.Bold = true;
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            lblUserMessage.Text = ex.Message;
            lblUserMessage.ForeColor = System.Drawing.Color.Red;
            lblUserMessage.Font.Bold = true;
        }
    }
}