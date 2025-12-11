// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Grpc.Net.Client;
using Voting;

Console.WriteLine("=== Cliente da Autoridade de Votação ===");
Console.WriteLine();

// IMPORTANTE: ajusta o endereço se o mock estiver noutro endpoint ou em HTTPS.
using var channel = GrpcChannel.ForAddress("http://localhost:9091");

var client = new VotingService.VotingServiceClient(channel);

try
{
    // 1. Obter lista de candidatos
    Console.WriteLine("A obter lista de candidatos...");
    var candidatesResponse = await client.GetCandidatesAsync(new GetCandidatesRequest());

    Console.WriteLine();
    Console.WriteLine("=== Candidatos Disponíveis ===");

    if (candidatesResponse.Candidates.Count == 0)
    {
        Console.WriteLine("Nenhum candidato disponível.");
        return;
    }

    foreach (var c in candidatesResponse.Candidates)
    {
        Console.WriteLine($"{c.Id} - {c.Name}");
    }

    Console.WriteLine();
    // 2. Ler credencial de voto
    Console.Write("Introduza a credencial de voto: ");
    var credential = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(credential))
    {
        Console.WriteLine("Credencial de voto inválida.");
        return;
    }

    // 3. Ler ID do candidato
    Console.Write("Introduza o ID do candidato em que pretende votar: ");
    var candidateIdText = Console.ReadLine();

    if (!int.TryParse(candidateIdText, out var candidateId))
    {
        Console.WriteLine("ID de candidato inválido.");
        return;
    }

    // 4. Submeter voto
    var voteRequest = new VoteRequest
    {
        VotingCredential = credential,
        CandidateId = candidateId
    };

    Console.WriteLine();
    Console.WriteLine("A submeter o voto à Autoridade de Votação...");

    var voteResponse = await client.VoteAsync(voteRequest);

    Console.WriteLine();
    Console.WriteLine("=== Resposta da Autoridade de Votação ===");
    Console.WriteLine($"Estado:   {voteResponse.Status}");
    Console.WriteLine($"Mensagem: {voteResponse.Message}");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("Ocorreu um erro ao comunicar com a Autoridade de Votação:");
    Console.WriteLine(ex.Message);
}
