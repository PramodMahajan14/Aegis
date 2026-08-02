namespace Aegis.Utility.Common
{
    public class GuidUtility
    {
        

        public static Guid ToGuid(string value)
        {
            return Guid.Parse(value);
        }

        public static Guid? ToNullableGuid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return Guid.TryParse(value, out var guid) ? guid : null ;
        }

        public static string ToString(Guid value)
        {
            return value.ToString();
        }
    }
}