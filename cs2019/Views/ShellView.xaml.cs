namespace cs2019.Views
{
    using Caliburn.Micro;
    using Microsoft.Toolkit.Wpf.UI.XamlHost;
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Data;
    using ViewModels;

    public partial class ShellView
    {
        private readonly ShellViewModel _vm = IoC.Get<ShellViewModel>(nameof(ShellViewModel));

        public ShellView()
        {
            InitializeComponent();
        }

        private void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            WindowsXamlHost host = sender as WindowsXamlHost;

            if (host == null)
                return;

            if (host.Child is TextBox tb)
            {
                tb.PlaceholderText = "Enter your name";
                tb.Width = 120;
                tb.Height = 32;
                tb.Margin = new Windows.UI.Xaml.Thickness(2);

                Binding binding = new Binding
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Source = _vm,
                    Path = new Windows.UI.Xaml.PropertyPath("YourName")
                };
                BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);
            }
            else if (host.Child is Button btn)
            {
                btn.Content = "Say Hello";
                btn.Width = 80;
                btn.Height = 32;
                btn.Margin = new Windows.UI.Xaml.Thickness(2);
                btn.Click += async (_, __) => { await new Windows.UI.Popups.MessageDialog($"Hello {_vm?.YourName}", "UWP 消息框").ShowAsync(); };

                Binding binding = new Binding()
                {
                    Mode = BindingMode.OneWay,
                    Source = DataContext,
                    Path = new Windows.UI.Xaml.PropertyPath("CanSayHello")
                };
                BindingOperations.SetBinding(btn, Button.IsEnabledProperty, binding);
            }
            else
            {
                // 
            }
        }
    }
}
