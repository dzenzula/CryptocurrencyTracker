using CryptocurrencyTracker.Library.Models;
using CryptocurrencyTracker.Library.Services;
using CryptocurrencyTracker.UI.Pages;
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
    public class CryptoSearchViewModel : CryptoViewModelBase
    {
        public ICommand InfoCommand { get; set; }
        private ICoinCapService _coinCapService;
        private string _searchString;
        public TaskWatcher<List<BaseCryptoModel>> AllCoinsTask { get; set; }
        private ObservableCollection<BaseCryptoModel> _allCoinsList;
        private ObservableCollection<BaseCryptoModel> _filteredCoinList;
        private BaseCryptoModel _selectedCoin;

        public CryptoSearchViewModel()
        {
            InitializeCommands();
            LoadAsyncData();
        }
        public override void InitializeCommands()
        {
            InfoCommand = new RelayCommand(ShowMethod, CanShow);
        }

        public override void LoadAsyncData()
        {
            _coinCapService = ContainerHelper.Container.Resolve<CoinCapService>();
            AllCoinsTask = new TaskWatcher<List<BaseCryptoModel>>(_coinCapService.GetAllCoinsAsync());
            AllCoinsTask.PropertyChanged += AllCoinsTask_PropertyChanged;
        }

        private bool CanShow(object parametr)
        {
            if (parametr != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ShowMethod(object paremetr)
        {
            //Against MVVM, need refoctoring
            InformationWindow objPopupwindow = new InformationWindow(SelectedCoin);
            objPopupwindow.Show();
        }

        private void AllCoinsTask_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                CoinList = new ObservableCollection<BaseCryptoModel>(AllCoinsTask.Result);
                FilteredCoinList = CoinList;
            }

            if (e.PropertyName == "IsFaulted")
            {
                throw new Exception();
            }

            return;
        }

        private void OnSearch()
        {
            var tempSearch = SearchString.ToLower();

            if (tempSearch == null) return;

            FilteredCoinList = new ObservableCollection<BaseCryptoModel>(CoinList.Where(c => c.Symbol.ToLower().Contains(tempSearch) ||
                                                                        c.Name.ToLower().Contains(tempSearch))
                                                                        .ToList());
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

        public string SearchString
        {
            get
            {
                return _searchString;
            }

            set
            {
                _searchString = value;
                RaisePropertyChanged();
                OnSearch();
            }
        }


        public ObservableCollection<BaseCryptoModel> FilteredCoinList
        {
            get
            {
                return _filteredCoinList;
            }

            set
            {
                _filteredCoinList = value;
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
            }
        }
    }
}
