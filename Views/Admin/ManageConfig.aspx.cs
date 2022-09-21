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
    public partial class ManageConfig : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbConfig_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvGtkepsts.Visible = false;
            lbConfig2.Visible = false;
            lbConfig.Visible = false;
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbNotes.Text))
            {
                isValid = false;
                lblError.Text = "Notes can not be blank.";
            }
            if (string.IsNullOrWhiteSpace(tbpwd.Text))
            {
                isValid = false;
                if (string.IsNullOrWhiteSpace(lblError.Text))
                {
                    lblError.Text = "Password can not be blank.";
                }
                else
                {
                    lblError.Text = lblError.Text + "<br/>Password can not be blank.";
                }
            }

                //Insert record
                if (isValid)
            {
                Config ConfigInsert = new Config()
                {

                    ConfirmTransferChoice = cbActive.Checked,
                    Password = tbpwd.Text,
                    Notes =tbNotes.Text,

                };
                _db.Configs.Add(ConfigInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvGtkepsts.Visible = true;
                lbConfig.Visible = true;
                lbConfig2.Visible = true;
                gvGtkepsts.DataBind();
            }
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvGtkepsts.Visible = true;
            lbConfig2.Visible = true;
            lbConfig.Visible = true;
            gvGtkepsts.DataBind();
        }

        protected void edsConfigs_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvGtkepsts.DataBind();
        }

        protected void edsConfigs_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvGtkepsts.DataBind();
        }
    }
}