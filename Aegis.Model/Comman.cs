namespace Aegis.Model
{
    public class BaseCreateUpdate
    {
        public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

        public DateTime? UpdatedAt {get;set;}
    }
}