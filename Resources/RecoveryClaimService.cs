namespace Resources;

public interface IRecoveryClaimService
{
    Task<IEnumerable<IdResponse>> ProcessClaims();
}

public class RecoveryClaimService(IRecoveryClaimRepository repository, IMapper mapper, ICasService casService, ILogger<IRecoveryClaimService> logger) : IRecoveryClaimService
{
    public async Task<IEnumerable<IdResponse>> ProcessClaims()
    {
        try
        {
            var claimResponses = new List<IdResponse>();
            var recoveryClaims = repository.GetPending();

            foreach (var recoveryClaim in recoveryClaims)
            {
                var invoice = mapper.Map<Invoice>(recoveryClaim);
                var createInvoiceResponse = await casService.CreateInvoice(invoice);
                var response = new IdResponse(recoveryClaim.Id, createInvoiceResponse.Content, createInvoiceResponse.StatusCode);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    repository.UpdateCodingBlockSubmissionStatus(recoveryClaim.Id, CodingBlockSubmissionStatus.Submitted);
                }
                else
                {
                    repository.UpdateFailure(recoveryClaim.Id, response.Content);
                }
                claimResponses.Add(response);
            }

            logger.LogInformation("The following recovery claims were processed and sent to CAS to generate invoices:\r\n{0}", System.Text.Json.JsonSerializer.Serialize(claimResponses));
            return claimResponses;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing claims.");
            throw;
        }
    }
}

public record IdResponse(Guid Id, string Content, HttpStatusCode StatusCode) : Response(Content, StatusCode) { }