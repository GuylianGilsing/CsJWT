using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Verification.Procedures;

namespace GuylianGilsing.JWT.Verification
{
    interface ITokenVerifier
    {
        public bool IsValid(Token a_token);
        public void RegisterProcedure(VerifyProcedure a_procedure);
    }
}