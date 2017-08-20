using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.Navigation;

namespace Zzz.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel, IDisposable
    {
        protected IMvxMessenger Messenger;

        public IMvxLanguageBinder TextSource =>
            new MvxLanguageBinder("", GetType().Name);

        public BaseViewModel(IMvxMessenger messenger)
        {
            Messenger = messenger;
        }

        protected async Task ReloadDataAsync()
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected virtual Task InitializeAsync()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            Messenger = null;
        }
    }

    public class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected IMvxMessenger Messenger;
        protected IMvxNavigationService Navigation;

        public IMvxLanguageBinder TextSource =>
            new MvxLanguageBinder("", GetType().Name);

        public BaseViewModel(IMvxMessenger messenger, IMvxNavigationService navigation)
        {
            Messenger = messenger;
            Navigation = navigation;
        }

        protected async Task ReloadDataAsync()
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public override Task Initialize(TParameter parameter)
        {
            return base.Initialize();
        }

        protected virtual Task InitializeAsync()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            Messenger = null;
            Navigation = null;
        }
    }
}
