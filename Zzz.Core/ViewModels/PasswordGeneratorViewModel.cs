using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;

namespace Zzz.Core.ViewModels
{
    public class PasswordGeneratorViewModel : BaseViewModel, IPasswordGeneratorViewModel
    {
        public PasswordGeneratorViewModel(IMvxMessenger messenger) : base(messenger)
        {
        }
    }
}
