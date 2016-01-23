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

            foreach (var key in dict.UnredactionTokens.Keys.OrderBy(x => x))
            {
                WriteObject(key);
            }
        }
    }
}
