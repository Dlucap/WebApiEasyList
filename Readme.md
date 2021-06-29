Especificação WebApi EasyList

- Api criada para fins educativos.

Descrição: Cadastrar uma lista de compra
* Cadastros possíveis:
 - Cadastrar usuário para logar na aplicação 
 - Cadastrar produto 
 - Cadastrar fornecedor
 - Cadastrar categoria
 - Cadastrar forma de pagamento
 - Criar e adicionar itens à lista de compra de sua própria lista ou de uma lista compartilhada
 - Adicionar a quantidade do produto a ser comprado no item de compra
 
* Ao fazer a compra:
 - Editar a lista de compra para adicionar o preço, quantidade, marca do produto atualizado
 - Adicionar novos produtos
 - Adicionar um check aos produtos adicionados ao carrinho de compra
 - Totalizar o valor gasto à medida em que os produtos forem marcados no carrinho
 - Finalizar a compra. Não será possível editar os dados da compra ou item da compra (rever regra negócio)
 - Alterar a quantidade do produto no item de compra correspondente
 - Permitir finalizar compra com a quantidade do item 0 caso o fornecedor não tenha o produto. (rever regra negócio)
 
* Estoque:
 - Cadastar uma quatidade mínima de produto no estoque
 - Adicionar produto com suas respectivas quantidades ao estoque após finalizar a compra
 - Abater a quantidade do estoque a medida em que o produto for sendo utilizado
 - Informar a data de validade do produto para controle
 - Consultar quais produtos estão próximos de acabar considerando a quantidade mínima cadastrada
 - Sugerir adicionar produtos que estão próximos de acabar na próxima lista de compra 
 
* Histórico 
 - Listar as compras por dia em uma quantidade mínima de 10 compras por vez (por página)
 - Realizar busca por período de data de compra, data de compra, fornecedor
 - Consultar todas as informações referentes às compras realizadas
 - Realizar comparação de preço de determinado produto em um prazo de 10 compras.
 
* Implementações futuras:
 - Criar aplicação para utilização da api
 - Criar rotina para avisar quais produtos estão próximos de vencer
 - Criar comparativo de preço entre os produtos da última compra com a lista de compra atual
 - Disponibilizar a api no heroku utilizando docker.
 
* Implementar log de toda a aplicação *
 
 Obs:
 Informações referentes ao usuário criação, data criação, usuário modificação, data modificação serão controladas pela api.
 
 
 