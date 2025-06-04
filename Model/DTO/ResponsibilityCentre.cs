namespace Model;

public record ResponsibilityCentre
{
    [MaxLength(100)]
    public required string Code { get; set; }   // Dynamics Business Required emcr_code
}
