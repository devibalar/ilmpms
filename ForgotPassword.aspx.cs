using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    UserBO userBO = new UserBO();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnGetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = txtUserName.Text;
            bool status = userBO.ResetPassword(userName);
            if (status)
            {
                lblUserMessage.Text = "You have received a link to reset password in your email. Please check your mail";
                lblUserMessage.Font.Bold = true;
                lblUserMessage.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (CustomException cex)
        {
            lblUserMessage.Text = cex.Message;
            lblUserMessage.Font.Bold = true;
            lblUserMessage.ForeColor = System.Drawing.Color.Red;
        }
        catch (Exception ex)
        {
            lblUserMessage.Text = ex.Message;
            lblUserMessage.Font.Bold = true;
            lblUserMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
}