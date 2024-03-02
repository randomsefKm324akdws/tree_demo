using System.Text.Json;
using da_ef_model;
using da.interfaces.ILogRepository;

namespace da;

public class LogRepository : ILogRepository
{
	public async Task<int> LogExceptionAsync(Exception ex, string queryAndBodyParams)
	{
		using (var context = new AppDbContext())
		{
			LogRecordDataText rec = new LogRecordDataText
			{
				ExMessage = ex.Message,
				ExStacktrace = ex.StackTrace,
				QueryAndBodyParams = queryAndBodyParams
			};
			var item = new Log
			{
				Type = ex.GetType().ToString(),
				Data = JsonSerializer.Serialize(rec)
			};
			await context.Logs.AddAsync(item);
			await context.SaveChangesAsync();

			return item.Id;
		}
	}
}

public class LogRecordDataText
{
	public string ExMessage { get; set; }
	public string ExStacktrace { get; set; }
	public string QueryAndBodyParams { get; set; }
}