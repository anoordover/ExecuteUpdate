namespace ExecuteUpdateDemo.Data;

public class Credit
{
    public long Id { get; set; }

    public string Reference { get; set; } = string.Empty;

    public string ReferenceDeclaration { get; set; } = string.Empty;
    public long? DeclarationId { get; set; }
    
}

public class Period
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}