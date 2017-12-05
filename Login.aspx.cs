using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    UserBO userBO = new UserBO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            Session["prevUrl"] = Request.Url.AbsoluteUri;
        }        
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {       
        try
        {
            //clear user message
            lblUserMessage.Text = "";
            // get the input entered by user
            string userName = txtUserName.Text;
            string userpwd = txtPassword.Text;

            UserVO inuserVO = new UserVO(userName, userpwd);
            UserVO userVO = userBO.ValidateUser(inuserVO);
            if (userVO == null)
            {
                lblUserMessage.Text = "Login Failed. Please enter valid email and password";
                lblUserMessage.ForeColor = System.Drawing.Color.Red;
                lblUserMessage.Font.Bold = true;
            }
            else
            {
                Session["UserName"] = userVO.UserName;
                Session["Role"] = userVO.Role;
                Session["Name"] = userVO.Name;
                Session["StudentID"] = userVO.StudentID;
                if (userVO.FirstLogin)
                {
                    // redirect to change password
                    Response.Redirect("ChangePassword.aspx", false);
                }
                else
                {
                    if (userVO.Role == ApplicationConstants.StudentRole)
                    {                        
                        Response.Redirect("StudentHome.aspx", false);
                    }
                    else
                    {
                        Session["StaffID"] = userVO.StaffID;
                        Response.Redirect("StaffHome.aspx", false);
                    }
                }
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
            lblUserMessage.Text = ex.Message;
            lblUserMessage.ForeColor = System.Drawing.Color.Red;
            lblUserMessage.Font.Bold = true;
            ExceptionUtility.LogException(ex, "Error Page");
        }
    }
}