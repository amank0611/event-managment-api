using Microsoft.Extensions.Configuration;

namespace EventManagement.Application.Utilities
{
    public class ApplicationConstants
    {
        private IConfiguration _configuration { get; }
        public ApplicationConstants(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public enum RequestResponse
        {
            Success,
            Error,
            Failure
        }
        public enum ReactComponents
        {
            UserComponent,
            EventComponent,
            VenueComponent
        }
        public const double JwtExpireTime = 480;      // in minutes
        public const string AuthenticationSchemes = "Bearer";
        public static string ConnectionString = string.Empty;
    }
    public static class ApplicationRoles
    {
        public const string Organiser = "Organiser";
        public const string User = "User";
        public const string Admin = "Admin";
    }
    public enum EventStatus
    {
        Pending = 0,
        Approved = 1,
        Completed = 2,
        Cancelled = 3,
    }
    public enum UserRole
    {
        Organiser = 1,
        User = 2,
        Admin = 3
    }
}
