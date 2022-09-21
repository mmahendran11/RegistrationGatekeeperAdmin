using RegistrationGatekeeperAdmin.Entities;
using RegistrationGatekeeperAdmin.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistrationGatekeeperAdmin.Views.Admin
{
    public partial class Credentials : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbCredential_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvCredential.Visible = false;
            lbCredential2.Visible = false;
            lbCredential.Visible = false;
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbCredentialName.Text))
            {
                isValid = false;
                lblError.Text = "Credential Name can not be blank.";
            }
           
            //Insert record
            if (isValid)
            {
                Credential CredentialInsert = new Credential()
                {
                    CredentialName = tbCredentialName.Text,
                    Active = cbActive.Checked,
                   
                };
                _db.Credentials.Add(CredentialInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvCredential.Visible = true;
                lbCredential2.Visible = true;
                lbCredential.Visible = true;
                gvCredential.DataBind();
            }
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvCredential.Visible = true;
            lbCredential.Visible = true;
            lbCredential2.Visible = true;
            gvCredential.DataBind();
        }

        protected void edsCredentials_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvCredential.DataBind();
        }

        protected void edsCredentials_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvCredential.DataBind();
        }
    }
}