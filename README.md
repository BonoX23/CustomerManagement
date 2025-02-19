# Customer Management

> **Status do Projeto**: ✅ Em andamento

## 📌 Tópicos

- 🔹 [Descrição do projeto](#descrição-do-projeto)
- 🔹 [Solicitação do Cliente](#solicitação-do-cliente)
- 🔹 [Funcionalidades](#funcionalidades)
- 🔹 [Pré-requisitos](#pré-requisitos)
- 🔹 [Como rodar a aplicação](#como-rodar-a-aplicação)
- 🔹 [Resumo](#resumo)
- 🔹 [Tecnologias implementadas](#tecnologias-implementadas)
- 🔹 [Arquitetura](#arquitetura)
- 🔹 [Segurança](#segurança)
- 🔹 [Licença](#licença)

---

## 📖 Descrição do Projeto

O projeto consiste em uma **API para gerenciamento de clientes**.  
Para acessar alguns endpoints, é necessário **autenticação com dados de usuário**. Após autenticado, o usuário pode acessar todas as funcionalidades da API, enviando sempre o **token recebido** no cabeçalho das requisições.

---

## 🎯 Solicitação do Cliente

- Deve ser possível **criar, atualizar, visualizar e remover clientes**.
- O cadastro do cliente deve conter:
  - Nome
  - E-mail (não pode ser duplicado)
  - Logotipo
  - Endereço (um cliente pode ter vários endereços)
- Deve ser possível **criar, atualizar, visualizar e remover endereços**.
- O acesso à API deve ser **aberto**, mas requer **autenticação e autorização**.
- A API deverá **suportar alto volume de requisições**, com preocupação constante com **performance**.

---

## 🚀 Funcionalidades

✅ **Cadastro de Clientes**: Um usuário será gerado para manutenção dos dados.  
✅ **Cadastro de Endereços**: Clientes podem ter múltiplos endereços cadastrados.  
✅ **Autenticação**: Login na API e possibilidade de alterar senha após autenticado.  

---

## 🔧 Pré-requisitos

Para rodar o projeto localmente, é necessário:

- ⚠️ **.NET Core SDK 6.0**
- ⚠️ **SQL Server**
- ⚠️ **Visual Studio 2022** ou **VS Code**

---

## ▶ Como Rodar a Aplicação

### Clonando o projeto:

```sh
git clone https://github.com/BonoX23/CustomerManagement.git
```

### 🔹 Executar via Visual Studio Community 2022:

1. Acesse a pasta `src\Clients` e abra a solução `Clients.sln`.
2. Faça **restore** dos pacotes NuGet.
3. Defina `WebApi` como **Startup Project**.
4. No arquivo `appsettings.json`, configure a **string de conexão (DefaultConnection)** para acesso ao SQL Server.
5. Execute com `F5` ou `Ctrl + F5`.
6. O Swagger será exibido com os endpoints disponíveis.

### 🔹 Executar via terminal:

1. Acesse `src\Clients\Presentation\WebAPI`.
2. No `appsettings.json`, configure a **string de conexão (DefaultConnection)**.
3. Abra o terminal e execute:

```sh
dotnet restore
dotnet run
```

4. Após a compilação, acesse no navegador:

   - [https://localhost:7094/swagger/index.html](https://localhost:7094/swagger/index.html)
   - [http://localhost:5093/swagger/index.html](http://localhost:5093/swagger/index.html)

---

## 📌 Resumo

- Os endpoints `api/v1/auth` e `api/v1/cliente` são **públicos** (não requerem autenticação).  
- O cadastro de um cliente gera automaticamente um **usuário**, e a senha inicial é retornada na resposta.  
- O **token de autenticação** tem validade de **90 minutos**.  
- Endpoints `/api/v1/address` são para **manutenção de endereços**.  
- Endpoints `/api/v1/cliente` são para **manutenção de clientes** (excluir cliente remove também os endereços e usuário associado).  
- Para **alterar senha**, use `api/v1/auth/update-password` (autenticado).  

---

## 🛠 Tecnologias Implementadas

- **ASP.NET Core 6.0**
- **ASP.NET Web API**
- **Entity Framework**
- **Dapper**
- **SQL Server**
- **Swagger UI**
- **Migrations**
- **FluentValidation**
- **Filters Exceptions**

---

## 🏛 Arquitetura

- **Princípios SOLID e Clean Code**
- **Design Orientado por Domínio (DDD)**
- **Eventos e Notificações de Domínio**
- **Padrão Repositório**
- **Injeção de Dependência**

---

## 🔒 Segurança

- Autenticação via **JWT Bearer Tokens**.

---

## 📜 Licença

© 2025 - Customer Management
