namespace EventManagement.Application.Helpers
{
    public class RepositoryResponse
    {
        public object _responseData { get; set; }
        public RepositoryResponse(object responseData)
        {
            _responseData = responseData;
        }
    }
    public static class RepositoryResponseError
    {
        public const string CreateSuccess = "Record Created Successfully";
        public const string CannotCreate = "Record Was Not Created";
        public const string CannotDelete = "Record Was Not Deleted";
        public const string CannotRead = "Cannot Get Record";
        public const string DeleteSuccessful = "Deleted Successfully";
        public const string AlreadyExist = "Event already booked on same venue and date";
        public const string UnAuthorizedAccess = "You are not authorized to access this functionality";
    }
}
