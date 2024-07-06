using Microsoft.Extensions.Configuration;
using PgpCore;

namespace Utilities.Gpg.Tests
{
	public class UnitTest1
	{
		private readonly IConfiguration _baseConfiguration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>()
			{
				{"Gpg:KeyFolderPath", @"C:\MyGpgFolder\"}
			})
			.Build();

		[Fact]
		public void GetPgpForDecryption_ReturnsValidPgpCoreClass_True()
		{
			//Arrange
			Gpg gpg = new Gpg(_baseConfiguration);
			string privateKeyName = "MyDecryptionKey.asc";
			string privateKeyPassword = "Password";

			//Act
			PGP pgp = gpg.GetPgpForDecryption(privateKeyName, privateKeyPassword);

			//Assert
			
		}

		[Fact]
		public void GetPgpForEncryption_ReturnsValidPgpCoreClass_True()
		{
			//Arrange
			Gpg gpg = new Gpg(_baseConfiguration);
			string publicKeyName = "MyEncryptionKey.asc";
			
			//Act
			PGP pgp = gpg.GetPgpForEncryption(publicKeyName);

			//Assert

		}
	}
}