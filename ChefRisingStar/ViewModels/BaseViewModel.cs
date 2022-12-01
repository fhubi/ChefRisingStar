using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace ChefRisingStar.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Service

        private HttpClient _httpClient;
        protected HttpClient Client => _httpClient ?? (_httpClient = new HttpClient());

        #endregion

        bool isBusy = false;
        public bool IsNotBusy => !IsBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                SetProperty(ref _isDirty, value);
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private User _user;
        public User CurrentUser
        {
            get { return _user; }
            protected set { SetProperty(ref _user, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            IsDirty = true;
            return true;
        }

        #region Constructors


        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
