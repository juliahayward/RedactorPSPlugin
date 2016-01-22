using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Unredact", "Configs")]
    public class UnredactConfigsCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Name of folder to unredact")]
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

            var extensions = new[] { "config", "settings", "designer.cs" };
            foreach (string name in folderNames)
            {
                var files = Directory.GetFiles(name).Where(x => extensions.Any(x.ToLower().EndsWith)).ToList();
                foreach (var file in files)
                {
                    WriteVerbose("Unredacting " + file);
                    var fileContents = System.IO.File.ReadAllText(file);

                    foreach (var token in dict.UnredactionTokens.Keys)
                        fileContents = fileContents.Replace(token, dict.UnredactionTokens[token]);

                    System.IO.File.WriteAllText(file, fileContents);
                }
            }
        }
    }
}
