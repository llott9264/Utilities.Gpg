namespace Utilities.Gpg;

public interface IGpg
{
	Task DecryptAsync(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword);
	void Decrypt(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword);
	Task EncryptAsync(string inputFileLocation, string outputFileLocation, string publicKeyName);
	void Encrypt(string inputFileLocation, string outputFileLocation, string publicKeyName);
}