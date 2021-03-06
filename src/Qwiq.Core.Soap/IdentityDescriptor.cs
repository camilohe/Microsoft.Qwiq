using Tfs = Microsoft.TeamFoundation.Framework.Client;

namespace Microsoft.Qwiq.Client.Soap
{
    public class IdentityDescriptor : Qwiq.IdentityDescriptor
    {
        internal IdentityDescriptor(Tfs.IdentityDescriptor descriptor)
            : base(descriptor?.IdentityType, descriptor?.Identifier)
        {
        }
    }
}