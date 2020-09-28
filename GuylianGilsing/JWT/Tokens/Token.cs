using System;
using System.Text.Json;
using System.Collections.Generic;

using GuylianGilsing.JWT.Hashing;

namespace GuylianGilsing.JWT.Tokens
{
    class Token
    {
        public Key secretKey = null;
        public TokenPart header = null;
        public TokenPart payload = null;
        public string signature = "";

        public HashAlgo hashAlgo = null;

        public Token(string a_parsedToken)
        {
            string[] tokenParts = this.SeperateTokenIntoParts(a_parsedToken);

            // Construct the different token parts
            TokenPart header = new TokenPart();
            TokenPart payload = new TokenPart();

            this.header = this.ParseClaimsFromString(tokenParts[0], header);
            this.payload = this.ParseClaimsFromString(tokenParts[1], payload);
            this.signature = tokenParts[2];
        }

        public Token()
        {
            this.header = new TokenPart();
            this.payload = new TokenPart();

            this.header.RegisterClaim("typ", "JWT");
        }

        public Token(TokenPart a_header, TokenPart a_payload)
        {
            this.header = a_header;
            this.payload = a_payload;
        }

        private string[] SeperateTokenIntoParts(string a_token)
        {
            return a_token.Split('.');
        }

        private TokenPart ParseClaimsFromString(string a_claimJson, TokenPart a_tokenPart)
        {
            string claimJson = Tools.Base64Decode(a_claimJson);

            // Add the claims to the given TokenPart
            Dictionary<string, dynamic> claims = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(claimJson);
            if(claims.Keys.Count > 0)
            {
                foreach(KeyValuePair<string, dynamic> entry in claims)
                {
                    a_tokenPart.RegisterClaim(entry.Key, Convert.ToString(entry.Value));
                }
            }

            return a_tokenPart;
        }
    }
}