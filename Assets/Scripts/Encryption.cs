using System;
using System.Text;

public class Encryption
{
    // This is the key used for XOR encryption, it should be kept secret
    private static readonly string encryptionKey = "YourSecretKey";

    // Public method to encrypt the password
    public string Encrypt(string input)
    {
        // Convert the string input into a byte array
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

        // XOR each byte with the key bytes (cycling through the key)
        for (int i = 0; i < inputBytes.Length; i++)
        {
            inputBytes[i] ^= keyBytes[i % keyBytes.Length];
        }

        // Convert the encrypted byte array back to a string
        return Convert.ToBase64String(inputBytes);
    }
}
