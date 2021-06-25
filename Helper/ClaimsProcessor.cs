using System.Security.Claims;
namespace AduabaNeptune.Helper
{
    public class ClaimsProcessor
    {
        public static string CheckClaimForEmail(ClaimsPrincipal requesterIdentity)
        {
            var hasClaim = requesterIdentity.HasClaim(c => c.Type == CustomClaimType.Email.ToString());

            if(!hasClaim)
            {
                return null;
            }
            else
            {
                var emailClaim = requesterIdentity.FindFirst(c => c.Type == CustomClaimType.Email.ToString());

                if(string.IsNullOrWhiteSpace(emailClaim.Value))
                {
                    return null;
                }

                
                return emailClaim.Value;
            }
        }

        public static int CheckClaimForCustomerId(ClaimsPrincipal requesterIdentity)
        {
            var hasClaim = requesterIdentity.HasClaim(c => c.Type == CustomClaimType.Id.ToString());

            if(!hasClaim)
            {
                return 0;
            }
            else
            {
                var idClaim = requesterIdentity.FindFirst(c => c.Type == CustomClaimType.Id.ToString());

                if(string.IsNullOrWhiteSpace(idClaim.Value))
                {
                    return 0;
                }

                if (int.TryParse(idClaim.Value, out int result))
                {
                    return result;
                }
                
                return 0;
            }
        }
    }
}