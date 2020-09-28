namespace GuylianGilsing.JWT.Tokens
{
    interface IClaimHandler
    {
        public void RegisterClaim(string a_name, string a_value);
        public void UnregisterClaim(string a_name);
        public string GetClaim(string a_name);
    }
}