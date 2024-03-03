namespace da.interfaces.INodesRepository.Exceptions;

public class WrongNameLengthException : SecureException
{
	public override string Message => "Wrong name length";
}