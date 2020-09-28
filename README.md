# CsJWT
A C# implementation of JsonWebToken (JWT). This project is mostly aimed to implement something while using the SOLID design principles. This implementation is not compatible with other libraries since the hashing algorithm works differently within C#.

**Note:** this implementation is written as an assignment for school. **DO NOT USE THIS IN YOUR OWN PROJECT!**

## How to use
### Create a basic token
```cs
using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Hashing;
using GuylianGilsing.JWT.Hashing.Algorithms;

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
```

### Working with claims
This implementation uses a TokenPart class that acts as either a header or payload token part.
```cs
using GuylianGilsing.JWT.Tokens;

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
```

### Working with hashing algorithms
CsJWT comes with one pre-written hashing algorithm, SHA256. Since this implementation follows the SOLID principles, you are able to write your own algorithms.

#### Writing your own algorithm
The only thing you have to do is create a new class that inherits the abstract `HashAlgo` class. From there you only have to override the `Hash(Token a_token, Key a_key)` method.

An algorithm class takes in, and returns, a token. The token that will be given to the method is changed by your own code and then returned back:
```cs
using System.Text;
using System.Security.Cryptography;

using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Hashing.Algorithms
{
    class Sha256Algo : HashAlgo
    {
        public override Token Hash(Token a_token, Key a_key)
        {
            HMACSHA256 sha256Algo;
            
            if(a_token.header != null && a_token.payload != null)
            {
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
            }

            return a_token;
        }
    }
}
```

#### Using your own algorithm
Using your own algorithm is very simple. You can assign the instantiated class instance of your algorithm straight to the token:
```cs
using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Hashing;
using GuylianGilsing.JWT.Hashing.Algorithms;

// Create a new token
Token token = new Token();

// Set the hashing algorithm
token.hashAlgo = new Sha256Algo();
```

### Token verification
#### Basic overview
You can verify a token by creating a token verifier, and assigning procedures to the verification process. Assigned processes will run in the order that you assign them to the verifier. This implementation comes with 2 pre-written procedures:<br/>
- Basic signature checking
- Expiration time checking
```cs
using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Verification;
using GuylianGilsing.JWT.Verification.Procedures;

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
```

#### Writing your own procedures
Writing your own verification procedure is very simple. You first start out by creating a class that inherits from the abstract `VerifyProcedure` class. After that you only have to override the `Run(Token a_token)` method.
```cs
using GuylianGilsing.JWT.Tokens;

namespace YOUR_NAMESPACE_HERE
{
    class VerifyIssuerProcedure : VerifyProcedure
    {
        private string issuer = "";

        public VerifyIssuerProcedure(string a_issuer)
        {
            this.issuer = a_issuer;
        }

        public override bool Run(Token a_token)
        {
            bool executedSuccessfully = false;

            if(a_token.payload.GetClaim("iss") == this.issuer)
                execyteSuccessfully = true;

            return executedSuccessfully;
        }
    }
}
```

#### Using your own procedures
Once your own procedure has been written, you can use it as follows:
```cs
using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Verification;
using GuylianGilsing.JWT.Verification.Procedures;

// Create a new token verifier
TokenVerifier verifier = new TokenVerifier();

// Register procedures
verifier.RegisterProcedure(new VerifyTokenHashProcedure()); // Basic procedure
verifier.RegisterProcedure(new VerifyIssuerProcedure("https://example.com")); // Our own custom procedure
```