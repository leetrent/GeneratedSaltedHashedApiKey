using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string apiKey = GenerateApiKey();
        string salt = GenerateSalt();
        string saltedHash = GenerateSaltedHash(apiKey, salt);

        Console.WriteLine($"API Key....: '{apiKey}'");
        Console.WriteLine($"Salt.......: '{salt}'");
        Console.WriteLine($"Salted Hash: '{saltedHash}'");

        // Simulating verification of the API key 
        //string incomingApiKey = apiKey; // This should come from the API request
        string incomingApiKey = "ba201e6b-e301-4c46-bec0-70e1086fa9ec";
        string incomingSaltedHash = GenerateSaltedHash(incomingApiKey, salt);

        Console.WriteLine();
        Console.WriteLine($"Incoming API Key....: '{incomingApiKey}'");
        Console.WriteLine($"Incoming Salted Hash: '{incomingSaltedHash}'");
        Console.WriteLine($"Salted Hash.........: '{saltedHash}'");

        Console.WriteLine();
        Console.WriteLine($"(saltedHash == incomingSaltedHash): '{saltedHash == incomingSaltedHash}'");


        if (saltedHash == incomingSaltedHash)
        {
            Console.WriteLine("API Key is valid.");
        }
        else
        {
            Console.WriteLine("API Key is invalid.");
        }
    }

    static string GenerateApiKey()
    {
        return Guid.NewGuid().ToString();
    }

    static string GenerateSalt()
    {
        byte[] saltBytes = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }

    static string GenerateSaltedHash(string apiKey, string salt)
    {
        byte[] apiKeyBytes = Encoding.UTF8.GetBytes(apiKey + salt);

        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] hashBytes = sha256Hash.ComputeHash(apiKeyBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
