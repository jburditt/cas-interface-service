## Generate Entities

### Version
As of June 4, 2025; this is the latest version. For more examples, see VSD repository.

### Overview
This Database project uses the latest standard of using Dynamics as a database and exposing ORM entities for use with repositories and command handlers
The advantages of this new standard are:
- Higher performance especially with nested queries which are no longer N^2 queries
- Strongly typed queries
- Compatible with repository or command handler patterns e.g. MediatR


### Prerequisites
- [XrmToolbox](https://www.xrmtoolbox.com/) with [Early Bound Generator V2 plugin](https://www.xrmtoolbox.com/plugins/DLaB.Xrm.EarlyBoundGeneratorV2/)
Download the latest version on the XrmToolbox home page. To install the plugin, open XrmToolbox, click on "Configuration -> Tool Library" and search for "Early Bound Generator V2" and install it.
You can also use Tool Library to update the existing plugins.
- Cisco vpn.gov.bc.ca connection


### Setup new database
1. Add a new class library project to your .NET solution e.g. "Database"
2. See below on how to create a new connection
3. Open XrmToolBox -> Tool "Early Bound Generator V2" and save the default settings to the project folder
4. Select "Entities Whitelist" and select the tables you will be need to access in the solution
5. Set "Namespace" -> "Database.Model", "Output Relative Directory" -> "Model", and "Service Context Name" -> "DatabaseContext"
6. Click "Generate" to generate the entities, messages, optionsets in their corresponding folders and DatabaseContext.
7. Copy "Shared.Contract" and "Shared.Database" folders and file "Database\DatabaseContext.Partial.cs"
8. Add project reference "Shared.Database" to project "Database"
8. Review the entity names, if you see any casing issues, add the prefix or name to the "Token Capitalization Overrides" e.g. EMCR will fix EMcR_ExpenseProject and STOB will fix EMcR_SToB
9. Add nuget packages "Microsoft.PowerPlatform.Dataverse.Client" and "Microsoft.PowerPlatform.Dataverse.Client.Dynamics" to project in step #1
10. Create DTO primitive copies of the entities, enums, etc in a new class library project e.g. "Manager.Contract"
11. Add mappings for the Entity -> DTO and DTO -> Entity 
12. Edit the prefix in file "SharedMapper.cs"
13. Add user secrets found in "ServiceCollectionExtensions.cs" from a developer or OpenShift


### How to generate entities
1. Open XrmToolbox, add connection, select connection string, and then add the following connection string replacing the placeholders with your IDIR login:
`authtype=AD;url=https://cscp-vs.dev.jag.gov.bc.ca;domain=https://ststest.gov.bc.ca/adfs/oauth2/token;username=<idir_username>@gov.bc.ca;password=<password>`
   Connection Wizard also works, leave domain and realm url blank and enter your credentials. The url will be something like https://embc-dfa.dev.jag.gov.bc.ca/ 
2. Open DLaB.EarlyBoundGeneratorV2.DefaultSettings.xml and then click "Generate" button.
This will generate the entities, messages, optionsets in their corresponding folders and DatabaseContext.

NOTE in theory, you could add your authentication profile to PAC using your connection string and then use the command lines found in the generated code. If you do try this, please update this ReadMe.md with your findings.

## Troubleshooting

If you encounter a user authentication error and the authentication hasn't changed and your VPN is connected, try restarting the XrmToolbox application. I find this happens often but restarting always fixes the issue.

## Dataverse Cheatsheet

.AddLink - Adds a link between two entity instances that already exist in database
.AddRelatedObject - Adds a new related entity to an existing entity