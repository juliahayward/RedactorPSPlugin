using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace JuliaHayward.Redactor
{
    [Cmdlet("Add", "RedactionToken")]
    public class AddRedactionTokenCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Raw (live) string")]
        public string Raw
        {
            get { return raw; }
            set { raw = value; }
        }

        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Redacted (safe) token")]
        public string Redacted
        {
            get { return redacted; }
            set { redacted = value; }
        }

        private string raw;
        private string redacted;

        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Load();

            if (dict.RedactionTokens.ContainsKey(raw))
            {
                this.WriteWarning("Cannot add raw text - already exists");
                return;
            }
            if (dict.UnredactionTokens.ContainsKey(redacted))
            {
                this.WriteWarning("Cannot add redacted token text - already exists");
                return;
            }

            dict.Add(raw, redacted);
            dict.Save();
        }
    }
}
