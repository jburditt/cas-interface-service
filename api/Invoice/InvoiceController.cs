namespace Api;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : Controller
{
    private readonly ICasHttpClient _casHttpClient;

    public InvoiceController(AppSettings appSettings, ICasHttpClient casHttpClient)
    {
        _casHttpClient = casHttpClient;

        if (string.IsNullOrEmpty(appSettings.Client?.Id) || string.IsNullOrEmpty(appSettings.Client.Secret) || string.IsNullOrEmpty(appSettings.Client.BaseUrl) || string.IsNullOrEmpty(appSettings.Client.TokenUrl))
        {
            throw new ArgumentNullException("Client is not configured. Check your user secrets, appsettings, and environment variables.");
        }

        _casHttpClient.Initialize(appSettings.Client, appSettings.IsProduction);
    }

    [HttpPost]
    public async Task<IActionResult> Generate([FromBody] Invoice invoice)
    {
        (var result, var statusCode) = await _casHttpClient.CreateInvoice(invoice);
        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }

    [HttpGet("{invoiceNumber}/{supplierNumber}/{supplierSiteCode}")]
    public async Task<IActionResult> Search(string invoiceNumber, string supplierNumber, string supplierSiteCode)
    {
        (var result, var statusCode) = await _casHttpClient.GetInvoice(invoiceNumber, supplierNumber, supplierSiteCode);
        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }

    [HttpGet("payment/{paymentNumber}/{payGroup}")]
    public async Task<IActionResult> GetPayment(string paymentNumber, string payGroup)
    {
        (var result, var statusCode) = await _casHttpClient.GetPayment(paymentNumber, payGroup);
        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }
}
