namespace QrSecureApi.Models
{
    public class LoginSession
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public bool IsLoggedIn { get; set; } = false;
        public string? Token { get; set; }
    }
}
