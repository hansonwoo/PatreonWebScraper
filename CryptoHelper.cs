using System.Security.Cryptography;
using System.Text;

public class CryptoHelper
{
    // TODO:
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("Your32ByteLongEncryptionKeyHere!"); // Must be 32 bytes for AES-256

    // Encrypt a string and return the result as base64
    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.GenerateIV(); // Random IV for each encryption

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        // Prepend IV to the ciphertext
        byte[] combinedBytes = new byte[aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, combinedBytes, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, combinedBytes, aes.IV.Length, cipherBytes.Length);

        return Convert.ToBase64String(combinedBytes);
    }

    // Decrypt a base64 string using the key
    public static string Decrypt(string cipherTextBase64)
    {
        byte[] combinedBytes = Convert.FromBase64String(cipherTextBase64);

        using var aes = Aes.Create();
        aes.Key = Key;

        // Extract IV
        byte[] iv = new byte[aes.BlockSize / 8];
        byte[] cipherBytes = new byte[combinedBytes.Length - iv.Length];
        Buffer.BlockCopy(combinedBytes, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(combinedBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    // Generate a secure random key
    public static byte[] GenerateKey()
    {
        using var aes = Aes.Create();
        aes.GenerateKey();
        return aes.Key;
    }
}