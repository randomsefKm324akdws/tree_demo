namespace da.interfaces.INodesRepository.Exceptions;

public class CannotHave2RootNodesException : SecureException
{
	public override string Message => "Cannot have 2 root nodes";
}