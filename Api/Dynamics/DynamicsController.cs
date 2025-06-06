namespace Api;

[Route("api/[controller]")]
[ApiController]
public class DynamicsController(IRecoveryClaimService recoveryClaimService) : Controller
{
    [HttpGet("process-claims")]
    public async Task<IActionResult> ProcessClaims()
    {
        var claimResponses = await recoveryClaimService.ProcessClaims();
        return Ok(claimResponses);
    }
    
}
