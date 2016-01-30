using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Show", "RedactionTokens")]
    public class ShowRedactionTokensCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Load();
            WriteVerbose("Tokens stored in " + dict.TokenStorePath);

            foreach (var token in dict.UnredactionTokens.OrderBy(x => x.Key))
            {
                WriteObject(token);
            }
        }
    }
}
