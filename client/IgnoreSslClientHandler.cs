namespace Client;

public class IgnoreSslClientHandler : HttpClientHandler
{
    public IgnoreSslClientHandler()
    {
        ServerCertificateCustomValidationCallback = DangerousAcceptAnyServerCertificateValidator;
    }
}