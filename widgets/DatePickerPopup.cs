using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy
{
    partial class DatePickerPopup : Popover
    {
        DateTime _startingTime;
        public DatePickerPopup() : this(DateTime.Now) { }

        public DatePickerPopup(DateTime startingTime) : this(new Builder("journal-buddy.glade")) {
            _startingTime = startingTime;
            calendarPickedDate.Date = _startingTime;
            calendarPickedDate.DaySelectedDoubleClick += DateSelected;
            calendarPickedDate.DaySelected += DateChanged;
            lblSelectedDate.Text = calendarPickedDate.Date.ToLongDateString();
            btnSetDate.Sensitive = true;
            btnSetDate.Clicked += DateSelected;
        }

        private void DateChanged(object sender, EventArgs e)
        {
            lblSelectedDate.Text = calendarPickedDate.Date.ToLongDateString();
        }

        private DatePickerPopup(Builder builder) : base(builder.GetRawOwnedObject("CalendarPopup"))
        {
            builder.Autoconnect(this);

        }

        private void DateSelected(object sender, EventArgs e)
        {
            DateTime selectedDate = calendarPickedDate.Date;
            DatePicked?.Invoke(this, selectedDate);
            this.Destroy();
        }

        public event EventHandler<DateTime> DatePicked;

    }
}
