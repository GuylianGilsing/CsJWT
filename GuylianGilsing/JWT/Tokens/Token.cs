using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

using GuylianGilsing.JWT.Hashing;

namespace GuylianGilsing.JWT.Tokens
{
    abstract class Token
    {
        public TokenPart header;
        public TokenPart payload;
        public string signature;

        public HashAlgo hashAlgo;

        public Token(string a_parsedToken)
        {
            string[] tokenParts = this.SeperateTokenIntoParts(a_parsedToken);

            TokenPart header = new TokenPart();
            TokenPart payload = new TokenPart();

            this.header = this.ParseClaimsFromString(tokenParts[0], header);
            this.payload = this.ParseClaimsFromString(tokenParts[1], payload);
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
            // Convert the header from base64 into a byte array
            byte[] jsonBytes = Convert.FromBase64String(a_claimJson);
            
            // Turn the byte array into a string
            string claimJson = ASCIIEncoding.ASCII.GetString(jsonBytes);

            // Add the claims to the given TokenPart
            Dictionary<string, string> claims = JsonSerializer.Deserialize<Dictionary<string, string>>(claimJson);
            if(claims.Keys.Count > 0)
            {
                foreach(KeyValuePair<string, string> entry in claims)
                {
                    a_tokenPart.RegisterClaim(entry.Key, entry.Value);
                }
            }

            return a_tokenPart;
        }
    }
}