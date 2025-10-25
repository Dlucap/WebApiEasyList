using System;
using System.ComponentModel.DataAnnotations;

namespace EasyList.Business.Models
{
  public class LogEntry : Entity
  {
    public LogEntry()
    {
      Timestamp = DateTime.UtcNow;
    }

    public DateTime Timestamp { get; set; }
    
    [MaxLength(50)]
    public string Level { get; set; }
    
    [MaxLength(200)]
    public string Category { get; set; }
    
    public string Message { get; set; }
    
    public string Exception { get; set; }
    
    [MaxLength(10)]
    public string HttpMethod { get; set; }
    
    [MaxLength(500)]
    public string Path { get; set; }
    
    public int? StatusCode { get; set; }
    
    public int? Duration { get; set; }
    
    [MaxLength(100)]
    public string UserId { get; set; }
    
    [MaxLength(100)]
    public string UserName { get; set; }
    
    [MaxLength(50)]
    public string IpAddress { get; set; }
    
    public string AdditionalInfo { get; set; }
  }
}
