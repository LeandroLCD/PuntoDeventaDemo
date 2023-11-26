using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.Report;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PuntoDeventa.UI.Utilities;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Reports
{
    internal class ReportProductPageViewModel : BaseCalendarViewModel
    {
        #region Fields
        private ObservableCollection<ProductSales> _productSales;
        private int _totalNeto;
        private int _amountVat;
        private string _searchText;
        private readonly IGetReportSales _reportUseCase;
        #endregion

        #region Constructor

        public ReportProductPageViewModel(IGetReportSales reportUseCase)
        {
            _reportUseCase = reportUseCase;
            InitializeCommands();
        }

        public ReportProductPageViewModel()
        {

            _reportUseCase = DependencyService.Get<IGetReportSales>();
            InitializeCommands();
        }



        #endregion

        #region Properties
        private LinkedList<ProductSales> GetProductSales { get; set; } = new LinkedList<ProductSales>();
        
        public ObservableCollection<ProductSales> ProductSales
        {
            get
            {
                if (_productSales.IsNull())
                {
                    _productSales = new ObservableCollection<ProductSales>();
                }

                return _productSales;
            }
            private set
            {
                TotalNet = (int)value.Sum(r => r.SubTotal);
                AmountVat = (int)value.Sum(r => r.SubTotalGross);
                SetProperty(ref _productSales, value);
            }
        }

        public int TotalNet
        {
            get => _totalNeto;
            set => SetProperty(ref _totalNeto, value);
        }

        public int AmountVat
        {
            get => _amountVat;
            set => SetProperty(ref _amountVat, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        #endregion

        #region Commands
        public Command SyncCommand { get; set; }

        public Command<string> SearchBarCommand { get; set; }

        public Command VisibleCalendarCommand { get; set; }
        #endregion

        #region Methods
        private void InitializeCommands()
        {

            SyncCommand = new Command(() =>
            {
                LoadDataMontAsync(DateRangeNow.StartDate);
            });
            SearchBarCommand = new Command<string>((text) =>
            {
                ProductFilter(text);
                _searchText = text;
            });
            SelectionChangedCommand = new Command(() =>
            {

                GetProductSales.Clear();
                ProductSales.Clear();
                LoadDataMontAsync(DateRangeNow.StartDate);

                if (DateRangeNow.EndDate.Year != DateRangeOld.StartDate.Year ||
                    DateRangeNow.EndDate.Month != DateRangeOld.StartDate.Month)
                {
                    LoadDataMontAsync(DateRangeNow.EndDate);
                }
            });
            VisibleCalendarCommand = new Command(() => IsVisibleCalendar = !IsVisibleCalendar);
        }

        private void ProductFilter(string text)
        {
            if (!string.IsNullOrEmpty(text))
                _productSales = new ObservableCollection<ProductSales>(GetProductSales.Where(c =>
                    c.Name.ToLower().Contains(text.ToLower()))?.ToList());
            else
                _productSales = new ObservableCollection<ProductSales>(GetProductSales);
            NotifyPropertyChanged(nameof(ProductSales));
        }

        public void OnAppearing()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {

            LoadDataMontAsync(DateRangeNow.StartDate);
        }

        private async void LoadDataMontAsync(DateTime date)
        {
            IsLoading = true;
            SearchText = string.Empty;
            try
            {
                await foreach (var reportSale in _reportUseCase.EmitSales(date))
                {
                    if (reportSale.Date.Date < DateRangeNow.StartDate.Date ||
                        reportSale.Date.Date > DateRangeNow.EndDate.Date) continue;

                    foreach (var product in reportSale.Products)
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            if (!GetProductSales.Contains(product))
                            {
                                ProductSales.Add(product);
                                GetProductSales.AddLast(product);
                            }
                            else
                            {
                                var ind = ProductSales.IndexOf(product);
                                ProductSales[ind].Quantity += product.Quantity;
                                var productTarget = GetProductSales.FirstOrDefault(p => p.SkuCode.Equals(product.SkuCode));
                                productTarget?.Apply(() =>
                                {
                                    productTarget.Quantity += product.Quantity;
                                });
                            }
                        });

                    }




                }
            }
            catch (CustomException ex)
            {
                if (!ex.ErrorCode.Equals(8)) throw ex;
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Punto de Venta", ex.Message, "Ok");
                return;

            }
            
            IsVisibleCalendar = IsLoading = false;
        }



        #endregion

    }
}