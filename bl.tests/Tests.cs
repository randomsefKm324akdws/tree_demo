using bl.NodesService.Models;
using da.interfaces.INodesRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace bl.tests;

[TestClass]
public class Tests
{
	[TestMethod]
	public async Task Test1()
	{
		//arrange
		var mockRepo = new Mock<INodesRepository>();

		var mockNodes = new List<NodeReadDto>();
		mockNodes.Add(new NodeReadDto
		{
			Id = 1,
			Name = "1",
			ParentId = null
		});
		mockNodes.Add(new NodeReadDto
		{
			Id = 2,
			Name = "2",
			ParentId = 1
		});
		mockNodes.Add(new NodeReadDto
		{
			Id = 3,
			Name = "3",
			ParentId = 1
		});
		mockNodes.Add(new NodeReadDto
		{
			Id = 4,
			Name = "4",
			ParentId = 2
		});


		mockRepo
			.Setup(x => x.GetAsync())
			.ReturnsAsync(mockNodes.ToArray());


		//act
		INodesService service = new NodesService.Models.NodesService(mockRepo.Object);
		var rootNode = await service.GetAsync();

		//assert
		Assert.IsNotNull(rootNode);
		Assert.AreEqual(2, rootNode.ChildNodes.Count());
	}
}