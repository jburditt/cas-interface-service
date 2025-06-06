﻿public class InvoiceTests(ICasService casService, AppSettings appSettings)
{
    // WARNING these are not valid unit tests, they depend on existing data in the CAS system and will fail if the data is changed or removed

    [Fact]
    public async Task Send_Invoices_Cas_Transaction_Succeed()
    {
        var invoices = new Invoice();
        invoices.IsBlockSupplier = true;
        invoices.InvoiceType = "Standard";
        invoices.SupplierNumber = "2002741";
        invoices.SupplierSiteNumber = 1;
        invoices.InvoiceDate = DateTime.Now;
        invoices.InvoiceNumber = "INV-2025-026102";
        invoices.InvoiceAmount = 284.00m;
        invoices.PayGroup = "GEN CHQ";
        invoices.DateInvoiceReceived = DateTime.Now;
        invoices.RemittanceCode = "01";
        invoices.SpecialHandling = false;
        invoices.NameLine1 = "Ida Albert";
        invoices.AddressLine1 = "2671 Champions Lounge";
        invoices.AddressLine2 = "30";
        invoices.AddressLine3 = "Galaxy Studios";
        invoices.City = "Chilliwack";
        invoices.Country = "CA";
        invoices.Province = "BC";
        invoices.PostalCode = "V4R9M0";
        invoices.QualifiedReceiver = "systemuser";
        invoices.Terms = "Immediate";
        invoices.PayAloneFlag = "Y";
        invoices.PaymentAdviceComments = "";
        invoices.RemittanceMessage1 = "21-03304-VIC-Albert, Ida";
        invoices.RemittanceMessage2 = "Income Support-Lost Earning Capacity-Minimum Wage";
        invoices.RemittanceMessage3 = "Crime Victim Assistance Program";
        invoices.GLDate = DateTime.Now;
        invoices.InvoiceBatchName = "SNBATCH";
        invoices.CurrencyCode = "CAD";
        invoices.InvoiceLineDetails = new List<InvoiceLineDetail>
        {
            new InvoiceLineDetail
            {
                InvoiceLineNumber = 1,
                InvoiceLineType = "Item",
                LineCode = "DR",
                InvoiceLineAmount = 284.00m,
                // vsd_programtyp columns vsd_clientcode.vsd_responsibilitycentre, vsd_serviceline, vsd_stob, vsd_projectcode
                DefaultDistributionAccount = "010.15004.10250.5298.1500000.000000.0000",
            }
        };

        var response = await casService.CreateInvoice(invoices);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // successful from Postman DEV
        //{ "invoice_number":"INV-2025-026103","CAS-Returned-Messages":"SUCCEEDED"}

        // successful JSON from cas-interface-service logs from Victim Services DEV
        //04/10/2025 08:30:26 JSON: {"operatingUnit":null,"invoiceType":"Standard","poNumber":null,"supplierName":null,"supplierNumber":"2000428","supplierSiteNumber":"001","invoiceDate":"09-Apr-2025","invoiceNumber":"INV-2025-087279","invoiceAmount":1000.00,"payGroup":"GEN CHQ","dateInvoiceReceived":"09-Apr-2025","dateGoodsReceived":"","remittanceCode":"01","specialHandling":"N","bankNumber":null,"branchNumber":null,"accountNumber":null,"eftAdviceFlag":null,"eftEmailAddress":null,"nameLine1":"Amanda Khalid","nameLine2":"","addressLine1":"16 High Street","addressLine2":"","addressLine3":"","city":"Coquitlam","country":"CA","province":"BC","postalCode":"V8V6V5","qualifiedReceiver":"CVAP Service Account","terms":"Immediate","payAloneFlag":"Y","paymentAdviceComments":"","remittanceMessage1":"21-22290-VIC-Khalid, Amanda","remittanceMessage2":"Wage Loss-Wage Loss","remittanceMessage3":"Crime Victim Assistance Program","termsDate":null,"glDate":"10-Apr-2025","invoiceBatchName":"SNBATCH","currencyCode":"CAD","invoiceLineDe...
        //04/10/2025 08:30:48 JSON: {"operatingUnit":null,"invoiceType":"Standard","poNumber":null,"supplierName":null,"supplierNumber":"2000428","supplierSiteNumber":"001","invoiceDate":"09-Apr-2025","invoiceNumber":"INV-2025-087279","invoiceAmount":1000.00,"payGroup":"GEN CHQ","dateInvoiceReceived":"09-Apr-2025","dateGoodsReceived":"","remittanceCode":"01","specialHandling":"N","bankNumber":null,"branchNumber":null,"accountNumber":null,"eftAdviceFlag":null,"eftEmailAddress":null,"nameLine1":"Amanda Khalid","nameLine2":"","addressLine1":"16 High Street","addressLine2":"","addressLine3":"","city":"Coquitlam","country":"CA","province":"BC","postalCode":"V8V6V5","qualifiedReceiver":"CVAP Service Account","terms":"Immediate","payAloneFlag":"Y","paymentAdviceComments":"","remittanceMessage1":"21-22290-VIC-Khalid, Amanda","remittanceMessage2":"Wage Loss-Wage Loss","remittanceMessage3":"Crime Victim Assistance Program","termsDate":null,"glDate":"10-Apr-2025","invoiceBatchName":"SNBATCH","currencyCode":"CAD","invoiceLineDe...
    }

    [Fact]
    public async Task Send_Invoices_Cas_Transaction_Failure_Amount_Mismatch()
    {
        var invoices = new Invoice();
        invoices.IsBlockSupplier = true;
        invoices.InvoiceType = "Standard";
        invoices.SupplierNumber = "2002741";
        invoices.SupplierSiteNumber = 1;
        invoices.InvoiceDate = DateTime.Now;
        invoices.InvoiceNumber = "INV-2025-026102";
        invoices.InvoiceAmount = 150.00m;
        invoices.PayGroup = "GEN CHQ";
        invoices.DateInvoiceReceived = DateTime.Now;
        invoices.RemittanceCode = "01";
        invoices.SpecialHandling = false;
        invoices.NameLine1 = "Ida Albert";
        invoices.AddressLine1 = "2671 Champions Lounge";
        invoices.AddressLine2 = "30";
        invoices.AddressLine3 = "Galaxy Studios";
        invoices.City = "Chilliwack";
        invoices.Country = "CA";
        invoices.Province = "BC";
        invoices.PostalCode = "V4R9M0";
        invoices.QualifiedReceiver = "systemuser";
        invoices.Terms = "Immediate";
        invoices.PayAloneFlag = "Y";
        invoices.PaymentAdviceComments = "";
        invoices.RemittanceMessage1 = "21-03304-VIC-Albert, Ida";
        invoices.RemittanceMessage2 = "Income Support-Lost Earning Capacity-Minimum Wage";
        invoices.RemittanceMessage3 = "Crime Victim Assistance Program";
        invoices.GLDate = DateTime.Now;
        invoices.InvoiceBatchName = "SNBATCH";
        invoices.CurrencyCode = "CAD";
        invoices.InvoiceLineDetails = new List<InvoiceLineDetail>
        {
            new InvoiceLineDetail
            {
                InvoiceLineNumber = 1,
                InvoiceLineType = "Item",
                LineCode = "DR",
                InvoiceLineAmount = 284.00m,
                // vsd_programtyp columns vsd_clientcode.vsd_responsibilitycentre, vsd_serviceline, vsd_stob, vsd_projectcode
                DefaultDistributionAccount = "010.15004.10250.5298.1500000.000000.0000",
            }
        };

        var response = await casService.CreateInvoice(invoices);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //Assert.Equal("{Response { Content = {\"invoice_number\":\"INV-2025-026102\",\"CAS-Returned-Messages\":\"[065] Batch name is already present in CFS for an organization other than that of the invoice.;[065] Batch name is already present in CFS for an organization other than that of the invoice.\"}, StatusCode = BadRequest }}", response.Content);
    }

    [Fact]
    public async Task Search_Invoice_Succeed()
    {
        // Victim Services DEV
        var invoiceNumber = "INV-2025-026102";
        var supplierNumber = "2002741";
        var supplierSiteCode = "001";

        var response = await casService.GetInvoice(invoiceNumber, supplierNumber, supplierSiteCode);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // successful from Postman DEV
        //{"invoice_number":"INV-2025-026103","invoice_status":"Never Validated","payment_status":"Not Paid","payment_number":" ","payment_date":" "}

        // successful JSON returned from CAS example
        //{ "invoice_number":"INV-2025-026102","invoice_status":"Validated","payment_status":"Fully Paid","payment_number":"15200000023","payment_date":"17-APR-2025"}
    }

    [Fact]
    public async Task Get_Payment_Succeed()
    {
        // UTEST
        var paymentNumber = "15200000001";
        var payGroup = "GEN CHQ";

        await casService.GetPayment(paymentNumber, payGroup);

        // successful JSON returned from CAS example
        //{"paymentnumber":15200000001,"paygroup":"GEN CHQ","paymentdate":"02-MAY-2025 00:00:00","paymentamount":284,"paymentstatus":"NEGOTIABLE","paymentstatusdate":"02-MAY-2025 13:40:11"}
    }
}
