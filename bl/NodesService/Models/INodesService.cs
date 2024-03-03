namespace bl.NodesService.Models;

public interface INodesService
{
	public Task<Node> GetAsync(string treeName);
}