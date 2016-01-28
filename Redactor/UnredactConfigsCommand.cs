using System.IO;
using System.Linq;
using System.Management.Automation;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Unredact", "Configs")]
    public class UnredactConfigsCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Name of folder(s) to unredact")]
        public string[] Name { get; set; }

        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Load();

            var extensions = new[] { "config", "settings", "designer.cs" };
            foreach (string name in Name)
            {
                var files = Directory.GetFiles(name, "*.*", SearchOption.AllDirectories)
                    .Where(x => extensions.Any(x.ToLower().EndsWith)).ToList(); 
                foreach (var file in files)
                {
                    WriteVerbose("Unredacting " + file);
                    var fileContents = File.ReadAllText(file);

                    foreach (var token in dict.UnredactionTokens.Keys)
                        fileContents = fileContents.Replace(token, dict.UnredactionTokens[token]);

                    File.WriteAllText(file, fileContents);
                }
            }
        }
    }
}
