using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Spravochnik
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Mushroom> Mushrooms { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            var items = await App.Database.GetItemsAsync();
            Mushrooms.Clear();
            foreach (var mushroom in items)
                Mushrooms.Add(mushroom);

            MushroomList.ItemsSource = Mushrooms;
        }

        private async void OnAddMushroom(object sender, EventArgs e)
        {
      
            await Navigation.PushAsync(new AddEditPage(Mushrooms));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection.FirstOrDefault() is Mushroom selected)
            {
                await Navigation.PushAsync(new AddEditPage(Mushrooms, selected));
            }
        }
    }

}
