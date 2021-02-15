using System;

namespace WebApiEasyList.Data
{
  public class Fornecedor
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeFantasia { get; set; }    
    public string Cnpj { get; set; }   
    public string Cep { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Endereco { get; set; }
    public int Numero { get; set; }
    public string RecCreatedBy { get; set; }
    public DateTime RecCreatedOn { get; set; }
    public string RecModifiedBy { get; set; }
    public DateTime RecModifiedOn { get; set; }
  }
}
//todo: Criar testes unitários para a web Api https://docs.microsoft.com/pt-br/aspnet/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api
/*https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/multi-container-microservice-net-applications/test-aspnet-core-services-web-apps
 * https://docs.microsoft.com/pt-br/aspnet/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api
 * /// <summary>
    /// Formatar uma string CNPJ
    /// </summary>
    /// <param name="CNPJ">string CNPJ sem formatacao</param>
    /// <returns>string CNPJ formatada</returns>
    /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>
 
    public static string FormatCNPJ(string CNPJ)
    {
        return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
    }
 
    /// <summary>
    /// Formatar uma string CPF
    /// </summary>
    /// <param name="CPF">string CPF sem formatacao</param>
    /// <returns>string CPF formatada</returns>
    /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>
 
    public static string FormatCPF(string CPF)
    {
        return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
    }
    /// <summary>
    /// Retira a Formatacao de uma string CNPJ/CPF
    /// </summary>
    /// <param name="Codigo">string Codigo Formatada</param>
    /// <returns>string sem formatacao</returns>
    /// <example>Recebe '99.999.999/9999-99' Devolve '99999999999999'</example>
 
    public static string SemFormatacao(string Codigo)
    {
        return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
    }
 */
