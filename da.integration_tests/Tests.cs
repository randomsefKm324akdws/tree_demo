using da_ef_model;
using da.interfaces.ILogRepository;
using da.interfaces.INodesRepository;
using da.interfaces.INodesRepository.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace da.integration_tests;

[TestClass]
public class Tests
{
	private readonly INodesRepository _nodesRepository;
	private readonly ILogRepository _logRepository;
	


	public Tests()
	{
		ServiceCollection services = new();
		services.AddTransient<AppDbContext, AppDbContext>();
		services.AddTransient<INodesRepository, NodesRepository>();
		services.AddTransient<ILogRepository, LogRepository>();

		var serviceProvider = services.BuildServiceProvider();

		_nodesRepository = serviceProvider.GetService<INodesRepository>();
		_logRepository = serviceProvider.GetService<ILogRepository>();
	}


	[TestMethod]
	public async Task NodeCreate()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId,
			TreeName = treeName
		});

		Assert.IsTrue(rootId > 0);
		Assert.IsTrue(createdId > 0);
	}
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeCreate_NameNull()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = null,
			ParentId = rootId,
			TreeName = treeName
		});

		Assert.IsTrue(rootId > 0);
		Assert.IsTrue(createdId > 0);
	}
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeCreate_NameLength()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = new string('a', 51),
			ParentId = rootId,
			TreeName = treeName
		});

		Assert.IsTrue(rootId > 0);
		Assert.IsTrue(createdId > 0);
	}

	
	[TestMethod]
	public async Task NodeUpdate()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId,
			TreeName = treeName
		});
		
		await _nodesRepository.UpdateAsync(new NodeUpdateDto()
		{
			Name = "newtest",
			Id = createdId,
			TreeName = treeName
		});
	}
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeUpdate_NameNull()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId,
			TreeName = treeName
		});
		
		await _nodesRepository.UpdateAsync(new NodeUpdateDto()
		{
			Name = null,
			Id = createdId,
			TreeName = treeName
		});
	}
	
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeUpdate_NameLength()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId,
			TreeName = treeName
		});
		
		await _nodesRepository.UpdateAsync(new NodeUpdateDto()
		{
			Name = new string('a', 51),
			Id = createdId,
			TreeName = treeName
		});
	}
	
	[TestMethod]
	public async Task NodeDelete()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var node1 = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId,
			TreeName = treeName
		});
		
		var node2= await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = node1,
			TreeName = treeName
		});
		
		var node2B= await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = node1,
			TreeName = treeName
		});

		await _nodesRepository.DeleteAsync(node2B);
		await _nodesRepository.DeleteAsync(node2);
		await _nodesRepository.DeleteAsync(node1);
	}


	[TestMethod]
	[ExpectedException(typeof(HaveToDeleteChildNodesFirstException))]
	public async Task NodeDelete_HaveToDeleteChildNodesFirstException()
	{
		var treeName = "test" + DateTime.Now;
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync(treeName);
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null,
				TreeName = treeName
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var node1 = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId,
			TreeName = treeName
		});
		
		var _= await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = node1,
			TreeName = treeName
		});
		
		await _nodesRepository.DeleteAsync(node1);
	}
	
	[TestMethod]
	[ExpectedException(typeof(CannotHave2RootNodesException))]
	public async Task NodeCreate_DontAllow2Roots()
	{
		var treeName = "test" + DateTime.Now;
		await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = null,
			TreeName = treeName
		});

		await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = null,
			TreeName = treeName
		});
	}


	[TestMethod]
	[ExpectedException(typeof(WrongParentIdException))]
	public async Task NodeCreate_WrongParentIdException()
	{
		var treeName = "test" + DateTime.Now;
		await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = -1,
			TreeName = treeName
		});
	}
	
	[TestMethod]
	public async Task Log()
	{
		var id1 = await _logRepository.LogExceptionAsync(new WrongParentIdException(), "test");
		var id2 = await _logRepository.LogExceptionAsync(new WrongParentIdException(), "test");
		
		Assert.AreNotEqual(id1, id2);
	}
}