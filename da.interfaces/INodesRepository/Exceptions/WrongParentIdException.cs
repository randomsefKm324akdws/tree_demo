namespace da.interfaces.INodesRepository.Exceptions;

public class WrongParentIdException : SecureException
{
	public override string Message => "Wrong parent id";
}