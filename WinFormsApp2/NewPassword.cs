public class PasswordRecoveryService
{
    private readonly Random random = new();

    public string GenerateCode()
    {
        return random.Next(100000, 999999).ToString(); 
    }

    public void NewCode(string code, string phoneNumber)
    {
        
        Console.WriteLine($"Код {code} отправлен на {phoneNumber}");
    }
}