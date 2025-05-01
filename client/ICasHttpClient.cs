namespace Client;

public interface ICasHttpClient
{
    void Initialize(Model.Settings.Client settings, bool isProduction);
    Task<HttpStatusCode> GetToken();
    Task<Response> Get(string url);
    Task<Response> Post(string url, string payload);
    Task<Response> CreateInvoice(Invoice invoices);
    Task<Response> GetInvoice(string invoiceNumber, string supplierNumber, string supplierSiteCode);
    Task<Response> GetPayment(string paymentNumber, string payGroup);

    Task<Response> GetSupplierByNumber(string supplierNumber);
    Task<Response> GetSupplierByNumberAndSiteCode(string supplierNumber, string supplierSiteCode);
    Task<Response> GetSupplierByLastNameAndSin(string lastName, string sin);
    Task<Response> FindSupplierByName(string supplierName);
    Task<Response> GetSupplierByBusinessNumber(string businessNumber);
}