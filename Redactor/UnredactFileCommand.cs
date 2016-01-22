using System.Management.Automation;


namespace JuliaHayward.Redactor
{
    [Cmdlet("Unredact", "File", HelpUri = "https://github.com/juliahayward/RedactorPSPlugin")]
    public class UnredactFileCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Name of file(s) to unredact")]
        public string[] Name
        {
            get { return fileNames; }
            set { fileNames = value; }
        }

        private string[] fileNames;

        protected override void ProcessRecord()
        {
            foreach (string name in fileNames)
            {
                WriteVerbose("Unredacting " + name);
                var fileContents = System.IO.File.ReadAllText(name);

                foreach (var token in TokenDictionary.UnredactionTokens.Keys)
                    fileContents = fileContents.Replace(token, TokenDictionary.UnredactionTokens[token]);

                System.IO.File.WriteAllText(name, fileContents);

            }
        }
    }
}
