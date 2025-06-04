namespace Resources;

public class RecoveryClaimMapper : Profile
{
    public RecoveryClaimMapper()
    {
        CreateMap<DFA_ProjectClaim, RecoveryClaim>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DFA_ProjectClaimId))
            .ForMember(dest => dest.ClientCode, opt => opt.MapFrom(src => src.DFA_ClientCodeId))
            .ForMember(dest => dest.ResponsibilityCentre, opt => opt.MapFrom(src => src.DFA_RESP))
            .ForMember(dest => dest.ServiceLine, opt => opt.MapFrom(src => src.DFA_ServiceLine))
            .ForMember(dest => dest.STOB, opt => opt.MapFrom(src => src.DFA_STOB))
            .ForMember(dest => dest.ExpenseProject, opt => opt.MapFrom(src => src.DFA_ProjectNumber))
            .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
            .ForMember(dest => dest.CodingBlockSubmissionStatus, opt => opt.MapFrom(src => src.DFA_CodingBlockSubmissionStatus))
            .ForMember(dest => dest.SupplierNumber, opt => opt.MapFrom(src => src.DFA_SupplierNumber))
            .ForMember(dest => dest.SupplierSiteNumber, opt => opt.MapFrom(src => src.DFA_Site))
            .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.DFA_InvoiceDate))
            .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.DFA_InvoiceNumber))
            .ForMember(dest => dest.InvoiceAmount, opt => opt.MapFrom(src => src.DFA_CasInvoiceAmount))
            .ForMember(dest => dest.PayGroup, opt => opt.MapFrom(src => src.DFA_PayGroupType))
            .ForMember(dest => dest.DateGoodsReceived, opt => opt.MapFrom(src => src.DFA_DateGoodsAndServicesReceived))
            .ForMember(dest => dest.DateInvoiceReceived, opt => opt.MapFrom(src => src.DFA_ClaimReceivedDate))
            .ForMember(dest => dest.QualifiedReceiver, opt => opt.MapFrom(src => src.DFA_QualifiedReceiver))
            .ForMember(dest => dest.PaymentAdviceComments, opt => opt.MapFrom(src => src.DFA_PaymentAdviceComments));

        //CreateMap<EMCR_R
    }
}
