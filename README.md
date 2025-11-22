# DIO - Trilha .NET - API e Entity Framework

www.dio.me

## Desafio de projeto

Foi realizado um projeto de tarefa, onde parte do código já estava implementado, mas o código estava incompleto. Eu completei o código, seguindo as regras do desafio, e você poderá conferir o resultado final no repositório.

## Contexto

Foi construido um sistema gerenciador de tarefas simples, onde é possível cadastrar, editar, deletar, consultar tarefas e alterar o status das tarefas.

Essa lista de tarefas tem um CRUD, ou seja, permiti a você obter os registros, criar, salvar e deletar esses registros.

É uma aplicação do tipo Web API, utilizando o Entity Framework para manipulação do banco de dados.

## Métodos Implementados

Foi implementado os métodos conforme a seguir:

**Swagger**

![Métodos Swagger](https://raw.githubusercontent.com/Samuel-Henrique6/trilha-net-api-desafio/main/swagger.png)

**Endpoints**

| Verbo  | Endpoint                   | Parâmetro | Body          |
| ------ | -------------------------- | --------- | ------------- |
| GET    | /Tarefa/{id}               | id        | N/A           |
| PUT    | /Tarefa/{id}               | id        | Schema Tarefa |
| DELETE | /Tarefa/{id}               | id        | N/A           |
| GET    | /Tarefa/ObterTodos         | N/A       | N/A           |
| GET    | /Tarefa/ObterPorTitulo     | titulo    | N/A           |
| GET    | /Tarefa/ObterPorData       | data      | N/A           |
| GET    | /Tarefa/ObterPorStatus     | status    | N/A           |
| POST   | /Tarefa                    | N/A       | Schema Tarefa |
| PATCH  | /Tarefa/Toggle-Status/{id} | id        | N/A           |

Esse é o schema (model) de Tarefa, utilizado para passar para os métodos que exigirem

```json
{
    "id": 0,
    "titulo": "string",
    "descricao": "string",
    "data": "2022-06-08T01:31:07.056Z",
    "status": "Pendente"
}
```

## Tecnologias Utilizadas

-   C#
-   .NET 10.0
-   Entity Framework Core
-   SQL Server
-   Swagger
