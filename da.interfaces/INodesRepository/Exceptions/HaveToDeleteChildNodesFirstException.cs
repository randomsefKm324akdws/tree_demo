namespace da.interfaces.INodesRepository.Exceptions;

public class HaveToDeleteChildNodesFirstException : SecureException
{
	public override string Message => "You have to delete all children nodes first";
}