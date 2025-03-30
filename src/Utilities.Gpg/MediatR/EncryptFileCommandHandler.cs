using MediatR;

namespace Utilities.Gpg.MediatR;

public class EncryptFileHandler(IGpg gpg) : IRequestHandler<EncryptFileCommand>
{
	public async Task Handle(EncryptFileCommand request, CancellationToken cancellationToken)
	{
		await gpg.EncryptFileAsync(request.InputFileLocation, request.OutputFileLocation, request.PublicKeyName);
	}
}