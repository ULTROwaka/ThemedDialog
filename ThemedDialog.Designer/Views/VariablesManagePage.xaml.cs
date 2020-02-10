using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThemedDialog.Designer.Repositories;
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
    public sealed partial class VariablesManagePage : Page
    {
        internal VariablesManageViewModel ViewModel;
        public VariablesManagePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel == null)
            {
                var param = (e.Parameter as VariablesManagePageParameter);
                ViewModel = new VariablesManageViewModel(param.Repository);
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

        private void AddCancel_Click(object sender, RoutedEventArgs e)
        {
            AddFlyout.Hide();
        }

        private void EditCancel_Click(object sender, RoutedEventArgs e)
        {
            EditFlyout.Hide();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete();
        }
    }

    internal class VariablesManagePageParameter
    {
        internal Repository Repository { get; set; }
    }
}
