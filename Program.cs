using System;
using System.Collections.Generic;

using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Hashing;
using GuylianGilsing.JWT.Verification;
using GuylianGilsing.JWT.Hashing.Algorithms;
using GuylianGilsing.JWT.Verification.Procedures;

namespace CsJWT
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenSigner signer = new TokenSigner();
            Key validKey = new Key("SuperSecretKey");
            Key inValidKey = new Key("Banaan");

            // long expirationTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 10;

            Token token = new Token();
            token.payload.RegisterClaim("Banaan", "Met slagroom!");
            // token.payload.RegisterClaim("exp", Convert.ToString(expirationTime));
            token.hashAlgo = new Sha256Algo();

            string signedToken = signer.Sign(token, validKey);
            Console.WriteLine($"Signed: { signedToken }");

            Console.ReadLine();

            TokenVerifier verifier = new TokenVerifier();
            verifier.RegisterProcedure(new VerifyTokenHashProcedure());
            verifier.RegisterProcedure(new VerifyTokenTimeoutProcedure());

            // Token tokenWithValidSecret = new Token(signedToken);
            Token tokenWithValidSecret = new Token("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJCYW5hYW4iOiJNZXQgc2xhZ3Jvb20hIiwiZXhwIjoiMTYwMTI4NTIyNyJ9.NzcyMzYyMzkyMzE5ODM2MjQ1MTk3MTU5MjM2MTUyNTU4MDEyMzE3MzI4MjAxMTAxMjA4MTQyMTIxNDI0MjY0MTg1MTI4MjQ3MzEyMTY0ODU5MjI1");
            // tokenWithValidSecret.secretKey = validKey;
            tokenWithValidSecret.hashAlgo = new Sha256Algo();

            Console.WriteLine(verifier.IsValid(tokenWithValidSecret));

            // // Output the claims
            // Console.Write("\n");
            
            // Console.WriteLine($"TokenPart: header");
            // Console.WriteLine($"ALG - { token.header.GetClaim("alg") }");
            // Console.WriteLine($"TYP - { token.header.GetClaim("typ") }");
            
            // Console.Write("\n");

            // Console.WriteLine($"TokenPart: payload");
            // Console.WriteLine($"SUB - { token.payload.GetClaim("sub") }");
            // Console.WriteLine($"name - { token.payload.GetClaim("name") }");
            // Console.WriteLine($"IAT - { token.payload.GetClaim("iat") }");

            Console.ReadLine();
        }
    }
}
