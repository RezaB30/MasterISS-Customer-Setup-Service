using RadiusR.DB.Enums.CustomerSetup;
using RadiusR_Customer_Setup_Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadiusR_Customer_Setup_Service
{
    public static class CustomConverter
    {
        public static TaskStatuses GetFaultCodeTaskStatus(FaultCodes faultCode)
        {
            switch (faultCode)
            {
                case FaultCodes.RendezvousMade:
                case FaultCodes.WaitingForNewRendezvous:
                case FaultCodes.NoFault:
                    return TaskStatuses.InProgress;
                case FaultCodes.InvalidAddress:
                case FaultCodes.TelekomLineFault:
                case FaultCodes.ClientCancelled:
                case FaultCodes.ModemFault:
                case FaultCodes.CustomerCouldNotBeReached:
                case FaultCodes.TelekomGeneralFault:
                case FaultCodes.BuildingInstallationFault:
                    return TaskStatuses.Halted;
                case FaultCodes.SetupComplete:
                    return TaskStatuses.Completed;
                case FaultCodes.CustomerDidTheSetup:
                    return TaskStatuses.Cancelled;
                default:
                    return TaskStatuses.New;
            }
        }

        public static DateTime? GetCompletionDate(TaskStatuses status)
        {
            switch (status)
            {
                case TaskStatuses.Completed:
                case TaskStatuses.Cancelled:
                    return DateTime.Now;
                case TaskStatuses.New:
                case TaskStatuses.InProgress:
                case TaskStatuses.Halted:
                default:
                    return null;
            }
        }
    }
}