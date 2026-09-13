namespace Adveshta.Model.Master
{
    public class JobeRolesVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }

    public class BasicJobRoleVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProspectStatusVm
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class ProspectTemperatureVm
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class ProjectStageVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}