using System;
using System.Collections.Generic;

using GuylianGilsing.JWT.Tokens;
using GuylianGilsing.JWT.Verification.Procedures;

namespace GuylianGilsing.JWT.Verification
{
    class TokenVerifier : ITokenVerifier
    {
        private List<VerifyProcedure> verifyProcedures = new List<VerifyProcedure>();

        public bool IsValid(Token a_token)
        {
            bool isValid = true;

            // Run each procedure
            foreach(VerifyProcedure procedure in this.verifyProcedures)
            {
                bool procedureExecutedSuccessfully = procedure.Run(a_token);

                // Make sure that every procedure has been executed successfully
                if(!procedureExecutedSuccessfully)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        public void RegisterProcedure(VerifyProcedure a_procedure)
        {
            this.verifyProcedures.Add(a_procedure);
        }

        public void RegisterProcedure(List<VerifyProcedure> a_procedures)
        {
            foreach(VerifyProcedure procedure in a_procedures)
            {
                this.verifyProcedures.Add(procedure);
            }
        }
    }
}