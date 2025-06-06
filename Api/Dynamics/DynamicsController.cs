namespace Api;

[Route("api/[controller]")]
[ApiController]
public class DynamicsController(IRecoveryClaimService recoveryClaimService, IBackgroundTaskQueue taskQueue) : Controller
{
    [HttpGet("process-claims")]
    public IActionResult ProcessClaims()
    {
        _ = taskQueue.QueueBackgroundWorkItemAsync(async job =>
        {
            await recoveryClaimService.ProcessClaims();
        });
        return Ok();
    }
}
