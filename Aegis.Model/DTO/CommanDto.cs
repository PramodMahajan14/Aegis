namespace Aegis.Model.DTO
{
    public class WorkSpaceSelectDto
    {
        public Guid WorkspaceId {get;set;}
    }

    public class RefreshTokenDto
    {
        public string? RefreshToken {get;set;} = null;
    }
}