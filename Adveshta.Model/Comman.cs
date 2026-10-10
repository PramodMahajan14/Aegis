namespace Adveshta.Model
{
    public class BaseCreateUpdate
    {
        public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

        public DateTime? UpdatedAt {get;set;}
    }


    public class DateRange
    {
        public DateTime From {get;set;}
        public DateTime To {get;set;}
    }
}