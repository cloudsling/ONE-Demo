using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.Storage;

namespace App1
{
    public class OneSettings : INotifyPropertyChanged
    {
        public OneSettings()
        {
            LocalSettings = ApplicationData.Current.LocalSettings;
        }

        public ApplicationDataContainer LocalSettings { get; set; }

        public void SaveSettings(string key, object value)
        {
            LocalSettings.Values[key] = value;
        }

        private T ReadSettings<T>(string key, T defaultValue)
        {
            if (LocalSettings.Values.ContainsKey(key))
            {
                return (T)LocalSettings.Values[key];
            }
            if (null != defaultValue)
            {
                return defaultValue;
            }
            return default(T);
        }

        public double OneFontSize
        {
            get { return ReadSettings(nameof(OneFontSize), 200.0); }

            set
            {
                SaveSettings(nameof(OneFontSize), value);
                OnPropertyChanged();
            }
        }
        public bool Switch
        {
            get { return ReadSettings(nameof(Switch), true); }

            set
            {
                SaveSettings(nameof(Switch), value);
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var temp = Volatile.Read(ref PropertyChanged);
            if (temp != null) temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
