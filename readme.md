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
   - Wallet API (porta 3003)

3. **Verificar se os serviços estão rodando:**
   ```bash
   docker-compose ps
   ```

4. **Acessar a API:**
   - API: http://localhost:3003
   - Swagger UI: http://localhost:3003/swagger

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
   ```http
   @baseUrl = http://localhost:3003
   @walletId = seu-wallet-id-aqui
   ```

2. **Execute as requisições clicando em "Send Request":**

### Usando Swagger UI

1. **Acesse:** http://localhost:3003/swagger

2. **Teste diretamente na interface:**
   - Clique em "Try it out"
   - Preencha os parâmetros
   - Clique em "Execute"

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
netstat -tulpn | grep :3003
netstat -tulpn | grep :5432

# Parar todos os containers
docker-compose down
```
## 🔄 Eventos Kafka

A aplicação publica eventos no Kafka quando transações são criadas:

- **Tópico:** `transaction-created`
- **Evento:** `TransactionCreatedEvent`

## 🧪 Executar Testes

```bash
# Executar todos os testes
dotnet test
```

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
---

**Desenvolvido usando .NET 8**