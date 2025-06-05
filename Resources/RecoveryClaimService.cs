namespace Resources;

public interface IRecoveryClaimService
{
    Task ProcessClaims();
}

public class RecoveryClaimService(IRecoveryClaimRepository repository, IMapper mapper, ICasService casService) : IRecoveryClaimService
{
    public async Task ProcessClaims()
    {
        var recoveryClaims = repository.GetPending();

        foreach (var recoveryClaim in recoveryClaims)
        {
            var invoice = mapper.Map<Invoice>(recoveryClaim);
            var response = await casService.CreateInvoice(invoice);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                repository.UpdateCodingBlockSubmissionStatus(recoveryClaim.Id, CodingBlockSubmissionStatus.Submitted);
            }
            else
            {
                repository.UpdateCodingBlockSubmissionStatus(recoveryClaim.Id, CodingBlockSubmissionStatus.Failed);
            }
        }
    }
}
