using GuylianGilsing.JWT.Hashing;

namespace GuylianGilsing.JWT.Tokens
{
    interface ITokenSigner
    {
        public string Sign(Token a_token, Key a_key);
    }
}