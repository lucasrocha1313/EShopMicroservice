namespace BuildingBlocks.Exceptions;

public class BadeRequestException: Exception
{
    public BadeRequestException(string message): base(message)
    {
        
    }
    
    public BadeRequestException(string message, string details): base(message)
    {
        Details = details;
    }
    
    public string? Details { get; set; }
}