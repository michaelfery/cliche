﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Cliche.Fluent.Models;
using Cliche.Fluent.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Cliche.Fluent.Views
{
    public sealed partial class CharactersPageDetailPage : Page, INotifyPropertyChanged
    {
        private Character _item;

        public Character Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public CharactersPageDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Item = e.Parameter as Character;
            base.OnNavigatedTo(e);

            ConnectedAnimation imageAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("characterImage");
            if (imageAnimation != null)
            {
                imageAnimation.TryStart(CharacterImage);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
