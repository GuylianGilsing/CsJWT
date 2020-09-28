using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Hashing
{
    interface IHashAlgo
    {
        public Token Hash(Token a_token, Key a_key);
    }
}