using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;

namespace Zzz.Core.ViewModels
{
    public class PasswordGeneratorViewModel : BaseViewModel, IPasswordGeneratorViewModel
    {
        public PasswordGeneratorViewModel(IMvxMessenger messenger) : base(messenger)
        {
        }

        public IMvxCommand CloseCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }
    }
}
