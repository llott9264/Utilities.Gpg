using Microsoft.Extensions.Configuration;

namespace Utilities.Gpg.Tests
{
	public class GpgTests
	{
		private const string PrivateKeyName = "PrivateKey.asc";
		private const string PrivateKeyPassword = "password";
		private const string PublicKeyName = "PublicKey.asc";
		
		private readonly IConfiguration _baseConfiguration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>()
			{
				{"Gpg:KeyFolderPath", @"GpgFolder\"}
			})
			.Build();

		[Fact]
		public void Decrypt_ReturnsDecryptedFile_True()
		{
			//Arrange
			Gpg gpg = new(_baseConfiguration);
			const string fileToDecrypt = @"GpgFolder\FileToDecrypt.txt.gpg";
			const string fileToDecryptTxt = @"GpgFolder\FileToDecrypt.txt";

			if (File.Exists(fileToDecryptTxt)) File.Delete(fileToDecryptTxt);

			//Act
			gpg.Decrypt(fileToDecrypt, fileToDecryptTxt, PrivateKeyName, PrivateKeyPassword);

			//Assert
			Assert.True(File.Exists(fileToDecryptTxt));
		}

		[Fact]
		public void DecryptAsync_ReturnsDecryptedFile_True()
		{
			//Arrange
			Gpg gpg = new(_baseConfiguration);
			const string fileToDecrypt = @"GpgFolder\FileToDecryptAsync.txt.gpg";
			const string fileToDecryptTxt = @"GpgFolder\FileToDecryptAsync.txt";

			if (File.Exists(fileToDecryptTxt)) File.Delete(fileToDecryptTxt);

			//Act
			_ = gpg.DecryptAsync(fileToDecrypt, fileToDecryptTxt, PrivateKeyName, PrivateKeyPassword);

			//Assert
			Assert.True(File.Exists(fileToDecryptTxt));
		}

		[Fact]
		public void Encrypt_ReturnsEncryptedFile_True()
		{
			//Arrange
			Gpg gpg = new(_baseConfiguration);
			const string fileToEncrypt = @"GpgFolder\FileToEncrypt.txt";
			const string fileToEncryptGpg = @"GpgFolder\FileToEncrypt.txt.gpg";

			if (File.Exists(fileToEncryptGpg)) File.Delete(fileToEncryptGpg);
			
			//Act
			gpg.Encrypt(fileToEncrypt, fileToEncryptGpg, PublicKeyName);
			
			//Assert
			Assert.True(File.Exists(fileToEncryptGpg));
		}

		[Fact]
		public void EncryptAsync_ReturnsEncryptedFile_True()
		{
			//Arrange
			Gpg gpg = new(_baseConfiguration);
			const string fileToEncryptAsync = @"GpgFolder\FileToEncryptAsync.txt";
			const string fileToEncryptAsyncGpg = @"GpgFolder\FileToEncryptAsync.txt.gpg";
			
			if (File.Exists(fileToEncryptAsyncGpg)) File.Delete(fileToEncryptAsyncGpg);

			//Act
			_ = gpg.EncryptAsync(fileToEncryptAsync, fileToEncryptAsyncGpg, PublicKeyName);

			//Assert
			Assert.True(File.Exists(fileToEncryptAsyncGpg));
		}
	}
}