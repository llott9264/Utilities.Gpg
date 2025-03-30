using Moq;
using Utilities.Gpg.MediatR;

namespace Utilities.Gpg.Tests;

public class MediaRTests
{
	[Fact]
	public void EncryptFileHandler_CallsEncryptFileAsync()
	{
		//Arrange
		Mock<IGpg> mock = new();
		EncryptFileCommand request = new("inputFileLocation", "outputFileLocation", "publicKeyName");
		EncryptFileHandler handler = new(mock.Object);

		//Act
		_ = handler.Handle(request, CancellationToken.None);

		//Assert
		mock.Verify(g => g.EncryptFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		mock.VerifyNoOtherCalls();
	}

	[Fact]
	public void DecryptFileHandler_CallsDecryptFileAsync()
	{
		//Arrange
		Mock<IGpg> mock = new();
		DecryptFileCommand request = new("inputFileLocation", "outputFileLocation", "privateKeyName", "privateKeyPassword");
		DecryptFileCommandHandler handler = new(mock.Object);

		//Act
		_ = handler.Handle(request, CancellationToken.None);

		//Assert
		mock.Verify(g => g.DecryptFileAsync(It.Is<string>(s => s == "inputFileLocation"), 
			It.Is<string>(s => s == "outputFileLocation"), 
			It.Is<string>(s => s == "privateKeyName"), 
			It.Is<string>(s => s == "privateKeyPassword")), Times.Once);
		mock.VerifyNoOtherCalls();
	}
}
