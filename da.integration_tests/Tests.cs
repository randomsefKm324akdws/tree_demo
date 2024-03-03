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
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId
		});

		Assert.IsTrue(rootId > 0);
		Assert.IsTrue(createdId > 0);
	}
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeCreate_NameNull()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = null,
			ParentId = rootId
		});

		Assert.IsTrue(rootId > 0);
		Assert.IsTrue(createdId > 0);
	}
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeCreate_NameLength()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = new string('a', 51),
			ParentId = rootId
		});

		Assert.IsTrue(rootId > 0);
		Assert.IsTrue(createdId > 0);
	}

	
	[TestMethod]
	public async Task NodeUpdate()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId
		});
		
		await _nodesRepository.UpdateAsync(new NodeUpdateDto()
		{
			Name = "newtest",
			Id = createdId
		});
	}
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeUpdate_NameNull()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId
		});
		
		await _nodesRepository.UpdateAsync(new NodeUpdateDto()
		{
			Name = null,
			Id = createdId
		});
	}
	
	
	[TestMethod]
	[ExpectedException(typeof(WrongNameLengthException))]
	public async Task NodeUpdate_NameLength()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var createdId = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId
		});
		
		await _nodesRepository.UpdateAsync(new NodeUpdateDto()
		{
			Name = new string('a', 51),
			Id = createdId
		});
	}
	
	[TestMethod]
	public async Task NodeDelete()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var node1 = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId
		});
		
		var node2= await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = node1
		});
		
		var node2B= await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = node1
		});

		await _nodesRepository.DeleteAsync(node2B);
		await _nodesRepository.DeleteAsync(node2);
		await _nodesRepository.DeleteAsync(node1);
	}


	[TestMethod]
	[ExpectedException(typeof(HaveToDeleteChildNodesFirstException))]
	public async Task NodeDelete_HaveToDeleteChildNodesFirstException()
	{
		NodeReadDto[] existingRecords = await _nodesRepository.GetAsync();
		
		int? rootId;
		if (existingRecords.Length == 0)
		{
			rootId = await _nodesRepository.CreateAsync(new NodeCreateDto
			{
				Name = "test",
				ParentId = null
			});			
		}
		else
		{
			rootId = existingRecords.Single(x => x.ParentId == null).Id;
		}

		var node1 = await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = rootId
		});
		
		var _= await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = node1
		});
		
		await _nodesRepository.DeleteAsync(node1);
	}
	
	[TestMethod]
	[ExpectedException(typeof(CannotHave2RootNodesException))]
	public async Task NodeCreate_DontAllow2Roots()
	{
		await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = null
		});

		await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = null
		});
	}


	[TestMethod]
	[ExpectedException(typeof(WrongParentIdException))]
	public async Task NodeCreate_WrongParentIdException()
	{
		await _nodesRepository.CreateAsync(new NodeCreateDto
		{
			Name = "test",
			ParentId = -1
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