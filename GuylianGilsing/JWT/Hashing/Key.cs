using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Hashing
{
    abstract class Key
    {
        public string secret = "YOUR_SECRET_HERE";

        public Key(string a_secret)
        {
            this.secret = a_secret;
        }
    }
}