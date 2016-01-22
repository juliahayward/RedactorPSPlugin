using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Redact", "Configs", HelpUri = "https://github.com/juliahayward/RedactorPSPlugin")]
    public class RedactConfigsCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Name of folder to redact")]
        public string[] Name
        {
            get { return folderNames; }
            set { folderNames = value; }
        }

        private string[] folderNames;

        protected override void ProcessRecord()
        {
            var extensions = new[] {"config", "settings"};
            foreach (string name in folderNames)
            {
                var files = Directory.GetFiles(name).Where(x => extensions.Any(x.ToLower().EndsWith)).ToList();
                foreach (var file in files)
                {
                    WriteVerbose("Redacting " + name);
                    var fileContents = System.IO.File.ReadAllText(file);

                    foreach (var token in TokenDictionary.RedactionTokens.Keys)
                        fileContents = fileContents.Replace(token, TokenDictionary.RedactionTokens[token]);

                    System.IO.File.WriteAllText(name, fileContents);
                }
            }
        }
    }
}
