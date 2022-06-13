# Comandos Entity Framework

 Parâmetros Comuns
   Parâmetro       |  Descrição
 :----------------:|---------------
  Context         | A classe DbContext a ser usada. 
  Projeto         | O projeto de destino. 
  StartupProject  | O projeto de inicialização.
 - Verbose         | Mostrar saída detalhada
 
 ## Add-Migration
  Adiciona noma migração
  
  Parâmetro       |  Descrição
 :---------------:|---------------
 Nome            | O nome da migração. Esse é um parâmetro posicional e é necessário.
 OutputDir       | O diretório é usado para gerar os arquivos.
 Namespace       | O namespace a ser usado para as classes geradas. 
 
```sh
add-migrations NomeMigrations -Context NomeContext
``` 

 ## Update-Database
Atualiza o banco de dados para a última migração ou para uma migração especificada. 
  
   Parâmetro      |  Descrição
 :---------------:|---------------
 Migração        |	A migração de destino. As migrações podem ser identificadas por nome ou por ID. O número 0 é um caso especial que significa antes da primeira migração e faz com que todas as migrações sejam revertidas. Se nenhuma migração for especificada, o comando usa como padrão a última migração.
Conexão          |	A cadeia de conexão para o banco de dados. O padrão é aquele especificado em AddDbContext ou OnConfiguring . Adicionado no EF Core 5,0.
 
 ```sh
update-database  -Context NomeContext
``` 
 
 ## Drop-Database
 Remove o banco de dados.
 
   Parâmetro      |  Descrição
 :---------------:|---------------
 WhatIf          |	Mostrar qual banco de dados seria descartado, mas não descartá-lo.
 
 ## Get-DbContext
 Lista as migrações disponíveis. Adicionado no EF Core 5,0.

 ## Get-Migration
 Lista as migrações disponíveis. Adicionado no EF Core 5,0.
 
  Parâmetro       |  Descrição
 :---------------:|---------------
 Conexão          |	A cadeia de conexão para o banco de dados. O padrão é aquele especificado em AddDbContext ou onconfiguring.
 Noconnect	      |Não se conecte ao banco de dados.

  ```sh
 remove-migration  -Context NomeContext
```
 ## Remove-Migration
 Remove a última migração (reverte as alterações de código que foram feitas para a migração).
 
   Parâmetro      |  Descrição
 :---------------:|---------------
  Force           | Reverter a migração (reverter as alterações que foram aplicadas ao banco de dados). 

```sh
 remove-migration  -Context NomeContext
```
 
 ## Script-DbContext
 Gera um script SQL do DbContext. Ignora todas as migrações. Adicionado no EF Core 3,0.
 
   Parâmetro       |  Descrição
 :----------------:|---------------
   Saída           |	O arquivo no qual o resultado será gravado
 
  ```sh
 Script-Migration 
```
 
 ## Script-Migration
 Gera um script SQL que aplica todas as alterações de uma migração selecionada para outra migração selecionada.
 
   Parâmetro     |  Descrição
 :---------------:|---------------
  De              |	A migração inicial. As migrações podem ser identificadas por nome ou por ID. O número 0 é um caso especial que significa antes da primeira migração. Assume o padrão de 0.
 Para             |	A migração final. O padrão é a última migração.
 Idempotente	  | Gere um script que possa ser usado em um banco de dados em qualquer migração.
 Transtransações  |	Não gere instruções de transação SQL. Adicionado no EF Core 5,0.
 Saída            |	O arquivo no qual o resultado será gravado. Se esse parâmetro for omitido, o arquivo será criado com um nome gerado na mesma pasta que os arquivos de tempo de execução do aplicativo são criados, por exemplo: /obj/Debug/netcoreapp2.1/ghbkztfz.SQL/.
 
 ```sh
 Script-Migration -Context NomeContext
```
## Fonte: https://docs.microsoft.com/pt-br/ef/core/cli/powershell
 
 
 
 
 
 
