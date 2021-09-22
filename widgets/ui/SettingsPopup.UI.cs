using Gtk;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy {
    partial class SettingsPopup: Popover {
        [UI] SpinButton spinSeconds;
        [UI] Button btnAbout;
    }
}