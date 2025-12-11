// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Grpc.Net.Client;
using Voting;

Console.WriteLine("=== Cliente da Autoridade de Registo ===");
Console.WriteLine();

Console.Write("Introduza o número do Cartão de Cidadão: ");
var citizenCardNumber = Console.ReadLine();

if (string.IsNullOrWhiteSpace(citizenCardNumber))
{
    Console.WriteLine("Número de Cartão de Cidadão inválido.");
    return;
}

// HTTPS ou HTTP consoante o mock – por agora deixamos http
using var channel = GrpcChannel.ForAddress("http://localhost:9093");

var client = new VoterRegistrationService.VoterRegistrationServiceClient(channel);

var request = new IssueVotingCredentialRequest
{
    CitizenCardNumber = citizenCardNumber
};

try
{
    Console.WriteLine();
    Console.WriteLine("A solicitar credencial de voto à Autoridade de Registo...");

    var response = await client.IssueVotingCredentialAsync(request);

    Console.WriteLine();
    Console.WriteLine("=== Resposta da Autoridade de Registo ===");
    Console.WriteLine($"Credencial de voto: {response.VotingCredential}");
    Console.WriteLine($"Mensagem:          {response.Message}");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("Ocorreu um erro ao obter a credencial de voto:");
    Console.WriteLine(ex.Message);
}
