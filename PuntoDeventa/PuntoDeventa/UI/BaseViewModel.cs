using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PuntoDeventa.IU
{
    [Preserve(AllMembers = true)]
    [DataContract]
    internal class BaseViewModel : INotifyPropertyChanged
    {

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        #region Event handler

        /// <summary>
        /// Ocurre cuando un propiedad cambia.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Methods

        /// <summary>
        /// Notifica a la vista cuando una propiedad ha cambiado.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Establece el nuevo valor de una propiedad ej: SetProperty(ref _property, value)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Ejecuta el NavigationBack, para retornar a la vista anterior.
        /// </summary>
        public async void NavigationBack(Type pageCurrentType, string backDataPass = null)
        {
            var uri = Shell.Current.CurrentState.Location.OriginalString;
            var remove = pageCurrentType.Name.Length + 1;
            var path = uri.Remove(uri.Length - remove).Replace("//", "///");
            if (!string.IsNullOrEmpty(backDataPass))
            {
                await Shell.Current.GoToAsync($"{path}?{backDataPass}");
            }
            else
            {
                await Shell.Current.GoToAsync($"{path}");
            }

        }

        #endregion

    }
}
