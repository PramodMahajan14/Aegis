namespace Adveshta.Model.DTO
{


    public class BaseMasterDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class WorkSpaceSelectDto
    {
        public Guid WorkspaceId { get; set; }
    }

    public class RefreshTokenDto
    {
        public string? RefreshToken { get; set; } = null;
    }


    // Project Stage

    public class ProjectStatgeDto : BaseMasterDTO
    {
        public Guid? Id { get; set; } = null;
    
    }
}