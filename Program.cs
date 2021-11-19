using C4Sharp.Diagrams.Core;
using C4Sharp.Models;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;

var customer = new Person("cliente", "Cliente do sistema bancário") { Description = "Um cliente do banco, com contas pessoais." };
var bankingSystem = new SoftwareSystem("SistemaBancario", "Internet Banking System") { Description = "Allows customers to view information about their bank accounts, and make payments." };
var mainframe = new SoftwareSystem("Mainframe", "Mainframe Banking System") { Description = "Armazena as principais informações bancarioas tais como clientes, contas, transações, etc.", Boundary = Boundary.External };
var mailSystem = new SoftwareSystem("SistemaDeEmail", "Sistema de Email") { Description = "SendGrid", Boundary = Boundary.External };

var diagram = new ContextDiagram
{
    Title = "Diagrama de um sistema bancário",
    Structures = new Structure[]
    {
        customer,
        bankingSystem,
        mainframe,
        mailSystem
    },
    Relationships = new[]
    {
        (customer > bankingSystem),
        (customer < mailSystem)["Envia email para"],
        (bankingSystem > mailSystem)["Envia email", "SMTP"][Position.Neighbor],
        (bankingSystem > mainframe),
    }
};

new PlantumlSession()
    .UseDiagramImageBuilder()
    .UseStandardLibraryBaseUrl()
    .Export(new[] { diagram });

Console.WriteLine("Diagrama gerado");