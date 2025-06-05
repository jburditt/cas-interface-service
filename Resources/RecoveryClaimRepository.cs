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
            //var queryResults = _databaseContext.DFA_ProjectClaimSet
            //    .Join(_databaseContext.SystemUserSet, pc => pc.DFA_QualifiedReceiver.Id, su => su.Id, (pc, su) => new { ProjectClaim = pc, QualifiedReceiver = su })
            //    //.WhereIf(query.Id != null, x => x.ProjectClaim.Id == query.Id.Value)
            //    .WhereIf(query.CodingBlockSubmissionStatus != null, x => x.ProjectClaim.DFA_CodingBlockSubmissionStatus == (DFA_CodingBlockSubmissionStatus?)query.CodingBlockSubmissionStatus)
            //    .WhereIf(query.AfterInvoiceDate != null, x => x.ProjectClaim.DFA_InvoiceDate >= query.AfterInvoiceDate)
            //    .WhereIf(query.AfterDateGoodsReceived != null, x => x.ProjectClaim.DFA_DateGoodsAndServicesReceived >= query.AfterDateGoodsReceived)
            //    .WhereIf(query.AfterDateInvoiceReceived != null, x => x.ProjectClaim.DFA_ClaimReceivedDate >= query.AfterDateInvoiceReceived);
            var queryResults = (from pc in _databaseContext.DFA_ProjectClaimSet
                               join su in _databaseContext.SystemUserSet on pc.DFA_QualifiedReceiver.Id equals su.Id
                               join cc in _databaseContext.DFA_ClientCodeSet on pc.DFA_ClientCodeId.Id equals cc.Id
                               select new { ProjectClaim = pc, QualifiedReceiver = su, ClientCode = cc })
                               .ToList();

            var results = queryResults
                .Select(x => new ProjectClaimEntity(x.ProjectClaim, x.QualifiedReceiver, x.ClientCode))
                .ToList();

            var mappedResults = _mapper.Map<IEnumerable<RecoveryClaim>>(results);

            //if (results.Any())
            //{
            //    var clientCodes = _mapper.Map<IEnumerable<ClientCode>>(_databaseContext.DFA_ClientCodeSet);
            //    var projectExpenses = _mapper.Map<IEnumerable<ExpenseProject>>(_databaseContext.EMCR_ExpenseProjectSet);

            //    foreach (var rc in mappedResults)
            //    {
            //        if (rc.ClientCodeKey?.Id != null)
            //            rc.ClientCode = clientCodes.FirstOrDefault(cc => cc.Id == rc.ClientCodeKey?.Id);
            //        if (rc.ExpenseProject?.Id != null)
            //            rc.ExpenseProject = projectExpenses.FirstOrDefault(pe => pe.Id == rc.ExpenseProject?.Id);
            //    }
            //}

            return mappedResults;
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

public record ProjectClaimEntity(DFA_ProjectClaim ProjectClaim, SystemUser QualifiedReceiver, DFA_ClientCode ClientCode);

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
