using ThemedDialog.Core;
using ThemedDialog.Designer.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        private void AddAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Add();
            AddFlyout.Hide();
        }

        private void EditEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Edit();
            EditFlyout.Hide();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete();
        }
    }
}