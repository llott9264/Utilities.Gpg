namespace Utilities.Gpg;

public interface IGpg
{
	Task DecryptFileAsync(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword);
	void DecryptFile(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword);
	Task EncryptFileAsync(string inputFileLocation, string outputFileLocation, string publicKeyName);
	void EncryptFile(string inputFileLocation, string outputFileLocation, string publicKeyName);
	Task<string> DecryptAsync(string input, string privateKeyName, string privateKeyPassword);
	string Decrypt(string input, string privateKeyName, string privateKeyPassword);
	Task<string> EncryptAsync(string input, string publicKeyName);
	string Encrypt(string input, string publicKeyName);
}