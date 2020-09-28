using GuylianGilsing.JWT.Tokens;

namespace GuylianGilsing.JWT.Verification.Procedures
{
    abstract class VerifyProcedure : IVerifyProcedure
    {
        public abstract bool Run(Token a_token);
    }
}