using RegistrationGatekeeperAdmin.Classes;
using RegistrationGatekeeperAdmin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                int? myGatekeeperStatusId = null;
                int? myCernerPositionId = null;
                DateTime UpdateDateTime = DateTime.Now;

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
                            item.GatekeeperStatusName = null;
                            myGatekeeperStatusId = null;
                        }
                        else
                        {
                            item.GatekeeperStatusId = Convert.ToInt32(ddlGatekeeperStatus.SelectedItem.Value);
                            item.GatekeeperStatusName = ddlGatekeeperStatus.SelectedItem.Text;
                            myGatekeeperStatusId = Convert.ToInt32(ddlGatekeeperStatus.SelectedItem.Value);
                        }
                        if (ddlGatekeeperCernerPosition.SelectedItem.Text == "Not Assigned")
                        {
                            item.CernerPositionId = null;
                            item.CernerPositionName = null;
                            myCernerPositionId = null;
                        }
                        else
                        {
                            item.CernerPositionId = Convert.ToInt32(ddlGatekeeperCernerPosition.SelectedItem.Value);
                            item.CernerPositionName = ddlGatekeeperCernerPosition.SelectedItem.Text;
                            myCernerPositionId = Convert.ToInt32(ddlGatekeeperCernerPosition.SelectedItem.Value);
                        }
                        item.GatekeeperValidatedDate = UpdateDateTime;
                        //item.GatekeeperDisplayName = _user.LastFirstName;
                        item.GatekeeperDisplayName = string.IsNullOrEmpty(_user.DisplayName) ? _user.LastName + ", " + _user.FirstName + " " + _user.MiddleName : _user.DisplayName;
                        item.GatekeeperUserId = _user.Username;
                    }

                }
                //Save needs to happen outside the foreach then it works fine. :)
                dbRegistration.SaveChanges();
                
                //Update log table
                //LogUpdateType 1 = Gatekeeper 2 = ACG 3 = User Information
                Log logInsert = new Log()
                {
                    RegistrationId = myId,
                    UserId = _user.Username,
                    DisplayName = string.IsNullOrEmpty(_user.DisplayName) ? _user.LastName + ", " + _user.FirstName + " " + _user.MiddleName : _user.DisplayName,
                    UpdateDateTime = UpdateDateTime,
                    LogUpdateTypeId = 1
                };
                _db.Logs.Add(logInsert);
                int myLogId = logInsert.LogId;

                //Update LogGatekeeper Table
                LogGatekeeper logGatekeeperInsert = new LogGatekeeper()
                {
                    LogId = myLogId,
                    GatekeeperStatusId = myGatekeeperStatusId,
                    CernerPositionId = myCernerPositionId,
                    Note = tbGatekeeperNote.Text
                };
                _db.LogGatekeepers.Add(logGatekeeperInsert);
                _db.SaveChanges();

                gvRegistration.DataBind();
                fvRegistration.DataBind();
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
            if (ddlPrimaryOrganization.SelectedValue == "All")
            {
                //No Filtering on this
            }
            else
            {
                List<string> primaryLocationSearch = new List<string>();
                primaryLocationSearch.Add(ddlPrimaryOrganization.SelectedItem.ToString());
                e.Query = from ex in e.Query.Cast<Registration>() where primaryLocationSearch.Contains(ex.Organization.OrganizationName) select ex;
            }
            if (ddlRelationship.SelectedValue == "All")
            {
                //No Filtering on this
            }
            else
            {
                List<string> relationshipSearch = new List<string>();
                Int32 relationshipId = Convert.ToInt32(ddlRelationship.SelectedValue);
                e.Query = from ex in e.Query.Cast<Registration>() where (ex.RelationshipId == relationshipId) select ex;
            }

        }

        protected void btnUpdateGateKeeper_Click(object sender, EventArgs e)
        {
            //Get all the values of the form
            DateTime UpdateDateTime = DateTime.Now;
            TextBox tbGatekeeperNote = (TextBox)fvRegistration.FindControl("tbGatekeeperNote");
            DropDownList ddlGatekeeperCernerPosition = (DropDownList)fvRegistration.FindControl("ddlGatekeeperCernerPosition");
            DropDownList ddlGatekeeperStatus = (DropDownList)fvRegistration.FindControl("ddlGatekeeperStatus");
            int? myGatekeeperStatusId = null;
            int? myCernerPositionId = null;

            Registration registrationUpdate = (from x in _db.Registrations where x.RegistrationId == (int)gvRegistration.SelectedValue select x).First();

            registrationUpdate.GatekeeperNote = tbGatekeeperNote.Text;
            if (ddlGatekeeperCernerPosition.SelectedItem.ToString() == "Not Assigned")
            {
                //Set to null
                registrationUpdate.CernerPositionId = null;
                registrationUpdate.CernerPositionName = null;
                myCernerPositionId = null;
            }
            else
            {
                registrationUpdate.CernerPositionId = Convert.ToInt32(ddlGatekeeperCernerPosition.SelectedValue);
                registrationUpdate.CernerPositionName = ddlGatekeeperCernerPosition.SelectedItem.Text;
                myCernerPositionId = Convert.ToInt32(ddlGatekeeperCernerPosition.SelectedValue);
            }
            if (ddlGatekeeperStatus.SelectedItem.ToString() == "Not Assigned")
            {
                registrationUpdate.GatekeeperStatusId = null;
                registrationUpdate.GatekeeperStatusName = null;
                myGatekeeperStatusId = null;
            }
            else
            {
                registrationUpdate.GatekeeperStatusId = Convert.ToInt32(ddlGatekeeperStatus.SelectedValue);
                registrationUpdate.GatekeeperStatusName = ddlGatekeeperStatus.SelectedItem.Text;
                myGatekeeperStatusId = Convert.ToInt32(ddlGatekeeperStatus.SelectedValue);
            }

            //Update log table
            //LogUpdateType 1 = Gatekeeper 2 = ACG 3 = User Information
            Log logInsert = new Log()
            {
                RegistrationId = (int)gvRegistration.SelectedValue,
                UserId = _user.Username,
                DisplayName = string.IsNullOrEmpty(_user.DisplayName) ? _user.LastName + ", " + _user.FirstName + " " + _user.MiddleName : _user.DisplayName,
                UpdateDateTime = UpdateDateTime,
                LogUpdateTypeId = 1
            };
            _db.Logs.Add(logInsert);
            int myLogId = logInsert.LogId;

            //Update LogGatekeeper Table
            LogGatekeeper logGatekeeperInsert = new LogGatekeeper()
            {
                LogId = myLogId,
                GatekeeperStatusId = myGatekeeperStatusId,
                CernerPositionId = myCernerPositionId,
                Note = tbGatekeeperNote.Text,
            };
            _db.LogGatekeepers.Add(logGatekeeperInsert);
            _db.SaveChanges();

            gvRegistration.DataBind();
            fvRegistration.DataBind();
        }

        protected void btnUpdateUserInfo_Click(object sender, EventArgs e)
        {
            //Get all the values of the form
            TextBox tbFirstName = (TextBox)fvRegistration.FindControl("tbFirstName");
            TextBox tbMiddleName = (TextBox)fvRegistration.FindControl("tbMiddleName");
            TextBox tbLastName = (TextBox)fvRegistration.FindControl("tbLastName");
            DropDownList ddlPrimaryLocation = (DropDownList)fvRegistration.FindControl("ddlPrimaryLocation");

            TextBox tbHREmployeeId = (TextBox)fvRegistration.FindControl("tbHREmployeeId");
            TextBox tbHREmployeeEmail = (TextBox)fvRegistration.FindControl("tbHREmployeeEmail");
            TextBox tbHRSupervisor = (TextBox)fvRegistration.FindControl("tbHRSupervisor");

            TextBox tbHRCompany = (TextBox)fvRegistration.FindControl("tbHRCompany");
            TextBox tbHRDepartment = (TextBox)fvRegistration.FindControl("tbHRDepartment");

            TextBox tbProviderNPI = (TextBox)fvRegistration.FindControl("tbProviderNPI");
            TextBox tbProviderSpecialty = (TextBox)fvRegistration.FindControl("tbProviderSpecialty");
            DropDownList ddlProviderCredentials = (DropDownList)fvRegistration.FindControl("ddlProviderCredentials");

            DropDownList ddlNursingAncillaryCredential = (DropDownList)fvRegistration.FindControl("ddlNursingAncillaryCredential");

            TextBox tbTitleExternal = (TextBox)fvRegistration.FindControl("tbTitleExternal");
            TextBox tbCompanyExternal = (TextBox)fvRegistration.FindControl("tbCompanyExternal");
            TextBox tbCityExternal = (TextBox)fvRegistration.FindControl("tbCityExternal");

            TextBox tbContactNameExternal = (TextBox)fvRegistration.FindControl("tbContactNameExternal");
            TextBox tbContactPhoneExternal = (TextBox)fvRegistration.FindControl("tbContactPhoneExternal");

            //Update the record where textbox or ddl is not blank or default
            Registration registrationUpdate = (from x in _db.Registrations where x.RegistrationId == (int)gvRegistration.SelectedValue select x).First();

            StringBuilder changeString = new StringBuilder();

            if (!(string.IsNullOrWhiteSpace(tbFirstName.Text)))
            {
                changeString.AppendFormat("First Name Before: {0} - After: {1}<br/>", registrationUpdate.FirstName, tbFirstName.Text);
                registrationUpdate.FirstName = tbFirstName.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbMiddleName.Text)))
            {
                changeString.AppendFormat("Middle Name Before: {0} - After: {1}<br/>", registrationUpdate.MiddleName, tbMiddleName.Text);
                registrationUpdate.MiddleName = tbMiddleName.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbLastName.Text)))
            {
                changeString.AppendFormat("Last Name Before: {0} - After: {1}<br/>", registrationUpdate.LastName, tbLastName.Text);
                registrationUpdate.LastName = tbLastName.Text;
            }

            if (Convert.ToInt32(ddlPrimaryLocation.SelectedItem.Value) > 0)
            {
                changeString.AppendFormat("Primary Organization Before: {0} - After: {1}<br/>", registrationUpdate.Organization == null ? "" : registrationUpdate.Organization.OrganizationName, ddlPrimaryLocation.SelectedItem.Text);
                registrationUpdate.OrganizationId = Convert.ToInt32(ddlPrimaryLocation.SelectedItem.Value);
                registrationUpdate.OrganizationAbbreviation = ddlPrimaryLocation.SelectedItem.Text;
            }

            if (!(string.IsNullOrWhiteSpace(tbHREmployeeId.Text)))
            {
                changeString.AppendFormat("HR Employee Id Before: {0} - After: {1}<br/>", registrationUpdate.HREmployeeId, tbHREmployeeId.Text);
                registrationUpdate.HREmployeeId = tbHREmployeeId.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbHREmployeeEmail.Text)))
            {
                changeString.AppendFormat("HR Employee Email Before: {0} - After: {1}<br/>", registrationUpdate.HREmployeeEmail, tbHREmployeeEmail.Text);
                registrationUpdate.HREmployeeEmail = tbHREmployeeEmail.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbHRSupervisor.Text)))
            {
                changeString.AppendFormat("HR Supervisor Before: {0} - After: {1}<br/>", registrationUpdate.HRSupervisor, tbHRSupervisor.Text);
                registrationUpdate.HRSupervisor = tbHRSupervisor.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbHRCompany.Text)))
            {
                changeString.AppendFormat("HR Company Before: {0} - After: {1}<br/>", registrationUpdate.HRCompany, tbHRCompany.Text);
                registrationUpdate.HRCompany = tbHRCompany.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbHRDepartment.Text)))
            {
                changeString.AppendFormat("HR Department Before: {0} - After: {1}<br/>", registrationUpdate.HRDepartment, tbHRDepartment.Text);
                registrationUpdate.HRDepartment = tbHRDepartment.Text;
            }

            if (!(string.IsNullOrWhiteSpace(tbProviderNPI.Text)))
            {
                changeString.AppendFormat("Provider NPI Before: {0} - After: {1}<br/>", registrationUpdate.ProviderNPI, tbProviderNPI.Text);
                registrationUpdate.ProviderNPI = tbProviderNPI.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbProviderSpecialty.Text)))
            {
                changeString.AppendFormat("Provider NPI Before: {0} - After: {1}<br/>", registrationUpdate.ProviderSpecialty, tbProviderSpecialty.Text);
                registrationUpdate.ProviderSpecialty = tbProviderSpecialty.Text;
            }
            if (Convert.ToInt32(ddlProviderCredentials.SelectedItem.Value) > 0)
            {
                changeString.AppendFormat("Provider Credentials Before: {0} - After: {1}<br/>", registrationUpdate.Organization == null ? "" : registrationUpdate.ProviderCredentialsName, ddlProviderCredentials.SelectedItem.Text);
                registrationUpdate.ProviderCredentialsId = Convert.ToInt32(ddlProviderCredentials.SelectedItem.Value);
                registrationUpdate.ProviderCredentialsName = ddlProviderCredentials.SelectedItem.Text;
            }

            if (Convert.ToInt32(ddlNursingAncillaryCredential.SelectedItem.Value) > 0)
            {
                changeString.AppendFormat("Nursing Ancillary Credentials Before: {0} - After: {1}<br/>", registrationUpdate.NursingAncillaryCertification == null ? "" : registrationUpdate.NursingAncillaryCertification, ddlNursingAncillaryCredential.SelectedItem.Text);
                registrationUpdate.NursingAncillaryCredentialId = Convert.ToInt32(ddlNursingAncillaryCredential.SelectedItem.Value);
                registrationUpdate.NursingAncillaryCertification = ddlNursingAncillaryCredential.SelectedItem.Text;
            }

            if (!(string.IsNullOrWhiteSpace(tbTitleExternal.Text)))
            {
                changeString.AppendFormat("Title External Before: {0} - After: {1}<br/>", registrationUpdate.TitleExternal, tbTitleExternal.Text);
                registrationUpdate.TitleExternal = tbTitleExternal.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbCompanyExternal.Text)))
            {
                changeString.AppendFormat("Company External Before: {0} - After: {1}<br/>", registrationUpdate.CompanyExternal, tbCompanyExternal.Text);
                registrationUpdate.CompanyExternal = tbCompanyExternal.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbCityExternal.Text)))
            {
                changeString.AppendFormat("City External Before: {0} - After: {1}<br/>", registrationUpdate.CityExternal, tbCityExternal.Text);
                registrationUpdate.CityExternal = tbCityExternal.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbContactNameExternal.Text)))
            {
                changeString.AppendFormat("Contact Name External Before: {0} - After: {1}<br/>", registrationUpdate.ContactNameExternal, tbContactNameExternal.Text);
                registrationUpdate.ContactNameExternal = tbContactNameExternal.Text;
            }
            if (!(string.IsNullOrWhiteSpace(tbContactPhoneExternal.Text)))
            {
                changeString.AppendFormat("Contact Phone External Before: {0} - After: {1}<br/>", registrationUpdate.ContactPhoneExternal, tbContactPhoneExternal.Text);
                registrationUpdate.ContactPhoneExternal = tbContactPhoneExternal.Text;
            }

            _db.SaveChanges();

            //Update the Log tables
            DateTime UpdateDateTime = DateTime.Now;

            //LogUpdateType 1 = Gatekeeper 2 = ACG 3 = User Information
            Log logInsert = new Log()
            {
                RegistrationId = (int)gvRegistration.SelectedValue,
                UserId = _user.Username,
                DisplayName = string.IsNullOrEmpty(_user.DisplayName) ? _user.LastName + ", " + _user.FirstName + " " + _user.MiddleName : _user.DisplayName,
                UpdateDateTime = UpdateDateTime,
                LogUpdateTypeId = 3
            };
            _db.Logs.Add(logInsert);
            int myLogId = logInsert.LogId;

            //Update LogUserInformation table
            LogUserInformation logUserInformationInsert = new LogUserInformation()
            {
                LogId = myLogId,
                ValuesChanged = changeString.ToString()
            };
            _db.LogUserInformations.Add(logUserInformationInsert);
            _db.SaveChanges();

            gvRegistration.DataBind();
            fvRegistration.DataBind();

        }
    }
}