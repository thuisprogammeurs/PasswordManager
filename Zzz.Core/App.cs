using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Zzz.Core.Utility;
using Zzz.Localization;
using ExpressMapper;
using Zzz.Core.Models;
using Zzz.Core.Models.Orm;

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

            RegisterAppStart(new AppStart());
        }

        private void MappingRegistration()
        {
            Mapper.Register<PasswordOrm, Password>();
            Mapper.Register<GroupOrm, Group>();
        }
    }
}
