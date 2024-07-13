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
			const string FileToDecrypt = @"GpgFolder\FileToDecrypt.txt.gpg";
			const string FileToDecryptTxt = @"GpgFolder\FileToDecrypt.txt";

			if (File.Exists(FileToDecryptTxt)) File.Delete(FileToDecryptTxt);

			//Act
			gpg.Decrypt(FileToDecrypt, FileToDecryptTxt, PrivateKeyName, PrivateKeyPassword);

			//Assert
			Assert.True(File.Exists(FileToDecryptTxt));
		}

		[Fact]
		public void DecryptAsync_ReturnsDecryptedFile_True()
		{
			//Arrange
			Gpg gpg = new(_baseConfiguration);
			const string FileToDecrypt = @"GpgFolder\FileToDecryptAsync.txt.gpg";
			const string FileToDecryptTxt = @"GpgFolder\FileToDecryptAsync.txt";

			if (File.Exists(FileToDecryptTxt)) File.Delete(FileToDecryptTxt);

			//Act
			_ = gpg.DecryptAsync(FileToDecrypt, FileToDecryptTxt, PrivateKeyName, PrivateKeyPassword);

			//Assert
			Assert.True(File.Exists(FileToDecryptTxt));
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