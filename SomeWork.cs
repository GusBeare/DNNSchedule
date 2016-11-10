using Simple.Data;

namespace DNNScheduledTask.DotNetNuke
{
    public static class SomeWork
    {
        public static void TruncateEventLog()
        {
            // Use Simple.Data to connect to DNN and Purge the event log by calling the PurgeEventLog procedure.
            var db = Database.OpenNamedConnection("SiteSQLServer");
            db.PurgeEventLog();

            // NB: as usual this is a bit of a mess in DNN 7.4 right now. See this:
            // http://www.dnnsoftware.com/community-blog/cid/155180/howto-truncate-your-eventlog-in-dnn-740
            // Ideally use the script for now. Dunno about the PurgeEventLog proc. Out of date?
            // what does DNN code do when you clear the event log? 

       

        }
      
    }
}
