using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy
{
    partial class MainWindow : Window
    {
        [UI] SearchBar JournalSearchBar;
        [UI] SearchEntry JournalSearchEntry;
        [UI] ListBox lbJournalList;
        [UI] Spinner spinnerLoading;
        [UI] Label lblStatus;
        [UI] Entry txtTitle;
        [UI] Entry txtTags;
        [UI] Label lblDate;
        [UI] Button btnPickDate;
        [UI] TextView tvJournalContent;
        [UI] Button btnNew;
        [UI] HeaderBar HeaderBar;
        [UI] Button btnSetting;

    }
}