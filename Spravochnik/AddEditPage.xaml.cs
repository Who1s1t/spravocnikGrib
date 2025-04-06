using System.Collections.ObjectModel;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
namespace Spravochnik
{
    public partial class AddEditPage : ContentPage
    {
        private ObservableCollection<Mushroom> _mushrooms;
        private Mushroom _editing;
        private string _photoPath;

        public AddEditPage(ObservableCollection<Mushroom> mushrooms, Mushroom mushroom = null)
        {
            InitializeComponent();
            _mushrooms = mushrooms;
            _editing = mushroom;

            if (_editing != null)
            {
                NameEntry.Text = _editing.Name;
                DescriptionEditor.Text = _editing.Description;
                _photoPath = _editing.PhotoPath;
                if (!string.IsNullOrEmpty(_photoPath))
                    MushroomImage.Source = ImageSource.FromFile(_photoPath);
            }
        }

        private async void OnPickPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Выберите фото гриба",
                    FileTypes = FilePickerFileType.Images
                });

                if (photo != null)
                {
                    _photoPath = photo.FullPath;
                    MushroomImage.Source = ImageSource.FromFile(_photoPath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_editing == null)
            {
                var newMushroom = new Mushroom
                {
                    Name = NameEntry.Text,
                    Description = DescriptionEditor.Text,
                    PhotoPath = _photoPath
                };
                await App.Database.SaveItemAsync(newMushroom);
                _mushrooms.Add(newMushroom);
            }
            else
            {
                _editing.Name = NameEntry.Text;
                _editing.Description = DescriptionEditor.Text;
                _editing.PhotoPath = _photoPath;
                await App.Database.SaveItemAsync(_editing);
            }

            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_editing != null)
            {
                await App.Database.DeleteItemAsync(_editing);
                _mushrooms.Remove(_editing);
            }
            await Navigation.PopAsync();
        }
    }
}
