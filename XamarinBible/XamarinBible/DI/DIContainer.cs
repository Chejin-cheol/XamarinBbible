using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinBible.DI.Service;
using XamarinBible.Page;
using XamarinBible.ViewModel;

namespace XamarinBible.DI
{
    public class DIContainer
    {
        private static IContainer _container = null;
        public IContainer Container
        {
            get => _container;
        }
        public DIContainer()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (_container == null)
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.RegisterType<TabbedPageViewModel>().SingleInstance().AsSelf();
                builder.RegisterType<BibleViewModel>().AsSelf();
                builder.RegisterType<SermonViewModel>().SingleInstance().AsSelf();
                builder.RegisterType<HymnViewModel>().AsSelf();
                builder.RegisterType<SermonService>().As<ISermonService>();
                builder.RegisterType<KoreanMatchService>().SingleInstance().As<IKoreanMatchService>();
                _container = builder.Build();
            }
        }
        public ISermonService SermonService
        {
            get => _container.Resolve<ISermonService>();
        }
        public IKoreanMatchService KoreanMatchService
        {
            get => _container.Resolve<IKoreanMatchService>();
        }

        public TabbedPageViewModel TabbedViewModel
        {
            get => _container.Resolve<TabbedPageViewModel>();
        }
        public BibleViewModel BibleViewModel
        {
            get => _container.Resolve<BibleViewModel>();
        }
        public SermonViewModel SermonViewModel
        {
            get => _container.Resolve<SermonViewModel>();
        }
        public HymnViewModel HymnViewModel
        {
            get => _container.Resolve<HymnViewModel>();
        }
    }
}
