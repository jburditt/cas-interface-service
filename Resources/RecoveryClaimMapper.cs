namespace Resources;

public class RecoveryClaimMapper : Profile
{
    public RecoveryClaimMapper()
    {
        CreateMap<DFA_ProjectClaim, RecoveryClaim>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DFA_ProjectClaimId))
            .ForMember(dest => dest.ClientCode, opt => opt.MapFrom(src => src.DFA_ClientCodeId))
            .ForMember(dest => dest.ResponsibilityCentre, opt => opt.MapFrom(src => src.DFA_ResP))
            ;

    }
}
