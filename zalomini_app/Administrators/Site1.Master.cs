using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace zalomini_app.Administrators
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
           
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            
            Session.Clear();
            Response.Redirect("~/Login/login.aspx?logout=true");
        }
    }
}