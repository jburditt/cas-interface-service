namespace Model;

public record ProjectClaimQuery //: IRequest<IEnumerable<ProjectClaim>>
{
    public CodingBlockSubmissionStatus? CodingBlockSubmissionStatus { get; set; }
    public DateTime? AfterInvoiceDate { get; set; }
    public DateTime? AfterDateGoodsReceived { get; set; }
    public DateTime? AfterDateInvoiceReceived { get; set; }
}

public record RecoveryClaim : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }

    public CodingBlockSubmissionStatus? CodingBlockSubmissionStatus { get; set; }   // Dynamics Optional dfa_codingblocksubmissionstatus
    public string? SupplierNumber { get; set; }  // Dynamics Optional dfa_suppliernumber
    [MaxLength(250)]
    public string? SupplierSiteNumber { get; set; } // Dynamics Optional dfa_site
    public DateTime? InvoiceDate { get; set; }  // Dynamics Optional dfa_invoicedate
    public string? InvoiceNumber { get; set; }  // Dynamics Optional dfa_invoicenumber
    public decimal? InvoiceAmount { get; set; } // Dynamics Optional dfa_casinvoiceamount
    public PayGroup? PayGroup { get; set; }     // Dynamics Optional dfa_paygroup
    public DateTime? DateGoodsReceived { get; set; } // Dynamics Optional dfa_dategoodsandservicesreceived
    public DateTime? DateInvoiceReceived { get; set; }  // Dynamics Optional dfa_claimreceiveddate
    public StaticReference? QualifiedReceiver { get; set; } // Dynamics Optional dfa_qualifiedreceiver, relationship dfa_systemuser
    [MaxLength(40)]
    public string? PaymentAdviceComments { get; set; }   // Dynamics Optional dfa_paymentadvicecomments
    public ClientCode? ClientCode { get; set; } // Dynamics Optional dfa_clientcodeid
    public ResponsibilityCentre? ResponsibilityCentre { get; set; } // Dynamics Optional dfa_resp
    public StaticReference? ResponsibilityCentreKey { get; set; } // Dynamics Optional dfa_resp
    public ServiceLine? ServiceLine { get; set; } // Dynamics Optional dfa_serviceline -> dfa_emcr_serviceline
    public Stob? Stob { get; set; } // Dynamics Optional dfa_stob -> dfa_emcr_stob
    public ExpenseProject? ExpenseProject { get; set; } // Dynamics Optional dfa_expenseproject -> dfa_projectnumber
}
