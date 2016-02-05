using System.IO;
using System.Linq;
using System.Management.Automation;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Redact", "Configs")]
    public class RedactConfigsCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Name of folder(s) to redact")]
        public string[] Name { get; set; }

        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Load();

            var extensions = new[] {"config", "settings", "designer.cs", ".dbml"};
            foreach (string name in Name)
            {
                var files = Directory.GetFiles(name, "*.*", SearchOption.AllDirectories)
                    .Where(x => extensions.Any(x.ToLower().EndsWith)).ToList();
                foreach (var file in files)
                {
                    WriteVerbose("Redacting " + file);
                    var fileContents = File.ReadAllText(file);

                    foreach (var token in dict.RedactionTokens.Keys)
                        fileContents = fileContents.Replace(token, dict.RedactionTokens[token]);

                    File.WriteAllText(file, fileContents);
                }
            }
        }
    }
}
