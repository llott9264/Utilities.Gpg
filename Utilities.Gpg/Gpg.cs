using Microsoft.Extensions.Configuration;
using PgpCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Utilities.Gpg.Tests")]
namespace Utilities.Gpg;

public class Gpg(IConfiguration configuration) : IGpg
{
	public string KeyFolderPath => configuration.GetValue<string>("Gpg:KeyFolderPath");

	public async Task DecryptAsync(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForDecryption(privateKeyName, privateKeyPassword))
		{
			await pgp.DecryptFileAsync(inputFile, outputFile);
		}
	}

	public void Decrypt(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);
		
		using (PGP pgp = GetPgpForDecryption(privateKeyName, privateKeyPassword))
		{
			pgp.DecryptFile(inputFile, outputFile);
		}
	}

	public async Task EncryptAsync(string inputFileLocation, string outputFileLocation, string publicKeyName)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForEncryption(publicKeyName))
		{
			await pgp.EncryptFileAsync(inputFile, outputFile);
		}
	}

	public void Encrypt(string inputFileLocation, string outputFileLocation, string publicKeyName)
	{
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);

		using (PGP pgp = GetPgpForEncryption(publicKeyName))
		{
			pgp.EncryptFile(inputFile, outputFile);
		}
	}

	internal PGP GetPgpForDecryption(string privateKeyName, string privateKeyPassword)
	{
		FileInfo privateKey = new($"{KeyFolderPath}{privateKeyName}");
		EncryptionKeys decryptionKey = new(privateKey, privateKeyPassword);
		PGP pgp = new(decryptionKey);
		return pgp;
	}

	internal PGP GetPgpForEncryption(string publicKeyName)
	{
		FileInfo publicKey = new($"{KeyFolderPath}{publicKeyName}");
		EncryptionKeys encryptionKey = new(publicKey);
		PGP pgp = new(encryptionKey);
		return pgp;
	}
}