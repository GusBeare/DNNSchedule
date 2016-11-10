using System;
using DNNScheduledTask.DotNetNuke;


namespace TestWebSite
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // button calls the same code as the scheduled task
        protected void btnRunSomeWork_OnClick(object sender, EventArgs e)
        {
            SomeWork.TruncateEventLog();
        }
    }
}