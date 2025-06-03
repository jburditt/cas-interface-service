namespace Model;

public record ProjectClaimQuery //: IRequest<IEnumerable<ProjectClaim>>
{
    public CodingBlockSubmissionStatus CodingBlockSubmissionStatus { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DateGoodsReceived { get; set; }
    public DateTime DateInvoiceReceived { get; set; }
}

public record RecoveryClaim : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public CodingBlockSubmissionStatus? CodingBlockSubmissionStatus { get; set; }   // Dynamics Optional dfa_codingblocksubmissionstatus
    public string SupplierNumber { get; set; }
    public string SupplierSiteNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }  // Dynamics Optional dfa_invoicedate
    public string InvoiceNumber { get; set; }
    public decimal InvoiceAmount { get; set; }
    //public PayGroup PayGroup { get; set; }

    public DateTime? DateGoodsReceived { get; set; } // Dynamics Optional dfa_dategoodsandservicesreceived
    public DateTime? DateInvoiceReceived { get; set; }  // Dynamics Optional dfa_claimreceiveddate
}
