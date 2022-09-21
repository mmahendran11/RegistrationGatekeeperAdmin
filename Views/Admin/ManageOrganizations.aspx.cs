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
    public partial class ManageOrganizations : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbNewOrganization_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvOrganizations.Visible = false;
            lbNewOrganization.Visible = false;
            lbNewOrganization2.Visible = false;
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbOrganizationName.Text))
            {
                isValid = false;
                lblError.Text = "Organization Name can not be blank.";
            }
            if (string.IsNullOrWhiteSpace(tbOrgCode.Text))
            {
                isValid = false;
                if (string.IsNullOrWhiteSpace(lblError.Text))
                {
                    lblError.Text = "Organization Code can not be blank.";
                }
                else
                {
                    lblError.Text = lblError.Text + "<br/>Organization Code can not be blank.";
                }

                
            }
            //Insert record
            if (isValid)
            {
                Organization organizationInsert = new Organization()
                {
                    OrganizationName = tbOrganizationName.Text,
                    Active = cbActive.Checked,
                    OrganizationAbbreviation = tbOrgCode.Text,
                    //OrganizationManagerADGroup = tbADMgrGroup.Tex
                };
                _db.Organizations.Add(organizationInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvOrganizations.Visible = true;
                lbNewOrganization.Visible = true;
                lbNewOrganization2.Visible = true;
                gvOrganizations.DataBind();
            }
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvOrganizations.Visible = true;
            lbNewOrganization.Visible = true;
            lbNewOrganization2.Visible = true;
            gvOrganizations.DataBind();
        }

        protected void edsOrganizations_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvOrganizations.DataBind();
        }

        protected void edsOrganizations_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvOrganizations.DataBind();
        }
    }
}