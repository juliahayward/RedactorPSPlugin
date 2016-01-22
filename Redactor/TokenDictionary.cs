using System.Collections.Generic;

namespace JuliaHayward.Redactor
{
    public class TokenDictionary
    {
        public static Dictionary<string, string> RedactionTokens
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("Julia", "REDACTED");
                return dictionary;
            }
        }

        public static Dictionary<string, string> UnredactionTokens
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("REDACTED", "Julia");
                return dictionary;
            }
        }
    }
}
