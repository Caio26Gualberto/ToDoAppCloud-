# ToDoApp Backend

Backend do projeto **ToDoApp**, desenvolvido em **.NET 8 / C#** com **Entity Framework Core**.  
Ele fornece APIs para gerenciar tarefas, categorias, prioridade, status e suporta integração com o frontend.

---

## 🚀 Pré-requisitos

Antes de rodar o backend, você precisa ter:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Um **banco de dados** configurado (SQL Server)
- (Opcional) [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/) para desenvolvimento

---

## ⚙️ Instalação das dependências

No terminal, dentro da pasta `Backend`, rode:

```bash
dotnet restore
```

Para configuração do Banco de Dados

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ToDoAppDb;User Id=sa;Password=SuaSenha;"
}
```
