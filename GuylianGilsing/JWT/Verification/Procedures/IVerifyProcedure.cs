using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Verification.Procedures
{
    interface IVerifyProcedure
    {
        public bool Run(Token a_token);
    }
}