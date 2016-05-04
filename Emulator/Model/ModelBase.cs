using System.ComponentModel;
using System.Runtime.CompilerServices;
using Emulator.Properties;

namespace Emulator.Model
{
    /// <summary>
    /// Базовый класс для модели
    /// </summary>
    public class ModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}