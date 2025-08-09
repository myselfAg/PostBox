using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PostBox
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void create_Click(object sender, EventArgs e)
        {
            string gender = "";
            if (male.Checked)
            {
                gender = "male";
            }
            else if (female.Checked)
            {
                gender = "female";
            }
            else if (others.Checked)
            {
                gender = "others";
            }
            else
            {
                gender = "prefer not to say";
            }
                bool t;
            Class1 c = new Class1();
            
            DataSet ds = Class1.fetch("select Username from SignUp where Username = '" + user.Text + "'");
            DataSet ds2 = Class1.fetch("select Email from SignUp where Email = '" + mail.Text + "@postbox.com" + "'");
            if (fname.Text != "" && lname.Text != "" && dob.Text != "" && mail.Text != "" && mobNo.Text != "" && user.Text != "" && pass.Text != "")
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        if (pass.Text == rePass.Text)
                        {
                            try
                            {
                                file.PostedFile.SaveAs(Server.MapPath("~/UploadedImages/") + file.FileName);
                            }
                            catch
                            {
                                passMes.Text = "Enter Photo";
                            }
                            
                            t = c.save("insert into SignUp(FirstName, LastName, DOB, Email, MobileNo, Username, Password, Gender, FileUpload) values('" + fname.Text + "','" + lname.Text + "','" + dob.Text + "','" + mail.Text + "@postbox.com" + "','" + mobNo.Text + "','" + user.Text + "','" + pass.Text + "','" + gender + "', '" + "/UploadedImages/" + file.FileName + "')");
                            if (t)
                            {
                                Response.Redirect("~/Login.aspx");
                            }
                            else
                            {
                                //Edit+++++++++++++++++++++++++++++++++++++++
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sign Up  is not Completed');", true);
                            }
                        }
                        else
                        {
                            passMes.Text = "Enter same password";
                        }
                    }
                    else
                    {
                        mail.Text = "Mail Address Already Taken";
                    }
                }
                else
                {
                    user.Text = "Username Already Taken";
                }
            }
            else
            {
                passMes.Text = "Fields Canot Be Empty";
            }
           
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }
    }
}