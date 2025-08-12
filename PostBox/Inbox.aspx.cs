using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace PostBox
{
    public partial class Inbox : System.Web.UI.Page
    {
        // +++++++++++++++++++++++++++++ Login Data And Inbox Data +++++++++++++++++++++++++++++++
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
             
            img.ImageUrl = Session["file"].ToString();
            img.Attributes.Add("style", "height: 6vh; width: 6vh; border-radius: 50%;");

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            user.Text = textInfo.ToTitleCase(Session["name"].ToString().ToLower());

            DataSet ds = Class1.fetch("select fromWhom, dateTime, subject, body from inbox where toWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
            if (ds.Tables[0].Rows.Count != 0)
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count != 0)
            {
                //mails.DataSource = ds;
                //mails.DataBind();
                mails.DataSource = ds;
                mails.DataBind();
            }
        }
        protected void mails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = mails.Rows[index];
                string mailId = mails.DataKeys[index].Value.ToString();

                // Redirect or display details using mailId
                Response.Redirect("ShowDetails.aspx?id=" + mailId);
            }
        }

        // +++++++++++++++++++++++++++++ Search Bar +++++++++++++++++++++++++++++++
        protected void searcBtn_Click(object sender, EventArgs e)
        {
            DataSet ds = Class1.fetch("select fromWhom, dateTime, subject, body  from inbox where (toWhom = '" + Session["email"] + "') and (subject = '" + search.Text + "' or fromWhom = '" + search.Text + "') order by dateTime desc ");
            DataSet sentDs = Class1.fetch("select toWhom, dateTime, subject, body  from sent where (fromWhom = '" + Session["email"] + "') and (subject = '" + search.Text + "' or toWhom = '" + search.Text + "') order by dateTime desc ");
            if (ds.Tables[0].Rows.Count != 0)
            {
                mails.DataSource = ds;
                mails.DataBind();
                sentSection.Attributes["class"] = "hide";
                hideDraftSection.Attributes["class"] = "hide";

            }
            else if (sentDs.Tables[0].Rows.Count != 0)
            {
                sentMails.DataSource = sentDs;
                sentMails.DataBind();
                sentSection.Attributes["class"] = "";
                hideDraftSection.Attributes["class"] = "hide";
            }
            else
            {
                search.Text = "No Data Found";
            }
        }

        // ++++++++++++++++++++++++++++++++ Logout Button +++++++++++++++++++++++++++++++++++++++++
        protected void logout_Click(object sender, EventArgs e)
        {
            Session["name"] = null;
            Session["email"] = null;
            Session["mobNo"] = null;
            Session["file"] = null;
            Response.Redirect("~/Index.aspx");
        }

        // ++++++++++++++++++++++++++++++++ Open Selected Inbox Mail ++++++++++++++++++++++++++++++++++++++++
        protected void mails_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideSelectedMailWindow.Attributes["class"] = "";
            fromWhom.Text = "From: " + mails.SelectedRow.Cells[1].Text;
            selectedMailDateTime.Text = mails.SelectedRow.Cells[2].Text;
            selectedMailSubjectText.Text = "Subject: " + mails.SelectedRow.Cells[3].Text;
            selectedMailBodyText.Text = mails.SelectedRow.Cells[4].Text;
        }

        // ++++++++++++++++++++++++++++++++ Open Selected Sent Mail ++++++++++++++++++++++++++++++++++++++++
        protected void sentMails_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideSelectedMailWindow.Attributes["class"] = "";
            fromWhom.Text = "To: " + sentMails.SelectedRow.Cells[1].Text;
            selectedMailDateTime.Text = sentMails.SelectedRow.Cells[2].Text;
            selectedMailSubjectText.Text = "Subject: " + sentMails.SelectedRow.Cells[3].Text;
            selectedMailBodyText.Text = sentMails.SelectedRow.Cells[4].Text;
        }

        // ++++++++++++++++++++++++++++++++ Edit Draft Mail ++++++++++++++++++++++++++++++++++++++++
        protected void draftMails_SelectedIndexChanged(object sender, EventArgs e)
        {
            composeHide.Attributes["class"] = "";

            to.Text = draftMails.SelectedRow.Cells[1].Text;
            subject.Text = draftMails.SelectedRow.Cells[3].Text;
            body.Text = draftMails.SelectedRow.Cells[4].Text;
            
        }

        // +++++++++++++++++++++++++++++ Selected Mail Window Close Button +++++++++++++++++++++++++++++++
        protected void closeSelectedMail_Click1(object sender, EventArgs e)
        {
            hideSelectedMailWindow.Attributes["class"] = "hide";
        }

        // +++++++++++++++++++++++++++++ Selected Mail Window Close Button +++++++++++++++++++++++++++++++
        protected void delete_Click(object sender, EventArgs e)
        {
            if(sentSection.Attributes["class"] == "hide")
            {
                DataSet ds = Class1.fetch("select id from inbox where (toWhom = '" + Session["email"] + "' and fromWhom = '" + mails.SelectedRow.Cells[1].Text + "') and (subject = '" + mails.SelectedRow.Cells[3].Text + "' and body = '" + mails.SelectedRow.Cells[4].Text + "')");
                Class1 c = new Class1();
                bool t;
                t = c.save("delete from inbox where id = '" + ds.Tables[0].Rows[0][0] + "'");
                if (t)
                {
                    hideSelectedMailWindow.Attributes["class"] = "hide";
                    DataSet getInboxData = Class1.fetch("select fromWhom, dateTime, subject, body from inbox where toWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
                    if (getInboxData.Tables[0].Rows.Count > 0)
                    {
                        mails.DataSource = getInboxData;
                        mails.DataBind();
                    }
                }
            }
            else
            {
                DataSet ds = Class1.fetch("select id from sent where (toWhom = '" + sentMails.SelectedRow.Cells[1].Text + "' and fromWhom = '" + Session["email"] + "') and (subject = '" + sentMails.SelectedRow.Cells[3].Text + "' and body = '" + sentMails.SelectedRow.Cells[4].Text + "')");
                Class1 c = new Class1();
                bool t;
                t = c.save("delete from sent where id = '" + ds.Tables[0].Rows[0][0] + "'");
                if (t)
                {
                    hideSelectedMailWindow.Attributes["class"] = "hide";
                    DataSet getSentData = Class1.fetch("select toWhom, dateTime, subject, body from sent where fromWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
                    if (getSentData.Tables[0].Rows.Count > 0)
                    {
                        sentMails.DataSource = getSentData;
                        sentMails.DataBind();
                    }
                }
            }
        }


        // +++++++++++++++++++++++++++++ Compose Window Open Button +++++++++++++++++++++++++++++++
        protected void compose_Click(object sender, EventArgs e)
        {
            composeHide.Attributes["class"] = "";
        }

        // +++++++++++++++++++++++++++++ Compose Window Back Button +++++++++++++++++++++++++++++++
        protected void back_Click(object sender, EventArgs e)
        {
            to.Text = "";
            subject.Text = "";
            body.Text = "";
            composeHide.Attributes["class"] = "hide";
        }

        // +++++++++++++++++++++++++++++ Compose Window Draft Button +++++++++++++++++++++++++++++++
        protected void clear_Click1(object sender, EventArgs e)
        {
            
            bool t;
            Class1 c = new Class1();
            DataSet maxIdSet = Class1.fetch("select max(id) from drafts");
            int id;
            if (maxIdSet != null &&
                maxIdSet.Tables.Count > 0 &&
                maxIdSet.Tables[0].Rows.Count > 0 &&
                maxIdSet.Tables[0].Rows[0][0] != DBNull.Value)
            {
                id = Convert.ToInt32(maxIdSet.Tables[0].Rows[0][0]) + 1;
            }
            else
            {
                id = 1;
            }

            if (to.Text != "" && (subject.Text != "" || body.Text != ""))
            {
                if (subject.Text == "")
                {
                    subject.Text = "[Empty]";
                }
                if (body.Text == "")
                {
                    body.Text = "[Empty]";
                }
                DateTime now = DateTime.Now;
                string fullDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                t = c.save("insert into drafts(id, toWhom, fromWhom, subject, body, dateTime) values('" + id + "', '" + to.Text + "','" + Session["email"].ToString() + "', '" + subject.Text + "', '" + body.Text + "', '" + fullDateTime + "')");
                if (t)
                {
                    to.Text = "";
                    subject.Text = "";
                    body.Text = "";
                    DataSet draftComposeUpdate = Class1.fetch("select toWhom, dateTime, subject, body from drafts where fromWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
                    if (draftComposeUpdate.Tables[0].Rows.Count != 0)
                    {
                        draftMails.DataSource = draftComposeUpdate;
                        draftMails.DataBind();
                    }
                }
            }
            else
            {
                to.Text = "";
                subject.Text = "";
                body.Text = "";
            }
        }

        // +++++++++++++++++++++++++++++ Compose Window Send Button +++++++++++++++++++++++++++++++
        protected void send_Click(object sender, EventArgs e)
        {
            bool t;
            Class1 c = new Class1();
            DataSet maxIdSet = Class1.fetch("select max(id) from inbox");
            DataSet maxIdSetSent = Class1.fetch("select max(id) from sent");
            int id;
            int idSent;

            if (maxIdSet != null &&
                maxIdSet.Tables.Count > 0 &&
                maxIdSet.Tables[0].Rows.Count > 0 &&
                maxIdSet.Tables[0].Rows[0][0] != DBNull.Value)
            {
                id = Convert.ToInt32(maxIdSet.Tables[0].Rows[0][0]) + 1;
            }
            else
            {
                id = 1;
            }

            if (maxIdSetSent != null &&
                maxIdSetSent.Tables.Count > 0 &&
                maxIdSetSent.Tables[0].Rows.Count > 0 &&
                maxIdSetSent.Tables[0].Rows[0][0] != DBNull.Value)
            {
                idSent = Convert.ToInt32(maxIdSetSent.Tables[0].Rows[0][0]) + 1;
            }
            else
            {
                idSent = 1;
            }

            DataSet ds = Class1.fetch("select email from SignUp where email='" + to.Text + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DateTime now = DateTime.Now;
                string fullDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");

                t = c.save("insert into inbox(id, toWhom, fromWhom, subject, body, dateTime) values('" + id + "', '" + to.Text + "','" + Session["email"].ToString() + "', '" + subject.Text + "', '" + body.Text + "', '" + fullDateTime + "')");
                t = c.save("insert into sent(id, fromWhom, toWhom, subject, body, dateTime) values('" + idSent + "', '" + Session["email"].ToString() + "', '" + to.Text + "', '" + subject.Text + "', '" + body.Text + "', '" + fullDateTime + "')");
                if (t)
                {
                    to.Text = "";
                    subject.Text = "";
                    body.Text = "";
                    DataSet inboxComposeUpdate = Class1.fetch("select fromWhom, dateTime, subject, body from inbox where toWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
                    if (inboxComposeUpdate.Tables[0].Rows.Count != 0)
                    {
                        mails.DataSource = inboxComposeUpdate;
                        mails.DataBind();
                    }
                    DataSet sentComposeUpdate = Class1.fetch("select toWhom, dateTime, subject, body from sent where fromWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
                    if (sentComposeUpdate.Tables[0].Rows.Count != 0)
                    {
                        sentMails.DataSource = sentComposeUpdate;
                        sentMails.DataBind();
                    }

                }

                composeHide.Attributes["class"] = "hide";
            }
            else
            {
                to.Text = "Invalid Mail Address";
            }
        }
        
        // +++++++++++++++++++++++++++++ Sent Mail Section +++++++++++++++++++++++++++++++
        protected void sent_Click(object sender, EventArgs e)
        {
            sentSection.Attributes["class"] = "";
            composeHide.Attributes["class"] = "hide";
            hideSelectedMailWindow.Attributes["class"] = "hide";
            hideSettingsWindow.Attributes["class"] = "hide";
            hideDraftSection.Attributes["class"] = "hide";
            DataSet sentDs = Class1.fetch("select toWhom, dateTime, subject, body from sent where fromWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
            if (sentDs.Tables[0].Rows.Count != 0)
            {
                sentMails.DataSource = sentDs;
                sentMails.DataBind();
            }
            
        }

        // +++++++++++++++++++++++++++++ Draft Mail Section +++++++++++++++++++++++++++++++
        protected void draft_Click(object sender, EventArgs e)
        {
            hideDraftSection.Attributes["class"] = "";
            composeHide.Attributes["class"] = "hide";
            hideSelectedMailWindow.Attributes["class"] = "hide";
            hideSettingsWindow.Attributes["class"] = "hide";
            DataSet draftsDs = Class1.fetch("select toWhom, dateTime, subject, body from drafts where fromWhom = '" + Session["email"].ToString() + "' order by dateTime desc");
            if (draftsDs.Tables[0].Rows.Count != 0)
            {
                draftMails.DataSource = draftsDs;
                draftMails.DataBind();
            }
        }

        // +++++++++++++++++++++++++++++ Back To Inbox Button +++++++++++++++++++++++++++++++
        protected void inboBt_Click(object sender, EventArgs e)
        {
            composeHide.Attributes["class"] = "hide";
            hideSelectedMailWindow.Attributes["class"] = "hide";
            sentSection.Attributes["class"] = "hide";
            hideDraftSection.Attributes["class"] = "hide";
            hideSettingsWindow.Attributes["class"] = "hide";
        }

        // +++++++++++++++++++++++++++++ Setting Window Button +++++++++++++++++++++++++++++++
        protected void settings_Click(object sender, EventArgs e)
        {
            hideSettingsWindow.Attributes["class"] = "";
        }

        // +++++++++++++++++++++++++++++ Setting Window Back Button +++++++++++++++++++++++++++++++
        protected void setBack_Click(object sender, EventArgs e)
        {
            hideSettingsWindow.Attributes["class"] = "hide";
        }

        // +++++++++++++++++++++++++++++ Setting Window Save Button +++++++++++++++++++++++++++++++
        protected void setSave_Click(object sender, EventArgs e)
        {
            bool t;
            Class1 c = new Class1();
            DataSet ds = Class1.fetch("select Username, Password from SignUp where Username = '" + userSet.Text + "'");
            if(ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0][1].ToString() == oldPass.Text)
                {
                    if (newPass.Text == reNewPass.Text)
                    {
                        t = c.save("update SignUp set Password = '" + newPass.Text + "' where Username = '" + userSet.Text + "'");
                        if (t)
                        {
                            hideSettingsWindow.Attributes["class"] = "hide";
                            Session["name"] = null;
                            Session["email"] = null;
                            Session["mobNo"] = null;
                            Session["file"] = null;
                            Response.Redirect("~/Login.aspx");
                        }
                        else
                        {
                            newPass.Text = "Password Has Not Changed";
                        }
                    }
                    else
                    {
                        reNewPass.Text = "Confirm Password Should Be Same With New Password";
                    }
                }
                else
                {
                    oldPass.Text = "Incorrect Password";
                }
            }
            else
            {
                userSet.Text = "Username Incorrect";
            }
        }

        protected void img_Click(object sender, ImageClickEventArgs e)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            
            userInfo.Attributes["class"] = "";

            username.Text = "Username - " + textInfo.ToTitleCase(Session["username"].ToString().ToLower());
            name.Text = "Name - " + textInfo.ToTitleCase(Session["name"].ToString().ToLower());
            dob.Text = "Date of Birth - " + textInfo.ToTitleCase(Session["dob"].ToString().ToLower());
            email.Text = "Email - " + Session["email"].ToString();
            mobNo.Text = "Mobile No. - " + textInfo.ToTitleCase(Session["mobNo"].ToString().ToLower());
            gender.Text = "Gender - " + textInfo.ToTitleCase(Session["gender"].ToString().ToLower());
        }

        protected void userInfoBack_Click(object sender, EventArgs e)
        {
            userInfo.Attributes["class"] = "hide";
        }
    }
}