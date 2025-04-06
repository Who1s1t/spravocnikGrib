using System;
using System.IO;

namespace Spravochnik
{
    public partial class App : Application
    {
        private static MushroomDatabase _database;
        public static MushroomDatabase Database => _database ??= new MushroomDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mushrooms.db3"));

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
