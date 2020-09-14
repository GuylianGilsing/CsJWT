using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Hashing
{
    abstract class HashAlgo : IHashAlgo
    {
        public string algKey {get; protected set;}

        public abstract Token Hash(Token a_token, Key a_key);
    }
}