﻿using System;

namespace WebApiEasyList.Data
{
  public class Produto
  {
    public int Id { get; set; }
    public string Marca { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int IdCategoria { get; set; }
    public DateTime Validade { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }
  }
}
