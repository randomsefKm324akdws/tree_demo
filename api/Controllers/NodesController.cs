using System.Net;
using System.Text.Json;
using api.Models;
using bl.NodesService.Models;
using da.interfaces.ILogRepository;
using da.interfaces.INodesRepository;
using da.interfaces.INodesRepository.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class NodesController : ControllerBase
{
	private readonly INodesService _nodesService;
	private readonly ILogRepository _logRepository;
	private readonly INodesRepository _repository;

	public NodesController(INodesRepository repository, INodesService nodesService, ILogRepository logRepository)
	{
		_repository = repository;
		_nodesService = nodesService;
		_logRepository = logRepository;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		try
		{
			var data = await _nodesService.GetAsync();
			var response = StatusCode((int)HttpStatusCode.OK, data);
			return response;
		}
		catch (Exception ex)
		{
			return await GetExceptionResponse(ex, null);
		}
	}


	[HttpPost]
	public async Task<IActionResult> Post([FromBody]NodeCreateApi item)
	{
		try
		{
			var id = await _repository.CreateAsync(new NodeCreateDto
			{
				Name = item.Name,
				ParentId = item.ParentId
			});
			var response = StatusCode((int)HttpStatusCode.OK, id);
			return response;
		}
		catch (Exception ex)
		{
			return await GetExceptionResponse(ex, item);
		}
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromBody]NodeUpdateApi item)
	{
		try
		{
			await _repository.UpdateAsync(new NodeUpdateDto
			{
				Id = item.Id,
				Name = item.Name
			});
			var response = StatusCode((int)HttpStatusCode.OK, true);
			return response;
		}
		catch (Exception ex)
		{
			return await GetExceptionResponse(ex, item);
		}
	}

	
	[HttpDelete]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			await _repository.DeleteAsync(id);
			var response = StatusCode((int)HttpStatusCode.OK, true);
			return response;
		}
		catch (Exception ex)
		{
			return await GetExceptionResponse(ex, id);
		}
	}
	
	private async Task<ObjectResult> GetExceptionResponse(Exception ex, object data)
	{
		var id = await _logRepository.LogExceptionAsync(ex, JsonSerializer.Serialize(data));
		ObjectResult resp =  StatusCode((int)HttpStatusCode.InternalServerError, new ExceptionApi
		{
			Type = ex is SecureException ? "Secure" : "Exception",
			Id = Convert.ToString(id),
			Data = new ExceptionApiData
			{
				Message = ex is SecureException ? ex.Message : $"Internal server error ID = {id}", 
			}
		});

		return resp;
	}
}