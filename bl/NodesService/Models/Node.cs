namespace bl.NodesService.Models;

public class Node
{
	public int Id { get; set; }
	public int? ParentId { get; set; }
	public string Name { get; set; }
	public IEnumerable<Node> ChildNodes { get; set; }
}
