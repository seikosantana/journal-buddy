using Gtk;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy {
    partial class SettingsPopup: Popover {
        public SettingsPopup(double savedSeconds) : this(new Builder("journal-buddy.glade")) {
            spinSeconds.Value = savedSeconds;
            btnAbout.Clicked += ShowAboutDialog;
        }

        public event EventHandler OnShowAbout;


        private void ShowAboutDialog(object sender, EventArgs e)
        {
            OnShowAbout?.Invoke(sender, e);
        }

        public SettingsPopup(Builder builder) : base(builder.GetRawOwnedObject("SettingsPopup")) {
            builder.Autoconnect(this);
        }
    }
}