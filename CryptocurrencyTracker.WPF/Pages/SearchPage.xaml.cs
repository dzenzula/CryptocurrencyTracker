using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptocurrencyTracker.UI.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is NavButton)
            {
                var ClickedButton = e.OriginalSource as NavButton;

                NavigationService.Navigate(ClickedButton.NavUri);
            }

            return;
        }
    }
}
