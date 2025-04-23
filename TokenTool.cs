namespace Session1WebApi.Data
{
    public class TokenTool
    {
        public static Dictionary<string, string> tokenStore = new Dictionary<string, string>();
        public static Dictionary<string, string> sessionStore = new Dictionary<string, string>();

        public static string CreateToken(string email)
        {
            var token = Guid.NewGuid().ToString();
            tokenStore[token] = email;
            return token;
        }

        public static bool CheckToken(string token)
        {
            return tokenStore.ContainsKey(token);
        }

        public static string CreateSessionId(string email)
        {
            var seesionId = Guid.NewGuid().ToString();
            sessionStore[seesionId] = email;
            return seesionId;
        }
    }
}
