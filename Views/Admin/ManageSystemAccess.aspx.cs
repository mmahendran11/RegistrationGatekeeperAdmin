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
    public partial class ManageSystemAccess : System.Web.UI.Page
    {
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }
        protected void lbsysaccess_Click(object sender, EventArgs e)
        {
            phNew.Visible = true;
            gvSysaccess.Visible = false;
            lbsysaccess.Visible = false;
            lbsysaccess2.Visible = false;
        }

        protected void lbInsert_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(tbSysaccessname.Text))
            {
                isValid = false;
                lblError.Text = "Systemaccess Name can not be blank.";
            }

            //Insert record
            if (isValid)
            {
                SystemAccess SystemAccessInsert = new SystemAccess()
                {
           
                    Active = cbActive.Checked,
                    SystemAccessName=tbSysaccessname.Text,


                };
                _db.SystemAccesses.Add(SystemAccessInsert);
                _db.SaveChanges();

                phNew.Visible = false;
                gvSysaccess.Visible = true;
                lbsysaccess2.Visible = true;
                lbsysaccess.Visible = true;
                gvSysaccess.DataBind();
            }
        }


        protected void lbClose_Click(object sender, EventArgs e)
        {
            phNew.Visible = false;
            gvSysaccess.Visible = true;
            lbsysaccess.Visible = true;
            lbsysaccess2.Visible = true;
            gvSysaccess.DataBind();
        }

        protected void edsSystemAccesses_Updated(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvSysaccess.DataBind();
        }

        protected void edsSystemAccesses_Inserted(object sender, EntityDataSourceChangedEventArgs e)
        {
            gvSysaccess.DataBind();
        }
    }
}