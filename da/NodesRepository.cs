using da_ef_model;
using da.interfaces.INodesRepository;
using da.interfaces.INodesRepository.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace da;

public class NodesRepository : INodesRepository
{
	private readonly AppDbContext _context;

	public NodesRepository(AppDbContext appDbContext)
	{
		_context = appDbContext;
	}

	public async Task<NodeReadDto[]> GetAsync()
	{
		NodeReadDto[] res = await _context.Nodes
			.Select(x => new NodeReadDto
			{
				Id = x.Id,
				ParentId = x.ParentId,
				Name = x.Name
			}).ToArrayAsync();
		return res;
	}

	public async Task<int> CreateAsync(NodeCreateDto dto)
	{
		try
		{
			Node item = new Node
			{
				Name = dto.Name,
				ParentId = dto.ParentId
			};
			await _context.Nodes.AddAsync(item);
			await _context.SaveChangesAsync();

			return item.Id;
		}
		catch (DbUpdateException ex)
		{
			if (ex.InnerException != null && ex.InnerException.Message.Contains("Nodes_ParentId_Null_Index"))
			{
				throw new CannotHave2RootNodesException();
			}			
			
			if (ex.InnerException != null && ex.InnerException.Message.Contains("FK_Nodes_Nodes_ParentId"))
			{
				throw new WrongParentIdException();
			}			
			
			if (ex.InnerException != null && ex.InnerException.Message.Contains("Cannot insert the value NULL"))
			{
				throw new WrongNameLengthException();
			}			
			
			if (ex.InnerException != null && ex.InnerException.Message.Contains("String or binary data would be truncated"))
			{
				throw new WrongNameLengthException();
			}

			throw;
		}
	}

	public async Task UpdateAsync(NodeUpdateDto dto)
	{
		try
		{
			var item = await _context.Nodes.Where(x => x.Id == dto.Id).SingleOrDefaultAsync();
			if (item == null)
			{
				throw new NoSuchNodeException();
			}
		
			item.Name = dto.Name;
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateException ex)
		{
			if (ex.InnerException != null && ex.InnerException.Message.Contains("Cannot insert the value NULL"))
			{
				throw new WrongNameLengthException();
			}

			if (ex.InnerException != null && ex.InnerException.Message.Contains("String or binary data would be truncated"))
			{
				throw new WrongNameLengthException();
			}
			
			throw;
		}
	}

	public async Task DeleteAsync(int id)
	{
		try
		{
			var node = await _context.Nodes.Where(x => x.Id == id).SingleOrDefaultAsync();
			if (node == null)
			{
				throw new NoSuchNodeException();
			}
			
			var childNodesCount = await _context.Nodes.Where(x => x.ParentId == id).CountAsync();
			if (childNodesCount > 0)
			{
				throw new HaveToDeleteChildNodesFirstException();
			}
			
			_context.Remove(node);
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateException ex)
		{
			if (ex.InnerException != null && ex.InnerException.Message.Contains("Nodes_ParentId_Null_Index"))
			{
				throw new HaveToDeleteChildNodesFirstException();
			}
			
			if (ex.InnerException != null && ex.InnerException.Message.Contains("FK_Nodes_Nodes_ParentId"))
			{
				throw new HaveToDeleteChildNodesFirstException();
			}

			throw;
		}
	}
}