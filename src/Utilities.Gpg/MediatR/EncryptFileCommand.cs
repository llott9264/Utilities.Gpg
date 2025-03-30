using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
namespace Utilities.Gpg.MediatR;

public class EncryptFileCommand(string inputFileLocation, string outputFileLocation, string publicKeyName) : IRequest
{
	public string InputFileLocation { get; } = inputFileLocation;
	public string OutputFileLocation { get; } = outputFileLocation;
	public string PublicKeyName { get; } = publicKeyName;
}