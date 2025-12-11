# VotingSystemClients

Este repositório contém duas aplicações cliente desenvolvidas em **C# (.NET 8)** para testar os serviços gRPC do sistema de votação eletrónica definido na unidade curricular **Integração de Sistemas** da Universidade Aberta.

Os clientes interagem com o servidor de mock disponível no repositório:

➡️ https://github.com/AndreMacielSousa/VotingMock

---

## 📁 Estrutura do repositório

VotingSystemClients/
├── VoterClient/ # Cliente da Entidade de Registo
├── VotingClient/ # Cliente da Entidade de Votação
├── Protos/
│ ├── voter.proto # Contrato IssueVotingCredential
│ └── voting.proto # Contratos GetCandidates e Vote
└── README.md


Cada projeto referencia o respetivo `.proto` no `.csproj`, permitindo a geração automática dos stubs gRPC.

---

## 🛠️ 1. Pré-requisitos

- .NET SDK **8.0** ou superior  
- `grpcurl` (opcional, para testes manuais)  
- Servidor de mock a correr (`VotingMock`)

---

## 🚀 2. Levantar o servidor de mock

Numa consola separada:

```bash
git clone https://github.com/AndreMacielSousa/VotingMock.git
cd VotingMock
dotnet run

O servidor ficará disponível em:

http://0.0.0.0:9091

## 3. Compilar este repositório
git clone https://github.com/AndreMacielSousa/VotingSystemClients.git
cd VotingSystemClients
dotnet build

## 4. Executar o VoterClient (Entidade de Registo)

⚠️ Nota: O mock atual não inclui o serviço VoterRegistrationService.
O cliente está implementado para cumprir o enunciado e funcionará assim que o serviço correspondente estiver disponível.

dotnet run --project VoterClient


Fluxo da aplicação:

Introdução do número de Cartão de Cidadão

Construção do pedido IssueVotingCredentialRequest

Apresentação da resposta (voting_credential, message)

## 5. Executar o VotingClient (Entidade de Votação)
dotnet run --project VotingClient


Fluxo da aplicação:

Invoca GetCandidates e apresenta a lista de candidatos

Solicita:

credencial de voto

ID do candidato

Invoca Vote e apresenta a mensagem devolvida pelo servidor

## 6. Testes com grpcurl
6.1. Obter lista de candidatos
grpcurl -plaintext -proto Protos/voting.proto \
  localhost:9091 \
  voting.VotingService/GetCandidates


Exemplo de resposta:

{
  "candidates": [
    { "id": 1, "name": "Andre" },
    { "id": 2, "name": "Bruno" },
    { "id": 3, "name": "Carlos" }
  ]
}

6.2. Submeter voto
grpcurl -plaintext -proto Protos/voting.proto \
  -d "{\"voting_credential\": \"TESTE\", \"candidate_id\": 1}" \
  localhost:9091 \
  voting.VotingService/Vote


Exemplo de resposta:

{
  "message": "Credential already used."
}

## 📚 7. Licença

Código disponibilizado exclusivamente para fins académicos no âmbito da UC Integração de Sistemas, Universidade Aberta.


