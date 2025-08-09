using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostBox
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login_Click(object sender, EventArgs e)
        {
            DataSet ds = Class1.fetch("select * from SignUp where Username='" + user.Text + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0][6].ToString() == pass.Text.ToString())
                {
                    Session["name"] = ds.Tables[0].Rows[0][0].ToString() + " " + ds.Tables[0].Rows[0][1].ToString();
                    Session["email"] = ds.Tables[0].Rows[0][3].ToString();
                    Session["mobNo"] = ds.Tables[0].Rows[0][4].ToString();
                    Session["file"] = ds.Tables[0].Rows[0][8].ToString();
                    Response.Redirect("~/Inbox.aspx?");
                }
                else
                {
                    //Response.Write("<script>alert('INVALID PASSWORD')</script>");
                    invalidPass.Text = "Invalid Password";
                }
            }
            else
            {
                //Response.Write("<script>alert('INVALID USER NAME')</script>");
                user.Text = "Invalid Username";

            }

        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }
    }
}