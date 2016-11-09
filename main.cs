using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Scheduling;
using System;



// When installing the schedule in DNN use this full class and assembly name:
// DNNScheduledTask.DotNetNuke.MyTask,DNNScheduledTask

namespace DNNScheduledTask.DotNetNuke
{
    internal class MyTask : SchedulerClient
    {
        public MyTask(ScheduleHistoryItem objScheduleHistoryItem)
        {
            ScheduleHistoryItem = objScheduleHistoryItem;
        }

        public override void DoWork()
        {
           
            try
            {
                // add a note to record task being run   
                ScheduleHistoryItem.AddLogNote("<br/><strong>MyTask job was started!</strong>");

                // do some work
                SomeWork.TruncateEventLog();

                // add a note to record success   
                ScheduleHistoryItem.AddLogNote("<br/>");
                ScheduleHistoryItem.AddLogNote("<br/><strong>MyTask job ran successfully!</strong>");
                   
               
                // report success to the scheduler framework
                ScheduleHistoryItem.Succeeded = true;
               

            }
            catch (Exception exc)
            {
                ScheduleHistoryItem.Succeeded = false;
                ScheduleHistoryItem.AddLogNote("EXCEPTION: " + exc);
                Errored(ref exc);
                Exceptions.LogException(exc);
            }
        }

     
    }
}

    

