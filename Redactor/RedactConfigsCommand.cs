using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Redact", "Configs")]
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
            var dict = new TokenDictionary();
            dict.Load();

            var extensions = new[] {"config", "settings"};
            foreach (string name in folderNames)
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
