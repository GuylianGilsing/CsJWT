using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

using GuylianGilsing.JWT.Verification.Procedures;

namespace GuylianGilsing.JWT
{
    static class Tools
    {
        public static string Base64Encode(string a_textToEncode)
        {
            return Base64UrlEncoder.Encode(a_textToEncode);
        }

        public static string Base64Decode(string a_textToDecode)
        {
            return Base64UrlEncoder.Decode(a_textToDecode);
        }

        public static List<VerifyProcedure> GetStandardVerifyProcedures()
        {
            List<VerifyProcedure> procedures = new List<VerifyProcedure>();
            
            // Add procedures here...
            
            return procedures;
        }
    }
}