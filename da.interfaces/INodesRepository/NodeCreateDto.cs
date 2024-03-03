namespace da.interfaces.INodesRepository;

public class NodeCreateDto
{
	public string Name { get; set; }
	public int? ParentId { get; set; }
	public string TreeName { get; set; }
}