using System;
using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Verification.Procedures
{
    class VerifyTokenHashProcedure : VerifyProcedure
    {
        public override bool Run(Token a_token)
        {
            bool executedSuccessfully = false;

            // Store the old signature, this will be used in the final comparison
            string oldSignature = a_token.signature;

            // Create a new JWT and check if the signatures match
            Token newlyHashedToken = a_token.hashAlgo.Hash(a_token, a_token.secretKey);
            if(newlyHashedToken.signature == oldSignature)
                executedSuccessfully = true;

            return executedSuccessfully;
        }
    }
}