# eeduca-api
Api RESTful para o aplicativo educacional eeduca. Desenvolvida em c# utilizando o padrão WebApi.

Utiliza o entity framework 6 no formato code-first para conectar à uma base de dados MySQL.

# como contribuir
Para poder compilar o projeto e contribuir, você precisa seguir alguns passos importantes:

### Instale a versão community do MySQL
Baixe o instalador do site da Oracle: https://www.mysql.com/

### Instale o Visual Studio Community
Baixe o instalador do site da Microsoft: https://visualstudio.microsoft.com/

### Crie o schema para o banco de dados
No MySQL WorkBench, conecte à sua instância e crie um novo schema chamado eeduca.

### Desenvolva!
O projeto já está se conectando à base de dados no db4free, mas se quiser criar uma base local para testar alguma funcionalidade, precisa modificar a string de conexão no arquivo Web.Config.

Se optar por utilizar uma base local, precisa criar a estrutura do banco de dados no seu schema. Para isto, com o projeto aberto no Visual Studio, acesse o Package Manager Console (Tools > NuGet Package Manager > Package Manager Console) e digite o seguinte comando: 

```
update-database
```

Este comando irá criar toda a estrutura de dados no schema de forma automática utilizando as migrações do Entity Framework.

Divirta-se! Seu ambiente está pronto para produzir.
