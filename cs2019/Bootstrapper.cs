namespace cs2019
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Threading;
    using Services;
    using ViewModels;

    public class Bootstrapper : BootstrapperBase, ICleanup
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance<SimpleContainer>(_container);

            _container.Singleton<IWindowManager, WindowManager>(nameof(WindowManager))
                .Singleton<IEventAggregator, EventAggregator>(nameof(EventAggregator));

            _container.Singleton<ShellViewModel>(nameof(ShellViewModel));
        }

        protected override void OnStartup(object s, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void OnExit(object s, EventArgs e)
        {
            Cleanup();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        /// <summary>
        /// 全局错误处理
        /// </summary>
        /// <param name="s">sender</param>
        /// <param name="e">event args</param>
        protected override void OnUnhandledException(object s, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "(^_−)≡★", MessageBoxButton.OK);
        }

        public void Cleanup()
        {
            // TODO: Cleanup
            IoC.Get<ShellViewModel>(nameof(ShellViewModel)).Cleanup();
        }
    }
}
