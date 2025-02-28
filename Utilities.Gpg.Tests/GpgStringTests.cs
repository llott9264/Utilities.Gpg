using Microsoft.Extensions.Configuration;

namespace Utilities.Gpg.Tests;

public class GpgStringTests
{
	private const string PrivateKeyName = "PrivateKey.asc";
	private const string PrivateKeyPassword = "password";
	private const string PublicKeyName = "PublicKey.asc";

	private const string StringToEncrypt = "Hello World!";
	private const string StringToDecrypt = "-----BEGIN PGP MESSAGE-----\r\nVersion: BouncyCastle.NET Cryptography (net6.0) v2.4.0+83ebf4a805\r\n\r\nhF4DY5b9b4k7/8sSAQdAsSBbTRA75o3Znsa80ghrYZNOOVHvFQtrKignPnlN9yww\r\nzONUIa6m1qkIfgeDfqWorMqHwwNctDIH0PP1k/QqwdnOvSBQrAFKLgCFV01YMkSE\r\n0jkBYADwndlZBZWNZuTxeQ8xQ9gQJ9QO0dLzcQuAjaSwfydApaAVaT1J+E4iweto\r\nXWnX3+ZjodNQLa4=\r\n=SH6M\r\n-----END PGP MESSAGE-----\r\n";

	private readonly IConfiguration _baseConfiguration = new ConfigurationBuilder()
		.AddInMemoryCollection(new Dictionary<string, string>()
		{
			{"Gpg:KeyFolderPath", @"GpgFolder\"}
		})
		.Build();

	[Fact]
	public void Decrypt_ReturnsDecryptedString_True()
	{
		//Arrange
		Gpg gpg = new(_baseConfiguration);

		//Act
		string decryptedString = gpg.Decrypt(StringToDecrypt, PrivateKeyName, PrivateKeyPassword);

		//Assert
		Assert.True(decryptedString == StringToEncrypt);
	}

	[Fact]
	public async Task DecryptAsync_ReturnsDecryptedString_True()
	{
		//Arrange
		Gpg gpg = new(_baseConfiguration);

		//Act
		string decryptedString = await gpg.DecryptAsync(StringToDecrypt, PrivateKeyName, PrivateKeyPassword);

		//Assert
		Assert.True(decryptedString == StringToEncrypt);
	}

	[Fact]
	public void Encrypt_ReturnsEncryptedString_True()
	{
		//Arrange
		Gpg gpg = new(_baseConfiguration);

		//Act
		string encryptedString = gpg.Encrypt(StringToEncrypt, PublicKeyName);


		//Assert
		Assert.Contains("-----BEGIN PGP MESSAGE-----", encryptedString);
		Assert.Contains("-----END PGP MESSAGE-----", encryptedString);
	}

	[Fact]
	public async Task EncryptAsync_ReturnsEncryptedString_True()
	{
		//Arrange
		Gpg gpg = new(_baseConfiguration);

		//Act
		string encryptedString = await gpg.EncryptAsync(StringToEncrypt, PublicKeyName);

		//Assert
		Assert.Contains("-----BEGIN PGP MESSAGE-----", encryptedString);
		Assert.Contains("-----END PGP MESSAGE-----", encryptedString);
	}
}