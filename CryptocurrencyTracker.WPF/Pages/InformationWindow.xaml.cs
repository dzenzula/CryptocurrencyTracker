using CryptocurrencyTracker.Library.Models;
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
using System.Windows.Shapes;

namespace CryptocurrencyTracker.UI.Pages
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        public InformationWindow(BaseCryptoModel selectedCoin)
        {
            InitializeComponent();
            DataContext = this;
            id.Text = selectedCoin.Id;
            Symbol.Text = selectedCoin.Symbol;
            Rank.Text = String.Format("{0:0.00}", selectedCoin.Rank);
            Name.Text = selectedCoin.Name;
            Price.Text = String.Format("{0:0.00}", selectedCoin.PriceUsd);
            Supply.Text = String.Format("{0:0.00}", selectedCoin.Supply);
            MaxSupply.Text = String.Format("{0:0.00}", selectedCoin.MaxSupply);
            MarketCap.Text = String.Format("{0:0.00}", selectedCoin.MarketCapUsd);
            Volume24H.Text = String.Format("{0:0.00}", selectedCoin.VolumeUsd24Hr);
            Change24H.Text = String.Format("{0:0.00}", selectedCoin.ChangePercent24Hr) + "%";
            Site.Text = selectedCoin.Explorer;
        }
    }
}
