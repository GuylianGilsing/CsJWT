using System;
using System.Collections.Generic;

using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Hashing;
using GuylianGilsing.JWT.Hashing.Algorithms;

namespace CsJWT
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenSigner signer = new TokenSigner();

            Token token = new Token();
            token.payload.RegisterClaim("Banaan", "Met slagroom!");
            token.hashAlgo = new Sha256Algo();

            string signedToken = signer.Sign(token, new Key("SuperSecretKey"));
            Console.WriteLine($"Signed: { signedToken }");

            // Output the claims
            Console.Write("\n");
            
            Console.WriteLine($"TokenPart: header");
            Console.WriteLine($"ALG - { token.header.GetClaim("alg") }");
            Console.WriteLine($"TYP - { token.header.GetClaim("typ") }");
            
            Console.Write("\n");

            Console.WriteLine($"TokenPart: payload");
            Console.WriteLine($"SUB - { token.payload.GetClaim("sub") }");
            Console.WriteLine($"name - { token.payload.GetClaim("name") }");
            Console.WriteLine($"IAT - { token.payload.GetClaim("iat") }");

            Console.ReadLine();
        }
    }
}
