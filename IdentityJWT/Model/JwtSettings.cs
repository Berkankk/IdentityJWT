namespace IdentityJWT.Model
{
    public class JwtSettings
    {
        public string? Key { get; set; } //boş değer geçilebilir diyoruz burada 
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
