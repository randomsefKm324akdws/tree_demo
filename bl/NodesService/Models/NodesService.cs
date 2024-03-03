using da.interfaces.INodesRepository;

namespace bl.NodesService.Models;

public class NodesService : INodesService
{
	private readonly INodesRepository _nodesRepository;

	public NodesService(INodesRepository nodesRepository)
	{
		_nodesRepository = nodesRepository;
	}

	public async Task<Node> GetAsync(string treeName)
	{
		var flatDtos = await _nodesRepository.GetAsync(treeName);

		var models = new List<Node>();
		foreach (var x in flatDtos)
		{
			models.Add(new Node
			{
				Id = x.Id,
				ParentId = x.ParentId,
				Name = x.Name,
				ChildNodes = new List<Node>()
			});
		}

		var nodes = models.ToLookup(d => d.ParentId);

		var rootNode = nodes[null].SingleOrDefault();
		if (rootNode != null)
		{
			FillChildNodes(rootNode, nodes);	
		}

		return rootNode;
	}

	private void FillChildNodes(Node node, ILookup<int?, Node> nodes)
	{
		var childNodes = nodes[node.Id];
		node.ChildNodes = childNodes;

		foreach (var childNode in node.ChildNodes)
		{
			FillChildNodes(childNode, nodes);
		}
	}
}