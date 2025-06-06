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
        var responses = await recoveryClaimService.ProcessClaims();

        // Assert
        Assert.NotNull(responses);
            }

    [Fact]
    public void UpdateCodingBlockSubmissionStatus_Success()
            {
        // Arrange
        var id = new Guid("3cc1c64d-3330-f011-b853-005056838fcd");
        var codingBlockSubmissionStatus = CodingBlockSubmissionStatus.Failed;

        // Act
        repository.UpdateCodingBlockSubmissionStatus(id, codingBlockSubmissionStatus);

        // Assert
        var updatedClaim = repository
            .Query(new RecoveryClaimQuery { Id = id })
            .FirstOrDefault();
        Assert.NotNull(updatedClaim);
        Assert.Equal(codingBlockSubmissionStatus, updatedClaim.CodingBlockSubmissionStatus);
    }
}
