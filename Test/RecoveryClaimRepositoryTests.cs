

public class RecoveryClaimRepositoryTests(IRecoveryClaimRepository repository)
{
    // WARNING these are not valid unit tests and depend on data in Dynamics not changing, use for local testing purposes only

    [Fact]
    public void Query()
    {
        var query = new RecoveryClaimQuery
        {
            IncludeChildren = true,
            Id = new Guid("1e06ea0a-7c91-ef11-b853-00505683fbf4"),
            CodingBlockSubmissionStatus = CodingBlockSubmissionStatus.Submitted,
            //AfterInvoiceDate = new DateTime(2023, 1, 1),
            //AfterDateGoodsReceived = new DateTime(2023, 1, 1),
            //AfterDateInvoiceReceived = new DateTime(2023, 1, 1)
        };
        var recoveryClaims = repository.Query(query);

        // Assert
        Assert.NotNull(recoveryClaims);
    }

    [Fact]
    public void GetPending_Success()
    {
        var recoveryClaims = repository.GetPending();

        // Assert
        Assert.NotNull(recoveryClaims);
    }
}
