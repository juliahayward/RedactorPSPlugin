using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace JuliaHayward.Redactor
{
    public class Token
    {
        [XmlAttribute]
        public string RawForm;
        [XmlAttribute]
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
            tokens.Add(raw, redacted);
        }

        public void Load()
        {
            if (File.Exists(@"C:\Users\jhayward\AppData\Local\Redactor\tokens.xml"))
            {
                using (var stream = new FileStream(@"C:\Users\jhayward\AppData\Local\Redactor\tokens.xml",
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
            Directory.CreateDirectory(@"C:\Users\jhayward\AppData\Local\Redactor");

            using (var stream = new FileStream(@"C:\Users\jhayward\AppData\Local\Redactor\tokens.xml",
                    FileMode.OpenOrCreate, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(Token[]),
                                     new XmlRootAttribute() { ElementName = "Tokens" });

                serializer.Serialize(stream,
                  tokens.Select(kv => new Token() { RawForm = kv.Key, RedactedForm = kv.Value }).ToArray());
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
