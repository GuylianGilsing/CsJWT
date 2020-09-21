using Microsoft.IdentityModel.Tokens;

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
    }
}