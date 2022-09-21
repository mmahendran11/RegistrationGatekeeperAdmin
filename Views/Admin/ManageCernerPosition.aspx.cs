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
    public partial class ManageCernerPosition : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbCernerPosition_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvCernerPositions.Visible = false;
            lbCernerPosition2.Visible = false;
            lbCernerPosition.Visible = false;
        }


        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbCernerPostionName.Text))
            {
                isValid = false;
                lblError.Text = "CernerPositionName can not be blank.";
            }

            //Insert record
            if (isValid)
            {
                CernerPosition cernerPositionInsert = new CernerPosition()
                {
                    CernerPositionName = tbCernerPostionName.Text,
                    Active=cbActive.Checked,
                    

                };

                phNew.Visible = false;
                gvCernerPositions.Visible = true;
                lbCernerPosition2.Visible = true;
                lbCernerPosition.Visible = true;
                gvCernerPositions.DataBind();
            }
        }

        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvCernerPositions.Visible = true;
            lbCernerPosition.Visible = true;
            lbCernerPosition2.Visible = true;
            gvCernerPositions.DataBind();
        }

        protected void edsCernerPositions_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvCernerPositions.DataBind();
        }

        protected void edsCernerPositions_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvCernerPositions.DataBind();
        }
    }
}