namespace EasyList.Business.Models
{
  public class Fornecedor : Entity
  {
    public string Nome { get; set; }
    public string NomeFantasia { get; set; }
    public string Cnpj { get; set; }
    public Endereco Endereco { get; set; }
       
    public bool Ativo { get; set; }
    //public string UsuarioCriacao { get; set; }
    //public DateTime DataCriacao { get; set; }
    //public string UsuarioModificacao { get; set; }
    //public DateTime DataModificacao { get; set; }
   
  }
}
