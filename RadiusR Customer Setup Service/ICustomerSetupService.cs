using RadiusR_Customer_Setup_Service.Requests;
using RadiusR_Customer_Setup_Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RadiusR_Customer_Setup_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICustomerSetupService
    {
        [OperationContract]
        string GetKeyFragment(string username);

        [OperationContract]
        GetTaskListResponse GetTaskList(GetTaskListRequest request);

        [OperationContract]
        GetTaskDetailsResponse GetTaskDetails(TaskNoRequest request);

        [OperationContract]
        GetCustomerCredentialResponse GetCustomerCredentials(TaskNoRequest request);

        [OperationContract]
        GetCustomerLineDetailsResponse GetCustomerLineDetails(TaskNoRequest request);

        [OperationContract]
        GetCustomerSessionInfoResponse GetCustomerSessionInfo(TaskNoRequest request);

        [OperationContract]
        ParameterlessResponse AddTaskStatusUpdate(AddTaskStatusUpdateRequest request);

        [OperationContract]
        ParameterlessResponse UpdateClientLocation(UpdateCustomerLocationRequest request);

        [OperationContract]
        ParameterlessResponse AddCustomerAttachment(AddCustomerAttachmentRequest request);

        [OperationContract]
        GetCustomerContractResponse GetCustomerContract(TaskNoRequest request);
    }
}
