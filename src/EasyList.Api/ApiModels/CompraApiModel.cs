﻿using EasyList.Business.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Api.ApiModels
{
  public class CompraApiModel 
  {   
    public Guid Id { get; set; }
    public Guid FornecedorId { get; set; }
    public Guid FormaPagamentoId { get; set; }
    public bool Compartilhado { get; set; } = false;
    public StatusCompraEnum StatusCompra { get; set; }
    public IEquatable<ItmCompraApiModel> ItensCompra { get; set; }
    public DateTime DataCompra { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime DataModificacao { get; set; }
    //todo: Adicionar validações nos campos
  }
}