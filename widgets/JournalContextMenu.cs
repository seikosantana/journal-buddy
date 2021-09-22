using Gtk;
using System;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy
{
    partial class JournalContextMenu: Popover {
        JournalRowWidget journalWidget;
        public JournalContextMenu(JournalRowWidget target): this(new Builder("journal-buddy.glade")) {
            this.RelativeTo = target;
            journalWidget = target;

            ctxEdit.Clicked += TriggerEdit;
            ctxDelete.Clicked += TriggerDelete;
        }

        private void TriggerDelete(object sender, EventArgs e)
        {
            OnDeleteJournal?.Invoke(this, journalWidget);
        }

        private void TriggerEdit(object sender, EventArgs e)
        {
            OnEditJournal?.Invoke(this, journalWidget);
        }

        public JournalContextMenu(Builder builder) : base(builder.GetRawOwnedObject("JournalContextMenu")) {
            builder.Autoconnect(this);
        }

        public event EventHandler<JournalRowWidget> OnDeleteJournal;
        public event EventHandler<JournalRowWidget> OnEditJournal;
    }

}