namespace da.interfaces.INodesRepository.Exceptions;

public class NoSuchNodeException : SecureException
{
	public override string Message => "No such node exists";
}