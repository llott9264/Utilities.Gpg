using Microsoft.Extensions.Configuration;
using PgpCore;

namespace Utilities.Gpg;

public class Gpg(IConfiguration configuration) : IGpg
{
	public string KeyFolderPath => configuration.GetValue<string>("Gpg:KeyFolderPath");

	public async Task DecryptFileAsync(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForDecryption(privateKeyName, privateKeyPassword))
		{
			await pgp.DecryptFileAsync(inputFile, outputFile);
		}
	}

	public void DecryptFile(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForDecryption(privateKeyName, privateKeyPassword))
		{
			pgp.DecryptFile(inputFile, outputFile);
		}
	}

	public async Task EncryptFileAsync(string inputFileLocation, string outputFileLocation, string publicKeyName)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForEncryption(publicKeyName))
		{
			await pgp.EncryptFileAsync(inputFile, outputFile);
		}
	}

	public void EncryptFile(string inputFileLocation, string outputFileLocation, string publicKeyName)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForEncryption(publicKeyName))
		{
			pgp.EncryptFile(inputFile, outputFile);
		}
	}

	public async Task<string> DecryptAsync(string input, string privateKeyName, string privateKeyPassword)
	{
		using (PGP pgp = GetPgpForDecryption(privateKeyName, privateKeyPassword))
		{
			return await pgp.DecryptAsync(input);
		}
	}

	public string Decrypt(string input, string privateKeyName, string privateKeyPassword)
	{
		using (PGP pgp = GetPgpForDecryption(privateKeyName, privateKeyPassword))
		{
			return pgp.Decrypt(input);
		}
	}

	public async Task<string> EncryptAsync(string input, string publicKeyName)
	{
		using (PGP pgp = GetPgpForEncryption(publicKeyName))
		{
			return await pgp.EncryptAsync(input);
		}
	}

	public string Encrypt(string input, string publicKeyName)
	{
		using (PGP pgp = GetPgpForEncryption(publicKeyName))
		{
			return pgp.Encrypt(input);
		}
	}
	
	private PGP GetPgpForDecryption(string privateKeyName, string privateKeyPassword)
	{
		FileInfo privateKey = new($"{KeyFolderPath}{privateKeyName}");
		EncryptionKeys decryptionKey = new(privateKey, privateKeyPassword);
		PGP pgp = new(decryptionKey);
		return pgp;
	}

	private PGP GetPgpForEncryption(string publicKeyName)
	{
		FileInfo publicKey = new($"{KeyFolderPath}{publicKeyName}");
		EncryptionKeys encryptionKey = new(publicKey);
		PGP pgp = new(encryptionKey);
		return pgp;
	}
}