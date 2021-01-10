using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArduinoXamarin
{
    public partial class MainPage : ContentPage,INotifyPropertyChanged
    {
        private bool _ledStatus;

        public bool LEDStatus
        {
            get { return _ledStatus; }
            set { SetProperty(ref _ledStatus, value); }
        }

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private async void ButtonTurnOn_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool result = await SwitchLED(1);

                LEDStatus = result;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Info", ex.Message, "Cancel");
            }
        }

        private async void ButtonTurnOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool result = await SwitchLED(0);

                LEDStatus = !result;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Info", ex.Message, "Cancel");
            }
        }

        private async Task<bool> SwitchLED(int state)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://f5527451b69b.eu.ngrok.io/api/");

            HttpResponseMessage response = await httpClient.PostAsync($"Arduino?state={state}", null);

            return response.IsSuccessStatusCode;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
