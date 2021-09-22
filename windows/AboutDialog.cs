using Gtk;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy {
    partial class AboutDialog : Gtk.AboutDialog {
        public AboutDialog() : this(new Builder("journal-buddy.glade")) {
            
        }

        public AboutDialog(Builder builder) : base(builder.GetRawOwnedObject("AboutDialog")) {
            builder.Autoconnect(this);
        }
    }
}