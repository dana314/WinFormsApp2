public class PasswordRecoveryService
{
    private readonly Random random = new();

    public string GenerateCode()
    {
        return random.Next(1000, 9999).ToString(); 
    }

    public void NewCode(string code, string phoneNumber)
    {
        
        Console.WriteLine($"Код {code} отправлен на {phoneNumber}");
    }
}