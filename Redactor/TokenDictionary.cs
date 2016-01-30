using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace JuliaHayward.Redactor
{
    public class Token
    {
        public string RawForm;
        public string RedactedForm;
    }

    public class TokenDictionary
    {
        private Dictionary<string, string> tokens;

        public TokenDictionary()
        {
            tokens = new Dictionary<string, string>();
        }

        public void Add(string raw, string redacted)
        {
            tokens.Add(raw, "**REDACTED" + redacted + "**");
        }

        public void Load()
        {
            // Temporarily hard coded
            if (File.Exists(TokenStorePath))
            {
                using (var stream = new FileStream(TokenStorePath,
                    FileMode.Open, FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof(Token[]),
                                         new XmlRootAttribute() { ElementName = "Tokens" });

                    tokens = ((Token[])serializer.Deserialize(stream))
                       .ToDictionary(i => i.RawForm, i => i.RedactedForm);
                }
            }
        }

        public void Save()
        {
            using (var stream = new FileStream(TokenStorePath,
                    FileMode.OpenOrCreate, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(Token[]),
                                     new XmlRootAttribute() { ElementName = "Tokens" });

                serializer.Serialize(stream,
                  tokens.Select(kv => new Token() { RawForm = kv.Key, RedactedForm = kv.Value }).ToArray());
            }
        }

        public string TokenStorePath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\tokens.xml";
            }
        }
        
        public Dictionary<string, string> RedactionTokens
        {
            get
            {
                return tokens;
            }
        }

        public Dictionary<string, string> UnredactionTokens
        {
            get
            {
                return tokens.GroupBy(x => x.Value, x => x.Key).ToDictionary(g => g.Key, g => g.First());
            }
        }
    }
}
