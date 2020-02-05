using ThemedDialog.Designer.Repositories;
using ThemedDialog.Designer.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ThemedDialog.Designer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class ThemesManagePage : Page
    {
        internal ThemeManageViewModel ViewModel;

        public ThemesManagePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel == null)
            {
                var param = (e.Parameter as ThemesManagePageParameter);
                ViewModel = new ThemeManageViewModel(param.Repository);
            }
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

    internal class ThemesManagePageParameter
    {
        public Repository Repository { get; set; }
    }
}