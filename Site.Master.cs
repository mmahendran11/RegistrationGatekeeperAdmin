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
            string strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            string strURL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "");
            string strAppPath = HttpContext.Current.Request.ApplicationPath.ToString();

            bool developers = HttpContext.Current.User.IsInRole(MyRoles.WebDevelopers);
            //bool overAllAdmins = HttpContext.Current.User.IsInRole(MyRoles.OverallAdmin);

            if (strAppPath == "/")
            {
                strAppPath = string.Empty;
            }

            lblName.Text = _user.DisplayName;

            //Get admin tab
            if (developers)
            {
                HtmlGenericControl _li = new HtmlGenericControl("li");
                HtmlGenericControl anchor = new HtmlGenericControl("a");

                anchor.Attributes.Add("href", strURL + strAppPath + "/Views/Admin/Index.aspx");
                anchor.InnerText = "Admin";
                _li.Controls.Add(anchor);
                phMenu.Controls.Add(_li);
            }
        }
    }
}