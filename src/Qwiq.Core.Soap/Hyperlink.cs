using System.Diagnostics;

using Tfs = Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Microsoft.Qwiq.Client.Soap
{
    public class Hyperlink : Qwiq.Hyperlink
    {
        [DebuggerStepThrough]
        internal Hyperlink(Tfs.Hyperlink hyperLink)
            : base(hyperLink.Location, hyperLink.Comment)
        {
        }
    }
}