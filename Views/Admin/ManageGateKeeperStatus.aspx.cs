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
    public partial class ManageGateKeeperStatus : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbgkpsts_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvGtkepsts.Visible = false;
            lbgkpsts.Visible = false;
            lbgkpsts2.Visible = false;
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbGateKeeperStatus.Text))
            {
                isValid = false;
                lblError.Text = "Gatekeeperstatus Name can not be blank.";
            }

            //Insert record
            if (isValid)
            {
                GateKeeperStatu GateKeeperStatuInsert = new GateKeeperStatu()
                {

                    Active = cbActive.Checked,
                    GateKeeperStatusName = tbGateKeeperStatus.Text,


                };
                _db.GateKeeperStatus.Add(GateKeeperStatuInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvGtkepsts.Visible = true;
                lbgkpsts2.Visible = true;
                lbgkpsts.Visible = true;
                gvGtkepsts.DataBind();
            }
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvGtkepsts.Visible = true;
            lbgkpsts2.Visible = true;
            lbgkpsts.Visible = true;
            gvGtkepsts.DataBind();
        }

        protected void edsGateKeeperStatus_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvGtkepsts.DataBind();
        }

        protected void edsGateKeeperStatus_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvGtkepsts.DataBind();
        }
    }
}