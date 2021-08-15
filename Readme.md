# Especificação WebApi EasyList

- Api criada para fins educativos.

Descrição: Cadastrar uma lista de compra
* Cadastros possíveis:
 - [x] Cadastrar usuário para logar na aplicação 
 - [x] Cadastrar produto 
 - [X] Cadastrar fornecedor
 - [X] Cadastrar categoria
 - [X] Cadastrar forma de pagamento
 - [X] Cadastro Compra
 - [X] Adicionar itens de compra para a compra cadastrada
 
* Ao fazer a compra:
 - [ ] Editar a lista de compra para adicionar o preço, quantidade, marca do produto atualizado
 - [ ] Adicionar novos produtos
 - [ ] Adicionar um check aos produtos adicionados ao carrinho de compra
 - [ ] Totalizar o valor gasto à medida em que os produtos forem marcados no carrinho
 - [ ] Finalizar a compra. Não será possível editar os dados da compra ou item da compra (rever regra negócio)
 - [ ] Alterar a quantidade do produto no item de compra correspondente
 - [ ] Permitir finalizar compra com a quantidade do item 0 caso o fornecedor não tenha o produto. (rever regra negócio)
 
* Estoque:
 - [ ] Cadastar uma quatidade mínima de produto no estoque
 - [ ] Adicionar produto com suas respectivas quantidades ao estoque após finalizar a compra
 - [ ] Abater a quantidade do estoque a medida em que o produto for sendo utilizado
 - [ ] Informar a data de validade do produto para controle
 - [ ] Consultar quais produtos estão próximos de acabar considerando a quantidade mínima cadastrada
 - [ ] Sugerir adicionar produtos que estão próximos de acabar na próxima lista de compra 

* Histórico 
 - [ ] Listar as compras por dia em uma quantidade mínima de 10 compras por vez (por página)
 - [ ] Realizar busca por período de data de compra, data de compra, fornecedor
 - [ ] Consultar todas as informações referentes às compras realizadas
 - [ ] Realizar comparação de preço de determinado produto em um prazo de 10 compras.
 
* Implementações futuras:
 - [ ] Criar aplicação para utilização da api
 - [ ] Criar rotina para avisar quais produtos estão próximos de vencer
 - [ ] Criar comparativo de preço entre os produtos da última compra com a lista de compra atual
 - [ ] Disponibilizar a api no heroku utilizando docker.
 - [ ] Implementar log de toda a aplicação 
 
 Obs:
 Informações referentes ao usuário criação, data criação, usuário modificação, data modificação serão controladas pela api.
 
 *** Informações Git
 - https://woliveiras.com.br/posts/facilitando-os-merges-no-git-com-o-visual-studio-code-como-merge-tool-e-editor-padr%C3%A3o/
 - https://www.youtube.com/watch?v=oXMgyQt0ce0&ab_channel=C%C3%B3digoFonteTV
 - https://www.youtube.com/watch?v=7riCX4Oxms8&ab_channel=GitHub 
 - https://www.youtube.com/watch?v=NR9jc5ODvuM&ab_channel=MichelliBrito
 - https://docs.microsoft.com/pt-br/azure/devops/repos/git/pull-requests?view=azure-devops
 - https://www.youtube.com/watch?v=T5OxUTOfjTk&ab_channel=CodingNight
 - https://www.youtube.com/watch?v=TEmvwagW8bI&ab_channel=AprendaC%C3%B3digo
 
 *** .NET 5 + ASP.NET Core + JWT + Refresh Tokens: exemplo de implementação
 - https://renatogroffe.medium.com/net-5-asp-net-core-jwt-refresh-tokens-exemplo-de-implementa%C3%A7%C3%A3o-fe885ecbaa4e 
 - https://github.com/renatogroffe/ASPNETCore5_JWT-Identity-RefreshTokens