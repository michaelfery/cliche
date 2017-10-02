﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Cliche.Fluent.Models;
using Cliche.Fluent.Services;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Cliche.Fluent.Views
{
    public sealed partial class MoviesPage : Page, INotifyPropertyChanged
    {
        private Movie _selected;

        public Movie Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<Movie> SampleItems { get; private set; } = new ObservableCollection<Movie>();

        public MoviesPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            SampleItems.Clear();

            var data = await SampleDataService.GetAllMovies();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e?.ClickedItem as Movie;
            if (item != null)
            {
                Selected = item;
                MasterListView.PrepareConnectedAnimation("movieImage", item, "MovieThumbImage");
                NavigationService.Navigate<Views.MoviesDetailPage>(item);

                //TODO Navigation transition request
                //NavigationService.Navigate<Views.CharactersPageDetailPage>(item, new DrillInNavigationTransitionInfo());
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