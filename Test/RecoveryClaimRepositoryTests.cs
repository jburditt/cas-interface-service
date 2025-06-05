public class RecoveryClaimRepositoryTests(IRecoveryClaimRepository repository, ICasService casService, IMapper mapper)
{
    // WARNING these are not valid unit tests and depend on data in Dynamics not changing, use for local testing purposes only

    [Fact]
    public void Query()
    {
        var query = new RecoveryClaimQuery
        {
            IncludeChildren = true,
            Id = new Guid("1e06ea0a-7c91-ef11-b853-00505683fbf4"),
            CodingBlockSubmissionStatus = CodingBlockSubmissionStatus.PendingSubmission,
            //AfterInvoiceDate = new DateTime(2023, 1, 1),
            //AfterDateGoodsReceived = new DateTime(2023, 1, 1),
            //AfterDateInvoiceReceived = new DateTime(2023, 1, 1)
        };
        var recoveryClaims = repository.Query(query);

        // Assert
        Assert.NotNull(recoveryClaims);
    }

    [Fact]
    public async Task GetPending_Success()
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

        // Assert
        Assert.NotNull(recoveryClaims);
    }
}
