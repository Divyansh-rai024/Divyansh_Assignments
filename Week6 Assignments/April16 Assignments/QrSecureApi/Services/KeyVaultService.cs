using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using System.Text;

public class KeyVaultService
{
    private readonly CryptographyClient _crypto;

    public KeyVaultService(string vaultUrl, string keyName)
    {
        var keyClient = new KeyClient(new Uri(vaultUrl), new DefaultAzureCredential());
        var key = keyClient.GetKey(keyName);

        _crypto = new CryptographyClient(key.Value.Id, new DefaultAzureCredential());
    }

    public async Task<string> EncryptAsync(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var result = await _crypto.EncryptAsync(EncryptionAlgorithm.RsaOaep, bytes);
        return Convert.ToBase64String(result.Ciphertext);
    }

    public async Task<string> DecryptAsync(string encrypted)
    {
        var bytes = Convert.FromBase64String(encrypted);
        var result = await _crypto.DecryptAsync(EncryptionAlgorithm.RsaOaep, bytes);
        return Encoding.UTF8.GetString(result.Plaintext);
    }
}