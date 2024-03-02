namespace da.interfaces.ILogRepository;

public interface ILogRepository
{
	public Task<int> LogExceptionAsync(Exception ex, string queryAndBodyParams);
}