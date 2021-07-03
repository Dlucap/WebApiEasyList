using System;

namespace EasyList.Business.Models
{
  public interface  IUser
  {
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    bool IsAuthenticated();
    bool IsInRole(string role);

    //IEnumerable<Claim> GetClaimsIdentity();
  }
}
