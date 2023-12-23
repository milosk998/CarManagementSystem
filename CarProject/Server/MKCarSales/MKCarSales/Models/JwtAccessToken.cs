namespace MKCarSales.Models;
public class JwtAccessToken
{
    public string Username { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
