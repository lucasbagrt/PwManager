# Password Manager
Gerenciador de senhas, feito em .NET 6 e Vue 3

### 📋 Pré-requisitos

* PostgreSQL
* .NET 6
* Vue 3
* Visual Studio (preferencialmente o 2022)

### 🔧 Instalação

### Backend:

1 - Abra o Visual Studio como administrador e selecione a opção "Clone a repository"

2 - No arquivo *appsettings.json*, ajuste o Username e a Password, colocando o mesmo do seu PostgreSQL

3 - Com o projeto aberto no Visual Studio, vá em Tools => Command Line => e selecione alguma das duas opções (tanto faz), e execute o seguinte comando: 
```
cd PasswordManager
```

3 - No path *\PwManager\PasswordManager\PasswordManager*, execute os seguintes comandos:
```
dotnet ef migrations add "PwManagerMg"
```
```
dotnet ef database update
```

4 - Clique para iniciar o projeto, e certique que está rodando na porta 7155, caso não estiver, é só trocar no front-end os endpoints

### Front-end

1 - Abra a pasta pwmanager-client na sua IDE, e execute os seguintes comandos:
```
npm install
```
```
npm run build
```
```
npm run serve
```

### Banco de dados

Apos fazer os passos acima para executar o backend e o front-end, execute a seguinte query, para criar algumas aplicações:

```sql
insert into applications values (1, 'Github', 'fa fa-github fa-lg');
insert into applications values (2, 'Youtube', 'fa fa-youtube fa-lg');
insert into applications values (3, 'Google', 'fa fa-google fa-lg');
insert into applications values (4, 'Amazon', 'fa fa-amazon fa-lg');
insert into applications values (5, 'Instagram', 'fa fa-instagram fa-lg');
insert into applications values (6, 'Linkedin', 'fa fa-linkedin fa-lg');
insert into applications values (7, 'Twitter', 'fa fa fa-twitter fa-lg');
```
