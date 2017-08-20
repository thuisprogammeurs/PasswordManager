using MvvmCross.Plugins.Messenger;
using Zzz.Core.Models;

namespace Zzz.Core.Messages
{
    public class PasswordCopiedMessage : MvxMessage
    {
        public PasswordCopiedMessage(object sender) : base(sender)
        {
        }

        public string Password { get; set; }
    }
}
