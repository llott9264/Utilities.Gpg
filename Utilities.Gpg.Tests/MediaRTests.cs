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
}