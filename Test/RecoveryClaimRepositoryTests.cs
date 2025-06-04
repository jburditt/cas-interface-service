

public class RecoveryClaimRepositoryTests(IRecoveryClaimRepository repository)
{
    //
    [Fact]
    public void Query()
    {
        var query = new RecoveryClaimQuery
        {
            IncludeChildren = true,
            CodingBlockSubmissionStatus = CodingBlockSubmissionStatus.Submitted,
            AfterInvoiceDate = new DateTime(2023, 1, 1),
            AfterDateGoodsReceived = new DateTime(2023, 1, 1),
            AfterDateInvoiceReceived = new DateTime(2023, 1, 1)
        };
        var recoveryClaims = repository.Query(query);

        // Assert
        Assert.NotNull(recoveryClaims);
    }
}
