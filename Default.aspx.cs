using RegistrationGatekeeperAdmin.Classes;
using RegistrationGatekeeperAdmin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace RegistrationGatekeeperAdmin
{
    public partial class _Default : Page
    {
        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);
        private MHCC_RegistrationEntities _db = new MHCC_RegistrationEntities();

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.Gatekeepers) || HttpContext.Current.User.IsInRole(MyRoles.ACG)))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
            
            if (!IsPostBack)
            {
                DateTime myDateTime;
                myDateTime = Convert.ToDateTime((string.Format("{0}/{1}/01", DateTime.Today.AddMonths(-1).Year.ToString(), DateTime.Today.AddMonths(-1).Month.ToString())));
                tbRegistrationDateStart.Text = myDateTime.ToString("yyyy-MM-dd");
                tbRegistrationDateEnd.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            }

        }
            public bool isGateKeeper()
        {
            bool myReturn = false;
            if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.Gatekeepers))
            {
                myReturn = true;
            }
            return myReturn;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //Need to do this as a button click and not as just a part of the page load so I can do the MassUpdateGateKeeper click option otherwise it clears the checkboxes.
            edsRegistration.DataBind();
            gvRegistration.DataBind();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        public GridView Getgrid
        {
            get
            {
                return this.gvRegistration; //ViewState needs to be enabled for gridview
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvRegistration.Rows.Count > 0)
            {
                string filename = string.Format("OneMcLarenReport-{0}.xls", DateTime.Now.ToString());
                gvRegistration.AllowPaging = false;
                gvRegistration.AllowSorting = false;
                gvRegistration.DataBind();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                // If you want the option to open the Excel file without saving than 
                // comment out the line below 
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                gvRegistration.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
                gvRegistration.AllowPaging = true;
                gvRegistration.AllowSorting = true;
                gvRegistration.DataBind();
            }
            else
            {
                string msg = "There is no data available for export.";
                System.Web.HttpContext.Current.Response.Write(string.Format("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"{0}\")</SCRIPT>", msg));
            }
        }

        public string FixString(object myString)
        {
            if (string.IsNullOrWhiteSpace((string)myString))
            {
                return string.Empty;
            }
            string myreturn = (string)myString;
            myreturn = myreturn.Replace("&nbsp;", " ");
            myreturn = myreturn.Replace("&amp;", "&");
            myreturn = myreturn.Replace("'", "`");
            myreturn = myreturn.Replace("  ", " ");
            myreturn = myreturn.Replace("&", "and");
            myreturn = myreturn.Trim();
            return myreturn;
        }

        protected void MassUpdateRow(Int32 myId)
        {
            MHCC_RegistrationEntities dbRegistration = new MHCC_RegistrationEntities();

            var updateMyRecord = from x in dbRegistration.Registrations where x.RegistrationId == myId select x;
            if (updateMyRecord.Any())
            {
                foreach (var item in updateMyRecord)
                {
                    if (Object.ReferenceEquals(item.RegistrationId, null))
                    {
                        //Should never get here
                    }
                    else
                    {
                        item.GatekeeperNote = FixString(tbGatekeeperNote.Text);
                        if (ddlGatekeeperStatus.SelectedItem.Text == "Not Assigned")
                        {
                            item.GatekeeperStatusId = null;
                        }
                        else
                        {
                            item.GatekeeperStatusId = Convert.ToInt32(ddlGatekeeperStatus.SelectedItem.Value);
                        }
                        if (ddlGatekeeperCernerPosition.SelectedItem.Text == "Not Assigned")
                        {
                            item.CernerPositionId = null;
                        }
                        else
                        {
                            item.CernerPositionId = Convert.ToInt32(ddlGatekeeperCernerPosition.SelectedItem.Value);
                        }
                        item.GatekeeperValidatedDate = DateTime.Now;
                        item.GatekeeperDisplayName = _user.LastFirstName;
                        item.GatekeeperUserId = _user.Username;
                    }

                }
                //Save needs to happen outside the foreach then it works fine. :)
                dbRegistration.SaveChanges();
            }
        }

        protected void FireRowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;

            Int32 id = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out id);

            switch (command)
            {
                case "MassAssign":
                    if (id != 0)
                    {
                        MassUpdateRow(id);
                        edsRegistration.DataBind();
                        gvRegistration.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
        protected void FilterReport(object sender, CustomExpressionEventArgs e)
        {
        }
    }
}