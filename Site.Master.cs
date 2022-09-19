using RegistrationGatekeeperAdmin.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RegistrationGatekeeperAdmin
{
    public partial class SiteMaster : MasterPage
    {
        private McUser _user = new McUser().GetUserByUsername(HttpContext.Current.User.Identity.Name);
        protected void Page_Load(object sender, EventArgs e)
        {
            lblName.Text = _user.DisplayName;

            //Get admin tab
            if (HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers) || HttpContext.Current.User.IsInRole(MyRoles.Gatekeepers) || HttpContext.Current.User.IsInRole(MyRoles.ACG))
            {
                HtmlGenericControl _li = new HtmlGenericControl("li");
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", "WhoIs.aspx");
                anchor.InnerText = "Access";
                _li.Controls.Add(anchor);
                phMenu.Controls.Add(_li);
            }

        }
    }
}