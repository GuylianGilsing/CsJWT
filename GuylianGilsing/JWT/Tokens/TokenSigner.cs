using GuylianGilsing.JWT.Hashing;

namespace GuylianGilsing.JWT.Tokens
{
    class TokenSigner : ITokenSigner
    {
        public string Sign(Token a_token, Key a_key)
        {
            Token hashedToken = a_token.hashAlgo.Hash(a_token, a_key);

            string header = Tools.Base64Encode(hashedToken.header.ToJson());
            string payload = Tools.Base64Encode(hashedToken.payload.ToJson());

            return $"{ header }.{ payload }.{ hashedToken.signature }";
        }
    }
}