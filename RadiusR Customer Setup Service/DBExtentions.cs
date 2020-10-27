using RadiusR.DB;
using RadiusR.DB.Enums.CustomerSetup;
using RadiusR_Customer_Setup_Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    public static class DBExtentions
    {
        private static short[] UpdatableStatuses = new[]{
            (short)TaskStatuses.New,
            (short)TaskStatuses.InProgress,
            (short)TaskStatuses.Halted
            };
        public static IQueryable<CustomerSetupTask> GetUserTasks(this IQueryable<CustomerSetupTask> tasks, int userId)
        {
            return tasks.Where(task => task.SetupUserID == userId);
        }

        public static bool CanBeUpdated(this CustomerSetupTask task)
        {
            return true;
            // fuck argeset -- removed all checks.
            var compareDate = DateTime.Now.AddDays(30);
            var firstCompleted = GetFirstCompleted(task);
            return UpdatableStatuses.Contains(task.TaskStatus) || firstCompleted == null || firstCompleted.Date < compareDate;
        }

        public static bool CanChangeState(this CustomerSetupTask task, FaultCodes newFaultCode)
        {
            return true;
            // fuck argeset -- removed all checks.
            var compareDate = DateTime.Now.AddDays(30);
            var firstCompleted = GetFirstCompleted(task);
            if ((firstCompleted == null || firstCompleted.Date < compareDate) && task.TaskStatus == (short)CustomConverter.GetFaultCodeTaskStatus(newFaultCode))
                return true;
            return UpdatableStatuses.Contains(task.TaskStatus);
        }

        private static CustomerSetupStatusUpdate GetFirstCompleted(CustomerSetupTask task)
        {
            return task.CustomerSetupStatusUpdates.OrderBy(update => update.Date).FirstOrDefault(update => update.FaultCode == (short)FaultCodes.SetupComplete);
        }
    }
}