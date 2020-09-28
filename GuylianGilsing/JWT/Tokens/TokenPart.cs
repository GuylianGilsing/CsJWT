using System.Text.Json;
using System.Collections.Generic;

namespace GuylianGilsing.JWT.Tokens
{
    class TokenPart : ITokenPart, IClaimHandler
    {
        private Dictionary<string, string> claims = new Dictionary<string, string>();

        public void RegisterClaim(string a_name, string a_value)
        {
            if(!this.claims.ContainsKey(a_name))
            {
                this.claims.Add(a_name, a_value);
            }
            else
            {
                this.claims[a_name] = a_value;
            }
        }

        public void UnregisterClaim(string a_name)
        {
            if(this.claims.ContainsKey(a_name))
                this.claims.Remove(a_name);
        }

        public string GetClaim(string a_name)
        {
            string claim = "";

            if(this.claims.ContainsKey(a_name))
                claim = this.claims[a_name];

            return claim;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this.claims);
        }
    }
}