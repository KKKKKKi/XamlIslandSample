namespace cs2019.ViewModels
{
    using Caliburn.Micro;
    using Services;

    public class ShellViewModel : Screen, ICleanup
    {
        private string _yourName = "";

        public string YourName
        {
            get => _yourName;
            set
            {
                if (Set(ref _yourName, value))
                {
                    CanSayHello = !string.IsNullOrEmpty(_yourName);
                }
            }
        }

        private bool _canSayHello = false;

        public bool CanSayHello
        {
            get => _canSayHello;
            private set => Set(ref _canSayHello, value);
        }

        public ShellViewModel()
        {
            DisplayName = "UWP";
        }

        public void Cleanup()
        {
            // TODO: cleanup
        }
    }
}
