using System.IO;
using System.Management.Automation;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Unredact", "File")]
    public class UnredactFileCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Name of file(s) to unredact")]
        public string[] Name { get; set; }

        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Load();

            foreach (string name in Name)
            {
                WriteVerbose("Unredacting " + name);
                var fileContents = File.ReadAllText(name);

                foreach (var token in dict.UnredactionTokens.Keys)
                    fileContents = fileContents.Replace(token, dict.UnredactionTokens[token]);

                File.WriteAllText(name, fileContents);
            }
        }
    }
}
