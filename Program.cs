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
            // Create a new token
            Token token = new Token();

            // Register payload claims
            token.payload.RegisterClaim("iss", "https://example.com");

            // Set the hashing algorithm
            token.hashAlgo = new Sha256Algo();

            // Set the secret key
            token.secretKey = new Key("MyKey");

            // Sign the token
            TokenSigner signer = new TokenSigner();
            string signedToken = signer.Sign(token);

            Console.WriteLine(signedToken);

            Console.ReadLine();
        }

        private static void CreateMostBasicTokenSnippet()
        {
            // using GuylianGilsing.JWT.Tokens;
            // using GuylianGilsing.JWT.Hashing;
            // using GuylianGilsing.JWT.Hashing.Algorithms;

            // Create a new token
            Token token = new Token();

            // Register payload claims
            token.payload.RegisterClaim("iss", "https://example.com");
            token.payload.RegisterClaim("sub", "code_snippet");
            token.payload.RegisterClaim("aud", "github");

            // Set the hashing algorithm
            token.hashAlgo = new Sha256Algo();

            // Set the secret key
            token.secretKey = new Key("MySuperSecretKey");

            // Sign the token
            TokenSigner signer = new TokenSigner();
            string signedToken = signer.Sign(token);

            // Sign the token with a seperate key
            string otherSignedToken = signer.Sign(token, new Key("OtherKey"));
        }

        private static void ClaimsSnippet()
        {
            // using GuylianGilsing.JWT.Tokens;

            // Create a new tokenpart (header / payload)
            TokenPart tokenPart = new TokenPart();

            // Register a claim
            tokenPart.RegisterClaim("test", "My Claim");

            // Retrieve a registered claim
            tokenPart.GetClaim("test");

            // Unregister a claim
            tokenPart.UnregisterClaim("test");

            // Retrieves the JSON version of the token part
            tokenPart.ToJson();
        }

        private static void VerifyTokenSnippet()
        {
            // using GuylianGilsing.JWT.Tokens;
            // using GuylianGilsing.JWT.Verification;
            // using GuylianGilsing.JWT.Verification.Procedures;

            // Construct a verifiable token
            Token token = new Token("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2V4YW1wbGUuY29tIn0.MTM4MTYwMjA2OTEyMDA4NzE5NzkzMDgyMjEyNzk5NjE0MDczMTA0MTQ2MTcwMjU1MTE5MjM0MjE3MTI1MjE1MjQ5NDc3NzE3NzE5ODUyMzgyNDc");
            token.secretKey = new Key("MySuperSecretKey");
            token.hashAlgo = new Sha256Algo();

            // Create a new token verifier
            TokenVerifier verifier = new TokenVerifier();

            // Register procedures
            verifier.RegisterProcedure(new VerifyTokenHashProcedure());

            // Verify the token
            verifier.IsValid(token);
        }
    }
}
