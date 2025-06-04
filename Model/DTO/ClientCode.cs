namespace Model;

public record ClientCode
{
    [MaxLength(100)]
    public required string Code { get; set; }   // Dynamics Business Required dfa_code
}
