# VotingSystemClients

Este repositório contém duas aplicações cliente gRPC desenvolvidas em C# (.NET 8) para testar os serviços da Entidade de Registo e da Entidade de Votação do sistema de votação eletrónica definido na unidade curricular **Integração de Sistemas**.

- `VoterClient` – cliente da Autoridade de Registo (AR), preparado para invocar o método `IssueVotingCredential`.
- `VotingClient` – cliente da Autoridade de Votação (AV), que invoca os métodos `GetCandidates` e `Vote`.

> O servidor de mock utilizado para testes encontra-se no repositório:  
> `https://github.com/AndreMacielSousa/VotingMock`

## 1. Pré-requisitos

- .NET SDK 8.0 (ou compatível)
- `grpcurl` instalado na máquina de desenvolvimento
- Servidor de mock em execução (ver secção seguinte)

## 2. Levantar o servidor de mock

1. Clonar o repositório do mock:

   ```bash
   git clone https://github.com/AndreMacielSousa/VotingMock.git
   cd VotingMock
   dotnet run
