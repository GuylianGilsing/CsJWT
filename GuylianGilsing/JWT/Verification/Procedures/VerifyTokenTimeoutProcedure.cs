using System;
using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Verification.Procedures
{
    class VerifyTokenTimeoutProcedure : VerifyProcedure
    {
        public override bool Run(Token a_token)
        {
            bool executedSuccessfully = false;

            string expirationClaim = a_token.payload.GetClaim("exp");
            if(expirationClaim.Length > 0)
            {
                long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if(currentUnixTime <= Convert.ToInt64(expirationClaim))
                    executedSuccessfully = true;
            }

            return executedSuccessfully;
        }
    }
}