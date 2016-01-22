using System.ComponentModel;
using System.Management.Automation;


namespace cmdlet
{
    [Cmdlet("Redact", "File")]
    public class RedactFileCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory=true)]
        public string[] Name // creates a -name argument
        {
            get { return processNames; }
            set { processNames = value; }
        }

        private string[] processNames;

        protected override void ProcessRecord()
        {
            foreach (string name in processNames)
            {
                var fileContents = System.IO.File.ReadAllText(name);

                fileContents = fileContents.Replace("Julia", "REDACTED");

                System.IO.File.WriteAllText(name, fileContents);

            }
        }
    }

    #region PowerShell snap-in

    /// <summary>
    /// Create this sample as an PowerShell snap-in
    /// </summary>
    [RunInstaller(true)]
    public class RedactFilePSSnapIn : PSSnapIn
    {
        /// <summary>
        /// Create an instance of the GetProcPSSnapIn01
        /// </summary>
        public RedactFilePSSnapIn()
            : base()
        {
        }

        /// <summary>
        /// Get a name for this PowerShell snap-in. This name will be used in registering
        /// this PowerShell snap-in.
        /// </summary>
        public override string Name
        {
            get
            {
                return "RedactFileSnapIn";
            }
        }

        /// <summary>
        /// Vendor information for this PowerShell snap-in.
        /// </summary>
        public override string Vendor
        {
            get
            {
                return "Julia";
            }
        }

        /// <summary>
        /// Gets resource information for vendor. This is a string of format: 
        /// resourceBaseName,resourceName. 
        /// </summary>
        public override string VendorResource
        {
            get
            {
                return "RedactFileSnapIn,Julia";
            }
        }

        /// <summary>
        /// Description of this PowerShell snap-in.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This is a PowerShell snap-in that lets you redact configuration files.";
            }
        }
    }

    #endregion PowerShell snap-in
}
