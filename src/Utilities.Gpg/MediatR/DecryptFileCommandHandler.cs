using MediatR;

namespace Utilities.Gpg.MediatR;

public class DecryptFileCommandHandler(IGpg gpg) : IRequestHandler<DecryptFileCommand>
{
	public async Task Handle(DecryptFileCommand request, CancellationToken cancellationToken)
	{
		await gpg.DecryptFileAsync(request.InputFileLocation, request.OutputFileLocation, request.PrivateKeyName, request.PrivateKeyPassword);
	}
}