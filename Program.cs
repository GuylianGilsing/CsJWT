using System;
using System.Collections.Generic;

using GuylianGilsing.JWT.Tokens;

namespace CsJWT
{
    class Program
    {
        static void Main(string[] args)
        {
            Token token = new Token("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
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
