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
    public partial class ManageNursingAncillaryCredential : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbNursing_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvNursingAncillaryCredentials.Visible = false;
            lbNursing.Visible = false;
            lbNursing2.Visible = false;
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbCertificationCode.Text))
            {
                isValid = false;
                lblError.Text = "Certification Code can not be blank.";
            }
            if (string.IsNullOrWhiteSpace(tbCertification.Text))
            {
                isValid = false;
                if (string.IsNullOrWhiteSpace(lblError.Text))
                {
                    lblError.Text = "Certification Name can not be blank.";
                }
                else
                {
                    lblError.Text = lblError.Text + "<br/>Certification Name can not be blank.";
                }


            }
            //Insert record
            if (isValid)
            {
                NursingAncillaryCredential NursingAncillaryCredentialInsert = new NursingAncillaryCredential()
                {
                    Certification = tbCertification.Text,
                    CertificationCode = tbCertificationCode.Text,
                    Active= cbActive.Checked,



                };
                _db.NursingAncillaryCredentials.Add(NursingAncillaryCredentialInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvNursingAncillaryCredentials.Visible = true;
                lbNursing.Visible = true;
                lbNursing2.Visible = true;
                gvNursingAncillaryCredentials.DataBind();
            }
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvNursingAncillaryCredentials.Visible = true;
            lbNursing.Visible = true;
            lbNursing2.Visible = true;
            gvNursingAncillaryCredentials.DataBind();
        }

        protected void edsNursingAncillaryCredentials_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvNursingAncillaryCredentials.DataBind();
        }

        protected void edsNursingAncillaryCredentials_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvNursingAncillaryCredentials.DataBind();
        }
    }
}