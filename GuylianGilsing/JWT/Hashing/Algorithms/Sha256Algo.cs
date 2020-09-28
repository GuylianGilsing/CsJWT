using System;
using System.Text;
using System.Security.Cryptography;

using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Hashing.Algorithms
{
    class Sha256Algo : HashAlgo
    {
        /// <summary>
        /// Hashes an existing token and returns a fully build JWT string.
        /// </summary>
        /// <param name="a_token">
        /// A token object with data in it.
        /// </param>
        /// <param name="a_key">
        /// A key object with a secret.
        /// </param>
        public override Token Hash(Token a_token, Key a_key)
        {
            HMACSHA256 sha256Algo;
            
            if(a_key != null)
            {
                byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(a_key.secret);
                sha256Algo = new HMACSHA256(keyBytes);
            }
            else
            {
                sha256Algo = new HMACSHA256();
            }

            // Register the alg claim with the given token
            a_token.header.RegisterClaim("alg", "HS256");

            // Construct the header and payload parts of the token
            string partialToken = $"{ a_token.header.ToString() }.{ a_token.payload.ToString() }";

            // Hash the partial token + add a salt to make it impossible for any lookup/rainbow tables
            // to crack the hash
            byte[] hashedBytes = sha256Algo.ComputeHash(Encoding.UTF8.GetBytes(partialToken));

            // Construct the signature string from the hashed bytes
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < hashedBytes.Length; i += 1)
            {
                stringBuilder.Append(hashedBytes[i]);
            }

            // Update the signature of the given token object
            string signature = Tools.Base64Encode(stringBuilder.ToString());
            a_token.signature = signature;

            return a_token;
        }
    }
}