namespace api.Models;

public class ExceptionApi
{
	public string Type { get; set; }
	public string Id { get; set; }
	public ExceptionApiData Data { get; set; }
	
	
}

public class ExceptionApiData
{
	public string Message { get; set; }
}