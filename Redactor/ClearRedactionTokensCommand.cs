using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Clear", "RedactionTokens")]
    public class ClearRedactionTokensCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Save();
        }
    }
}
