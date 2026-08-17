using Aegis.Model.Master;
using Aegis.Utility.Enum;
namespace Aegis.Model.Tenant
{
    public class Tenant
    {
        // Identity
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;


        // Contact
        public string Email { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? ContactNumber { get; set; }

        // Tenant URL / Routing
        public string? DomainName { get; set; }

        // Localization
        public string TimeZone { get; set; } = "Asia/Kolkata";

        public string Currency { get; set; } = "INR";

        public string Locale { get; set; } = "en-IN";

        // Lifecycle
        public TenantStatus Status { get; set; } = TenantStatus.Active;

        public DateTime OnboardingDate { get; set; }

        public DateTime? SuspendedAt { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        // Subscription
        public Guid? SubscriptionPlanId { get; set; }

        public DateTime? SubscriptionStartDate { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        // Special tenant
        public bool IsSystemTenant { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<ApplicationRole> ApplicationRoles {get;set;} = new List<ApplicationRole>();

        public ICollection<JobRole> JobRoles {get;set;} = new List<JobRole>();
    }

}