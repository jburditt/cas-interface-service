namespace Model;

public record STOB
{
    [MaxLength(100)]
    public string? Code { get; set; }   // Dynamics Optional emcr_code
}
