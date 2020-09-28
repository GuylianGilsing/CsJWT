using GuylianGilsing.JWT.Hashing;

namespace GuylianGilsing.JWT.Tokens
{
    class TokenSigner : ITokenSigner
    {
        public string Sign(Token a_token, Key a_key)
        {
            string signedToken = "";

            if(a_token.hashAlgo != null && a_token.header != null && a_token.payload != null)
            {
                Token hashedToken = a_token.hashAlgo.Hash(a_token, a_key);

                string header = Tools.Base64Encode(hashedToken.header.ToJson());
                string payload = Tools.Base64Encode(hashedToken.payload.ToJson());
                
                signedToken = $"{ header }.{ payload }.{ hashedToken.signature }";
            }

            return signedToken;
        }
    }
}