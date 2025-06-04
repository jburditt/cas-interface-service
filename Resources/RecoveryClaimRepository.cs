namespace Resources;

public interface IRecoveryClaimRepository : IQueryRepository<RecoveryClaimQuery, RecoveryClaim>, IBaseRepository<RecoveryClaim>
{

}

public class RecoveryClaimRepository : BaseRepository<DFA_ProjectClaim, RecoveryClaim>, IRecoveryClaimRepository
{
    private readonly DatabaseContext _databaseContext;

    public RecoveryClaimRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
    {
        _databaseContext = databaseContext;
    }

    public IEnumerable<RecoveryClaim> Query(RecoveryClaimQuery query)
    {
        if (query.IncludeChildren)
        {
            var queryResults = _databaseContext.DFA_ProjectClaimSet
                .Join(_databaseContext.SystemUserSet, pc => pc.DFA_QualifiedReceiver.Id, su => su.Id, (pc, su) => new { ProjectClaim = pc, QualifiedReceiver = su })
                //.Where(query)
                .WhereIf(query.CodingBlockSubmissionStatus != null, x => x.ProjectClaim.DFA_CodingBlockSubmissionStatus == (DFA_CodingBlockSubmissionStatus?)query.CodingBlockSubmissionStatus)
                .WhereIf(query.AfterInvoiceDate != null, x => x.ProjectClaim.DFA_InvoiceDate >= query.AfterInvoiceDate)
                .WhereIf(query.AfterDateGoodsReceived != null, x => x.ProjectClaim.DFA_DateGoodsAndServicesReceived >= query.AfterDateGoodsReceived)
                .WhereIf(query.AfterDateInvoiceReceived != null, x => x.ProjectClaim.DFA_ClaimReceivedDate >= query.AfterDateInvoiceReceived);
            //.Select(x => new { x.RecoveryClaim, x.ClientCode})
            //.ToList();
            var results = queryResults
                .Select(x => new ProjectClaimEntity(x.ProjectClaim, x.QualifiedReceiver))
                .ToList();

            //    .Select(x => new ProjectClaimEntity(x.ProjectClaim, x.ClientCode, x.ExpenseProject));
                //.GroupBy(rc => rc, cc => cc.ClientCode, (rc, cc) => new ProjectClaimEntity(rc, cc.Single()))
                //.GroupBy(rc => rc.ProjectClaim, ep => ep.ExpenseProject, (rc, cc) => new ProjectClaimEntity(rc, cc.Single(), ep.Single()));

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

public record ProjectClaimEntity(DFA_ProjectClaim ProjectClaim, SystemUser QualifiedReceiver);

public static class RecoveryClaimRepositoryExtensions
{
    public static IQueryable<DFA_ProjectClaim> Where(this IQueryable<DFA_ProjectClaim> results, RecoveryClaimQuery query)
    {
        return results
            .WhereIf(query.CodingBlockSubmissionStatus != null, x => x.DFA_CodingBlockSubmissionStatus == (DFA_CodingBlockSubmissionStatus?)query.CodingBlockSubmissionStatus)
            .WhereIf(query.AfterInvoiceDate != null, x => x.DFA_InvoiceDate >= query.AfterInvoiceDate)
            .WhereIf(query.AfterDateGoodsReceived != null, x => x.DFA_DateGoodsAndServicesReceived >= query.AfterDateGoodsReceived)
            .WhereIf(query.AfterDateInvoiceReceived != null, x => x.DFA_ClaimReceivedDate >= query.AfterDateInvoiceReceived);
    }
}
