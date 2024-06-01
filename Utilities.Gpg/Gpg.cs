using Microsoft.Extensions.Configuration;
using PgpCore;

namespace Utilities.Gpg;

public class Gpg(IConfiguration configuration) : IGpg
{
	public async Task DecryptAsync(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword)
	{
		FileInfo privateKey = new($"{configuration.GetValue<string>("Gpg:KeyFolderPath")}{privateKeyName}");
		EncryptionKeys decryptionKey = new(privateKey, privateKeyPassword);
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);
		PGP pgp = new(decryptionKey);
		await pgp.DecryptFileAsync(inputFile, outputFile);
	}

	public void Decrypt(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword)
	{
		FileInfo privateKey = new($"{configuration.GetValue<string>("Gpg:KeyFolderPath")}{privateKeyName}");
		EncryptionKeys decryptionKey = new(privateKey, privateKeyPassword);
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);
		PGP pgp = new(decryptionKey);
		pgp.DecryptFile(inputFile, outputFile);
		
	}

	public async Task EncryptAsync(string inputFileLocation, string outputFileLocation, string publicKeyName)
	{

		FileInfo publicKey = new($"{configuration.GetValue<string>("Gpg:KeyFolderPath")}{publicKeyName}");
		EncryptionKeys encryptionKey = new(publicKey);
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);
		PGP pgp = new(encryptionKey);
		await pgp.EncryptFileAsync(inputFile, outputFile);
	}

	public void Encrypt(string inputFileLocation, string outputFileLocation, string publicKeyName)
	{
		FileInfo publicKey = new($"{configuration.GetValue<string>("Gpg:KeyFolderPath")}{publicKeyName}");
		EncryptionKeys encryptionKey = new(publicKey);
		FileInfo inputFile = new(inputFileLocation);
		FileInfo outputFile = new(outputFileLocation);
		PGP pgp = new(encryptionKey);
		pgp.EncryptFile(inputFile, outputFile);
	}

	public void EncryptAndSign(string inputFileLocation, string outputFileLocation, string privateKeyLocation,
		string privateKeyPassword)
	{
		throw new NotImplementedException();
	}
}