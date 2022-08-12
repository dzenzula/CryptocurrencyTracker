using CryptocurrencyTracker.Library.Models;
using CryptocurrencyTracker.Library.Services;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace CryptocurrencyTracker.UI.MVVM
{
    public class CryptoListViewModel : CryptoViewModelBase
    {
        public ICommand RadioCommand { get; set; }
        private ICoinCapService _coinCapService;
        private ObservableCollection<BaseCryptoModel> _allCoinsList;
        private BaseCryptoModel _selectedCoin;
        private List<CoinHistory> _selectedCoinHistory;

        //Charts
        public SeriesCollection Series { get; set; }
        public Func<double, string> Formatter { get; set; }
        public string[] _labels;

        public TaskWatcher<List<BaseCryptoModel>> AllCoinsTask { get; set; }
        public TaskWatcher<List<CoinHistory>> CoinValues { get; set; }

        public CryptoListViewModel()
        {
            InitializeCommands();
            LoadAsyncData();

            Series = new SeriesCollection();
        }

        public override void InitializeCommands()
        {
            RadioCommand = new RelayCommand(GetSelectedCoinValues, CanGetSelectedCoinValues);
        }

        public override void LoadAsyncData()
        {
            _coinCapService = ContainerHelper.Container.Resolve<CoinCapService>();
            AllCoinsTask = new TaskWatcher<List<BaseCryptoModel>>(_coinCapService.GetAllCoinsAsync());
            AllCoinsTask.PropertyChanged += AllCoinsTask_PropertyChanged;
        }

        private bool CanGetSelectedCoinValues(object timeView)
        {
            if (timeView != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetSelectedCoinValues(object timeView)
        {
            CoinValues = new TaskWatcher<List<CoinHistory>>(_coinCapService.GetCoinHistory(SelectedCoin.Id, (string)timeView));
            CoinValues.PropertyChanged += CoinValues_PropertyChanged;
        }

        private void CoinValues_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                Series.Clear();
                SelectedCoinHistory = new List<CoinHistory>(CoinValues.Result);
                var coinValues = SelectedCoinHistory.Select(i => i.PriceUsd);

                Series.Add(new LineSeries
                {
                    Title = SelectedCoin.Name,
                    Values = new ChartValues<decimal>(coinValues),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 2
                });

                Labels = SelectedCoinHistory.Select(i => i.DateTime.ToShortDateString()).ToArray();
            }

            if (e.PropertyName == "IsFaulted")
            {
                throw new Exception();
            }

            return;
            
        }

        private void AllCoinsTask_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                CoinList = new ObservableCollection<BaseCryptoModel>(AllCoinsTask.Result.Take(10));
            }

            if (e.PropertyName == "IsFaulted")
            {
                throw new Exception();
            }

            return;
        }

        public string[] Labels
        {
            get 
            { 
                return _labels; 
            }

            set
            {
                _labels = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<BaseCryptoModel> CoinList
        {
            get
            {
                return _allCoinsList;
            }

            set
            {
                _allCoinsList = value;
                RaisePropertyChanged();
            }
        }

        public BaseCryptoModel SelectedCoin
        {
            get
            {
                return _selectedCoin;
            }

            set 
            { 
                _selectedCoin = value;
                RaisePropertyChanged();
                GetSelectedCoinValues("d1");
            }
        }

        public List<CoinHistory> SelectedCoinHistory
        {
            get
            {
                return _selectedCoinHistory;
            }

            set
            {
                _selectedCoinHistory = value;
                RaisePropertyChanged();
            }
        }
        
    }
}
