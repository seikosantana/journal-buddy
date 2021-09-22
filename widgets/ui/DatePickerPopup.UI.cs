using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy
{
    partial class DatePickerPopup : Popover
    {
#pragma warning disable CS0694
        [UI] Calendar calendarPickedDate;
        [UI] Button btnSetDate;
        [UI] Label lblSelectedDate;
#pragma warning restore CS0694
    }
}