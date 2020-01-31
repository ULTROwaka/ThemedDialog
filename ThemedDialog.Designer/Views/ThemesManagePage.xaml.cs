using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThemedDialog.Core;
using ThemedDialog.Designer.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ThemedDialog.Designer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThemesManagePage : Page
    {
        public ThemeManageViewModel ViewModel;
        public ThemesManagePage()
        {
            this.InitializeComponent();
            ViewModel = new ThemeManageViewModel(new DialogCharacter[] 
            { 
                new DialogCharacter("Ivan",null),
                new DialogCharacter("Bob",null),
                new DialogCharacter("Kain",null)
            }, new Theme[] 
            {
                new Theme("Sweet roll"),
                new Theme("What?"),
                new Theme("Bandits")
            });
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void EditEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Edit();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete();
        }

        private void AddAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Add();
        }
    }
}
