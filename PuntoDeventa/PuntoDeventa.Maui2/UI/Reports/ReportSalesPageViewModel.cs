using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.Report;
using PuntoDeventa.IU;
using PuntoDeventa.UI.Reports.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Reports
{
    internal class ReportSalesPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<ReportSale> _reportSales;
        private int _totalNeto;
        private int _amountVat;
        private readonly IGetReportSales _reportUseCase;

        #endregion

        #region Constructor

        public ReportSalesPageViewModel(IGetReportSales reportUseCase)
        {
            DateReport = DateTime.Now;
            _reportUseCase = reportUseCase;
            InitializeCommands();
        }

        public ReportSalesPageViewModel()
        {
            DateReport = DateTime.Now;
            _reportUseCase = DependencyService.Get<IGetReportSales>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SyncCommand = new Command(() =>
            {
                LoadDataMontAsync(DateReport);
            });
        }

        #endregion

        #region Properties

        private DateTime DateReport { get; set; }
        public ObservableCollection<ReportSale> ReportSales
        {
            get
            {
                if (_reportSales.IsNull())
                {
                    _reportSales = new ObservableCollection<ReportSale>();
                }

                return _reportSales;
            }
            private set
            {
                TotalNet = (int)value.Sum(r => r.TotalNet);
                AmountVat = (int)value.Sum(r => r.AmountIva);
                SetProperty(ref _reportSales, value);
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

        #endregion

        #region Commands
        public Command SyncCommand { get; set; }


        #endregion

        #region Methods


        public void OnAppearing()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {

            LoadDataMontAsync(DateReport);
        }

        private async void LoadDataMontAsync(DateTime date)
        {
            IsLoading = true;
            await foreach (var report in _reportUseCase.EmitSales(date))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (!ReportSales.Contains(report))
                        ReportSales.Add(report);
                    else
                    {
                        var ind = ReportSales.IndexOf(report);
                        ReportSales.RemoveAt(ind);
                        ReportSales.Insert(ind, report);
                    }


                });

            }
            IsLoading = false;
        }



        #endregion
    }
}
