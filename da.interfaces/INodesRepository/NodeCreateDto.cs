namespace da.interfaces.INodesRepository;

public class NodeCreateDto
{
	public string Name { get; set; }
	public int? ParentId { get; set; }
}