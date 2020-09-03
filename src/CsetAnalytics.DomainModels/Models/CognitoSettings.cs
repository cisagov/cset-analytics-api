namespace CsetAnalytics.DomainModels.Models
{
    public class CognitoSettings : ICognitoSettings
    {
        public string Region { get; set; }
        public string PoolId { get; set; }
        public string AppClientId { get; set; }
    }

    public interface ICognitoSettings
    {
        string Region { get; set; }
        string PoolId { get; set; }
        string AppClientId { get; set; }
    }
}
