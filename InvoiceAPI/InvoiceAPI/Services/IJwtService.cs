namespace InvoiceAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
