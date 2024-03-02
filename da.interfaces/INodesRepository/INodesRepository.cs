﻿namespace da.interfaces.INodesRepository;

public interface INodesRepository
{
	public Task<NodeReadDto[]> GetAsync();
	public Task<int> CreateAsync(NodeCreateDto dto);
	public Task UpdateAsync(NodeUpdateDto dto);
	public Task DeleteAsync(int id);
}