namespace Resources;

public interface IRecoveryClaimRepository : IQueryRepository<ProjectClaimQuery, RecoveryClaim>, IBaseRepository<RecoveryClaim>
{

}

public class RecoveryClaimRepository : BaseRepository<DFA_ProjectClaim, RecoveryClaim>, IRecoveryClaimRepository
{
    private readonly DatabaseContext _databaseContext;

    public RecoveryClaimRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
    {
        _databaseContext = databaseContext;
    }

    public IEnumerable<RecoveryClaim> Query(ProjectClaimQuery query)
    {
        if (query.IncludeChildren)
        {
            var queryResults = _databaseContext.DFA_ProjectClaimSet
                .Join(_databaseContext.DFA_ClientCodeSet, rc => rc.DFA_ClientCodeId.Id, cc => cc.Id, (rc, cc) => new { RecoveryClaim = rc, ClientCode = cc })
                //.Where(query)
                .WhereIf(query.CodingBlockSubmissionStatus != null, x => x.RecoveryClaim.DFA_CodingBlockSubmissionStatus == (DFA_CodingBlockSubmissionStatus?)query.CodingBlockSubmissionStatus)
                .WhereIf(query.AfterInvoiceDate != null, x => x.RecoveryClaim.DFA_InvoiceDate >= query.AfterInvoiceDate)
                .WhereIf(query.AfterDateGoodsReceived != null, x => x.RecoveryClaim.DFA_DateGoodsAndServicesReceived >= query.AfterDateGoodsReceived)
                .WhereIf(query.AfterDateInvoiceReceived != null, x => x.RecoveryClaim.DFA_ClaimReceivedDate >= query.AfterDateInvoiceReceived);
                //.Select(x => new { x.RecoveryClaim, x.ClientCode})
                //.ToList();
            var results = queryResults
                .ToList()
                .GroupBy(rc => rc.RecoveryClaim, cc => cc.ClientCode, (rc, cc) => new ProjectClaimEntity(rc, cc.Single()));

            return _mapper.Map<IEnumerable<RecoveryClaim>>(results);
        }
        else
        {
            var queryResults = _databaseContext.DFA_ProjectClaimSet
                .Where(query)
                .ToList();
            return _mapper.Map<IEnumerable<RecoveryClaim>>(queryResults);
        }
    }
}

public record ProjectClaimEntity(DFA_ProjectClaim ProjectClaim, DFA_ClientCode ClientCode);

public static class RecoveryClaimRepositoryExtensions
{
    public static IQueryable<DFA_ProjectClaim> Where(this IQueryable<DFA_ProjectClaim> results, ProjectClaimQuery query)
    {
        return results
            .WhereIf(query.CodingBlockSubmissionStatus != null, x => x.DFA_CodingBlockSubmissionStatus == (DFA_CodingBlockSubmissionStatus?)query.CodingBlockSubmissionStatus)
            .WhereIf(query.AfterInvoiceDate != null, x => x.DFA_InvoiceDate >= query.AfterInvoiceDate)
            .WhereIf(query.AfterDateGoodsReceived != null, x => x.DFA_DateGoodsAndServicesReceived >= query.AfterDateGoodsReceived)
            .WhereIf(query.AfterDateInvoiceReceived != null, x => x.DFA_ClaimReceivedDate >= query.AfterDateInvoiceReceived);
    }
}
