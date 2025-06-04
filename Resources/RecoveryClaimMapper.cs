namespace Resources;

public class RecoveryClaimMapper : Profile
{
    public RecoveryClaimMapper()
    {
        CreateMap<DFA_ProjectClaim, RecoveryClaim>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DFA_ProjectClaimId))
            .ForMember(dest => dest.ClientCodeKey, opt => opt.MapFrom(src => src.DFA_ClientCodeId))
            .ForMember(dest => dest.ResponsibilityCentreKey, opt => opt.MapFrom(src => src.DFA_Resp))
            .ForMember(dest => dest.ServiceLineKey, opt => opt.MapFrom(src => src.DFA_ServiceLine))
            .ForMember(dest => dest.StobKey, opt => opt.MapFrom(src => src.DFA_Stob))
            .ForMember(dest => dest.ExpenseProjectKey, opt => opt.MapFrom(src => src.DFA_ProjectNumber))
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

        CreateMap<ProjectClaimEntity, RecoveryClaim>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProjectClaim.DFA_ProjectClaimId))
            .ForMember(dest => dest.ResponsibilityCentreKey, opt => opt.MapFrom(src => src.ProjectClaim.DFA_Resp))
            .ForMember(dest => dest.ServiceLineKey, opt => opt.MapFrom(src => src.ProjectClaim.DFA_ServiceLine))
            .ForMember(dest => dest.StobKey, opt => opt.MapFrom(src => src.ProjectClaim.DFA_Stob))
            .ForMember(dest => dest.ExpenseProjectKey, opt => opt.MapFrom(src => src.ProjectClaim.DFA_ProjectNumber))
            .ForMember(dest => dest.QualifiedReceiver, opt => opt.MapFrom(src => src.QualifiedReceiver))
            .ForMember(dest => dest.ClientCodeKey, opt => opt.MapFrom(src => src.ProjectClaim.DFA_ClientCodeId));


        CreateMap<EMCR_ResponsibilityCentre, ResponsibilityCentre>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EMCR_ResponsibilityCentreId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.EMCR_Code));

        CreateMap<EMCR_ExpenseProject, ExpenseProject>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EMCR_ExpenseProjectId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.EMCR_Code));

        CreateMap<DFA_ClientCode, ClientCode>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DFA_ClientCodeId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.DFA_Code));

        CreateMap<EMCR_ServiceLine, ServiceLine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EMCR_ServiceLineId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.EMCR_Code));

        CreateMap<EMCR_Stob, Stob>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EMCR_StobId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.EMCR_Code));

        CreateMap<SystemUser, User>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

    }
}
