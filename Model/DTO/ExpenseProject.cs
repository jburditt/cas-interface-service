namespace Model;

public record ExpenseProject
{
    [MaxLength(100)]
    public required string Code { get; set; }   // Dynamics Business Required emcr_code
}
