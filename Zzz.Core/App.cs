using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Zzz.Core.Utility;
using Zzz.Localization;
using ExpressMapper;
using Zzz.Core.Models;
using Zzz.Core.Models.Orm;
using Zzz.Core.Services.Login;
using Zzz.Core.Contracts.Services;

namespace Zzz.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IMvxTextProvider>
                (new ResxTextProvider(Strings.ResourceManager));

            MappingRegistration();

            ILoginService _loginService = new LoginService();

            RegisterAppStart(new AppStart(_loginService));
        }

        private void MappingRegistration()
        {
            Mapper.Register<PasswordOrm, Password>();
            Mapper.Register<GroupOrm, Group>();
            Mapper.Register<PasswordGeneratorOrm, PasswordGenerator>();
        }
    }
}
