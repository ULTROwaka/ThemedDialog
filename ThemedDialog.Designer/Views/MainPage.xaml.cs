using System;

using ThemedDialog.Core;
using ThemedDialog.Designer.Repositories;
using ThemedDialog.Designer.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ThemedDialog.Designer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly MainPageViewModel ViewModel;
        private readonly Repository _repository;

        public MainPage()
        {
            this.InitializeComponent();

            ViewModel = new MainPageViewModel();

            this.DataContext = ViewModel;

            //TODO Replace with data loader
            _repository = new Repository(new Theme[]
            {
                new Theme("Sweet roll"),
                new Theme("What?"),
                new Theme("Bandits")
            },
            Array.Empty<Variable>(),
            new DialogCharacter[]
            {
                new DialogCharacter("Ivan",null),
                new DialogCharacter("Bob",null),
                new DialogCharacter("Kain",null)
            });
        }

        private void ManageThemesListBtn_Click(object sender, RoutedEventArgs e)
        {
            var param = new ThemesManagePageParameter()
            {
                Repository = _repository
            };
            Frame.Navigate(typeof(ThemesManagePage), param);
        }

        private void ManageVariablesListBtn_Click(object sender, RoutedEventArgs e)
        {
            var param = new VariablesManagePageParameter()
            {
                Repository = _repository
            };
            Frame.Navigate(typeof(VariablesManagePage), param);
        }
    }
}