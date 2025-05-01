namespace Api;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : Controller
{
    private readonly ICasHttpClient _casHttpClient;

    public SupplierController(AppSettings appSettings, ICasHttpClient casHttpClient)
    {
        _casHttpClient = casHttpClient;

        if (string.IsNullOrEmpty(appSettings.Client?.Id) || string.IsNullOrEmpty(appSettings.Client.Secret) || string.IsNullOrEmpty(appSettings.Client.BaseUrl) || string.IsNullOrEmpty(appSettings.Client.TokenUrl))
        {
            throw new ArgumentNullException("Client is not configured. Check your user secrets, appsettings, and environment variables.");
        }

        _casHttpClient.Initialize(appSettings.Client);
    }

    [HttpGet("{supplierNumber}")]
    public async Task<IActionResult> GetBySupplierNumber([FromRoute] string supplierNumber)
    {
        (var result, var statusCode) = await _casHttpClient.GetSupplierByNumber(supplierNumber);

        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }

    [HttpGet("{supplierNumber}/site/{supplierSiteCode}")]
    public async Task<IActionResult> GetBySupplierNumberAndSiteCode([FromRoute] string supplierNumber, [FromRoute] string supplierSiteCode)
    {
        (var result, var statusCode) = await _casHttpClient.GetSupplierByNumberAndSiteCode(supplierNumber, supplierSiteCode);

        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }

    [HttpGet("suppliersearch/{supplierName}")]
    public async Task<IActionResult> GetBySupplierName([FromRoute] string supplierName)
    {
        (var result, var statusCode) = await _casHttpClient.FindSupplierByName(supplierName);

        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }

    [HttpGet("{lastName}/lastname/{sin}/sin")]
    public async Task<IActionResult> GetBySupplierLastNameAndSin([FromRoute] string lastName, [FromRoute] string sin)
    {
        (var result, var statusCode) = await _casHttpClient.GetSupplierByLastNameAndSin(lastName, sin);

        return StatusCode((int)statusCode, new JsonResult(result).Value);
    }
}

