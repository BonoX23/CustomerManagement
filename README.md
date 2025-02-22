# Customer Management

> **Status do Projeto**: ✅ Concluído

## 📌 Tópicos

- [Descrição do Projeto](#descrição-do-projeto)
- [Solicitação do Cliente](#solicitação-do-cliente)
- [Funcionalidades](#funcionalidades)
- [Pré-requisitos](#pré-requisitos)
- [Tecnologias Implementadas](#tecnologias-implementadas)
- [Arquitetura](#arquitetura)
- [Segurança](#segurança)
- [Licença](#licença)

## 📖 Descrição do Projeto

Projeto de uma aplicação para gerenciamento de clientes. O usuário acessa a aplicação por meio de um Frontend desenvolvido em MVC, que consome uma API REST.
Para utilizar alguns endpoints da API, é necessário estar autenticado com credenciais de usuário. Após a autenticação, é possível acessar todas as funcionalidades da API, 
passando sempre o token recebido no cabeçalho das requisições.

## 📝 Solicitação do Cliente

- Deve ser possível criar, atualizar, visualizar e remover clientes.
  - O cadastro dos clientes deve conter apenas os seguintes campos:
    - Nome
    - E-mail
    - Logotipo
    - Endereço (um cliente pode conter vários endereços)
  - Um cliente não pode se registrar duas vezes com o mesmo endereço de e-mail.
  - Deve ser possível criar, atualizar, visualizar e remover os endereços dos clientes.
  - O acesso à API deve ser aberto ao público, porém deve possuir autenticação e autorização.
  - A API deve ser otimizada para lidar com um grande volume de requisições.

## ⚙️ Funcionalidades

✔️ **Cadastro de Clientes**: É possível realizar cadastros de clientes. Para cada cliente cadastrado, será gerado um usuário para manutenção dos dados.

✔️ **Cadastro de Endereços**: É possível cadastrar múltiplos endereços para um determinado cliente.

✔️ **Autenticação**: É possível se autenticar na aplicação e acessar as funcionalidades do sistema após o login.

## 🛠 Pré-requisitos

Caso execute o projeto localmente, será necessário:

- ⚠️ .NET Core SDK 6.0
- ⚠️ SQL Server
- ⚠️ Visual Studio 2022 ou VS Code

## 🚀 Como Rodar a Aplicação

### 🔹 Clonando o Repositório

Abra um terminal e execute:

```sh
git clone https://github.com/BonoX23/CustomerManagement.git
```

### 🔹 Executando no Visual Studio Community 2022 (método recomendado)

1. Vá até a pasta `src/Clients` e abra a solução `Clients.sln`.
2. Faça o restore dos pacotes NuGet.
3. No menu de seleção de projetos de inicialização, clique na setinha lateral e selecione **Múltiplos Projetos de Inicialização**.
4. Marque os projetos `WebAPI` e `FrontEnd` como startup e clique em **OK**.
5. No arquivo `appsettings.json`, configure a string de conexão `DefaultConnection` com os dados de acesso ao SQL Server.
6. Execute o projeto pressionando `F5` ou `Ctrl+F5`.
7. O navegador abrirá a aplicação com a interface MVC.

### 🔹 Executando via Terminal

1. Acesse a pasta `src/Clients/Presentation/WebAPI`.
2. Edite o arquivo `appsettings.json` e ajuste a string de conexão `DefaultConnection`.
3. Abra um terminal e execute:
   ```sh
   dotnet restore
   dotnet run
   ```
4. Abra outro terminal e vá até `src/Clients/Presentation/FrontEnd`.
5. Execute o Frontend:
   ```sh
   dotnet run
   ```
6. O navegador **não** abrirá automaticamente a aplicação MVC.

7. Acesse manualmente no navegador:
   [https://localhost:7296/](https://localhost:7296/)

## 📌 Resumo

- Ao iniciar a aplicação você será direcionado a tela de boas vindas, onde poderá cadastrar um cliente ou logar um cliente existente.
- Para ter acesso aos detalhes do cliente cadastrado, é necessário sua autennticação na área de login.
- Ao cadastrar um cliente você será redireciano a tela de detalhes do cliente cadastrado, onde poderá também cadastrar mais de um endereço como também alterar o nome do cliente.


## 🏗 Tecnologias Implementadas

- **Backend**
  - ASP.NET Core 6.0
  - ASP.NET WebAPI Core
  - Entity Framework
  - Dapper
  - SQL Server
  - Swagger UI
  - Migrations
  - FluentValidation
  - Filters Exceptions

- **Frontend**
  - MVC (Model-View-Controller)

## 🏛 Arquitetura

- Arquitetura baseada em princípios **SOLID** e **Clean Code**.
- Design orientado a domínio (**DDD**).
- Implementação de **Eventos de Domínio**.
- Utilização do **Padrão de Repositório**.
- Injeção de dependências para melhor organização do código.

## 🔒 Segurança

- Autenticação baseada em **JWT Bearer Tokens**.

## 📜 Licença

&copy; 2025 - Customer Management
