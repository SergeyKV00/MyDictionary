using System.Security.Claims;

namespace MyDictionary.Domain;

public class SessionContext
{
    public Guid UserId { get; private set; }

    public void Init(IEnumerable<Claim> claims)
    {
        var claimsDictionary = claims
            .GroupBy(c => c.Type)
            .ToDictionary(
                g => g.Key,
                g => g.Count() == 1 ? g.First().Value : string.Join(",", g.Select(c => c.Value))
            );

        var claimUserId = claimsDictionary.GetValueOrDefault("userId");
        UserId = Guid.TryParse(claimUserId, out var userId) ? userId : Guid.Empty;
    }
}
