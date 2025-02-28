using MediatR;

namespace Utilities.Gpg.MediatR;

public class DecryptFileCommand(string inputFileLocation, string outputFileLocation, string privateKeyName, string privateKeyPassword) : IRequest
{
	public string InputFileLocation { get; } = inputFileLocation;
	public string OutputFileLocation { get; } = outputFileLocation;
	public string PrivateKeyName { get; } = privateKeyName;
	public string PrivateKeyPassword { get; } = privateKeyPassword;
}