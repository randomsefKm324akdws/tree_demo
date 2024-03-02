namespace da.interfaces.INodesRepository;

public class NodeReadDto
{
	public int Id { get; set; }
	public int? ParentId { get; set; }
	public string Name { get; set; }
}