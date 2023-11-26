using PuntoDeventa.IU;
using Syncfusion.SfCalendar.XForms;
using System;
using Xamarin.Forms;

namespace PuntoDeventa.UI
{
    internal class BaseCalendarViewModel : BaseViewModel
    {
        private SelectionRange _dateRange;
        private SelectionRange _dateRangeOld;
        private bool _isVisibleCalendar;

        public BaseCalendarViewModel()
        {
            SetupCalendar();
        }

        private void SetupCalendar()
        {
            var date = DateTime.Now;
            DateRangeNow = DateRangeOld = new SelectionRange(new DateTime(date.Year, date.Month, 1), date);

        }

        public bool IsVisibleCalendar
        {
            get => _isVisibleCalendar;
            set => SetProperty(ref _isVisibleCalendar, value);
        }
        public SelectionRange DateRangeOld
        {
            get => _dateRangeOld;
            set => SetProperty(ref _dateRangeOld, value);
        }
        public SelectionRange DateRangeNow
        {
            get => _dateRange;
            set => SetProperty(ref _dateRange, value);
        }

        public Command SelectionChangedCommand { get; set; }
    }
}
