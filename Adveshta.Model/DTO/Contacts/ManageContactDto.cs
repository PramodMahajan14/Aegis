namespace Adveshta.Model.DTO.Contacts
{
    public class ManageContactDto
    {
        public Guid? Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string JobRole { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool IsPrimary { get; set; } = false;

        public Guid ProspectId { get; set; }

        public Guid ProjectContactRole { get; set; }

    }
}