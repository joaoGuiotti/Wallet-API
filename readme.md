# Wallet API

Uma API REST para gerenciamento de carteira digital construída com .NET 8, Entity Framework Core e integração com Kafka para eventos.

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados principal
- **Apache Kafka** - Sistema de mensageria para eventos
- **Docker & Docker Compose** - Containerização
- **Swagger/OpenAPI** - Documentação da API

## 📋 Pré-requisitos

- [Docker](https://www.docker.com/get-started) e [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (para desenvolvimento local)
- [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/)

## 🐳 Como Iniciar o Projeto

### Usando Docker (Recomendado)

1. **Clone o repositório:**
   ```bash
   git clone <url-do-repositorio>
   cd Wallet
   ```

2. **Inicie todos os serviços com Docker Compose:**
   ```bash
   docker-compose up -d
   ```

   Isso irá iniciar:
   - PostgreSQL (porta 5432)
   - Apache Kafka (porta 9092)
   - Zookeeper (porta 2181)
   - Wallet API (porta 8080)

3. **Verificar se os serviços estão rodando:**
   ```bash
   docker-compose ps
   ```

4. **Acessar a API:**
   - API: http://localhost:8080
   - Swagger UI: http://localhost:8080/swagger

### Desenvolvimento Local

1. **Configure as variáveis de ambiente:**
   ```bash
   # Copie o arquivo de exemplo
   cp .env.example .env
   
   # Edite as configurações conforme necessário
   ```

2. **Inicie apenas as dependências (PostgreSQL e Kafka):**
   ```bash
   docker-compose up -d postgres kafka zookeeper
   ```

3. **Execute as migrações do banco:**
   ```bash
   dotnet ef database update --project Wallet.Infrastructure --startup-project Wallet.API
   ```

4. **Inicie a aplicação:**
   ```bash
   cd Wallet.API
   dotnet run
   ```

## 🧪 Como Testar a API

### Usando o arquivo Wallet.http

O projeto inclui um arquivo `Wallet.http` na raiz que contém exemplos de todas as requisições da API.

#### Pré-requisitos para usar .http files:
- **Visual Studio Code** com a extensão [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
- **Visual Studio 2022** (suporte nativo)
- **JetBrains Rider** (suporte nativo)

#### Como usar:

1. **Abra o arquivo `Wallet.http`**

2. **Configure as variáveis (se necessário):**
   ```http
   @baseUrl = http://localhost:8080
   @walletId = seu-wallet-id-aqui
   ```

3. **Execute as requisições clicando em "Send Request":**

   **Exemplo de requisições disponíveis:**
   ```http
   ### Criar uma nova carteira
   POST {{baseUrl}}/api/wallets
   Content-Type: application/json
   
   {
     "userId": "user123",
     "initialBalance": 1000.00
   }
   
   ### Consultar saldo da carteira
   GET {{baseUrl}}/api/wallets/{{walletId}}/balance
   
   ### Criar uma transação de débito
   POST {{baseUrl}}/api/wallets/{{walletId}}/transactions
   Content-Type: application/json
   
   {
     "amount": 50.00,
     "type": "Debit",
     "description": "Compra no supermercado"
   }
   
   ### Criar uma transação de crédito
   POST {{baseUrl}}/api/wallets/{{walletId}}/transactions
   Content-Type: application/json
   
   {
     "amount": 200.00,
     "type": "Credit",
     "description": "Depósito"
   }
   
   ### Listar transações da carteira
   GET {{baseUrl}}/api/wallets/{{walletId}}/transactions?page=1&pageSize=10
   ```

### Usando Swagger UI

1. **Acesse:** http://localhost:8080/swagger

2. **Teste diretamente na interface:**
   - Clique em "Try it out"
   - Preencha os parâmetros
   - Clique em "Execute"

### Usando cURL

```bash
# Criar carteira
curl -X POST http://localhost:8080/api/wallets \
  -H "Content-Type: application/json" \
  -d '{"userId": "user123", "initialBalance": 1000.00}'

# Consultar saldo
curl -X GET http://localhost:8080/api/wallets/{walletId}/balance

# Criar transação
curl -X POST http://localhost:8080/api/wallets/{walletId}/transactions \
  -H "Content-Type: application/json" \
  -d '{"amount": 50.00, "type": "Debit", "description": "Teste"}'
```

## 📁 Estrutura do Projeto

```
Wallet/
├── Wallet.API/                 # Camada de apresentação (Controllers, DTOs)
├── Wallet.Application/         # Lógica de aplicação (Services, Interfaces)
├── Wallet.Domain/             # Entidades de domínio e regras de negócio
├── Wallet.Infrastructure/     # Acesso a dados e integrações externas
├── Wallet.Tests/             # Testes unitários e de integração
├── docker-compose.yml        # Configuração Docker
├── Dockerfile               # Build da aplicação
├── Wallet.http             # Arquivo de testes HTTP
└── README.md               # Este arquivo
```

## 🔧 Configuração

### Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto:

```env
# Database
POSTGRES_DB=walletdb
POSTGRES_USER=wallet_user
POSTGRES_PASSWORD=wallet_pass

# Kafka
KAFKA_BOOTSTRAP_SERVERS=localhost:9092

# API
ASPNETCORE_ENVIRONMENT=Development
```

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=walletdb;Username=wallet_user;Password=wallet_pass"
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "Topics": {
      "TransactionCreated": "transaction-created"
    },
    "ConsumerSettings": {
      "GroupId": "wallet-consumer-group"
    }
  }
}
```

## 🐛 Solução de Problemas

### Erro de conexão com o banco
```bash
# Verificar se o PostgreSQL está rodando
docker-compose logs postgres

# Recriar o banco
docker-compose down
docker-compose up -d postgres
```

### Erro de conexão com Kafka
```bash
# Verificar logs do Kafka
docker-compose logs kafka

# Reiniciar serviços de mensageria
docker-compose restart zookeeper kafka
```

### Portas ocupadas
```bash
# Verificar quais portas estão em uso
netstat -tulpn | grep :8080
netstat -tulpn | grep :5432

# Parar todos os containers
docker-compose down
```

## 📚 Endpoints da API

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/wallets` | Criar nova carteira |
| GET | `/api/wallets/{id}` | Obter dados da carteira |
| GET | `/api/wallets/{id}/balance` | Consultar saldo |
| POST | `/api/wallets/{id}/transactions` | Criar transação |
| GET | `/api/wallets/{id}/transactions` | Listar transações |
| GET | `/api/transactions/{id}` | Obter transação específica |

## 🔄 Eventos Kafka

A aplicação publica eventos no Kafka quando transações são criadas:

- **Tópico:** `transaction-created`
- **Evento:** `TransactionCreatedEvent`

## 🧪 Executar Testes

```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes específicos
dotnet test --filter "TestCategory=Unit"
```

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 🤝 Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📞 Suporte

Se você encontrar algum problema ou tiver dúvidas:

1. Verifique a seção de [Solução de Problemas](#-solução-de-problemas)
2. Consulte os logs com `docker-compose logs [service-name]`
3. Abra uma issue no repositório

---

**Desenvolvido com ❤️ usando .NET 8**