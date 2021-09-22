using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace JournalBuddy
{
    partial class MainWindow : Window
    {

        public MainWindow() : this(new Builder("journal-buddy.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);
            InitializeEvents();
        }

        void InitializeEvents()
        {
            btnPickDate.Clicked += ShowPickDatePopup;
            btnNew.Clicked += CreateNewJournal;
            DeleteEvent += Window_DeleteEvent;
            lbJournalList.RowSelected += SelectedJournalChanged;
            txtTitle.Changed += TitleEdited;
            txtTags.Changed += TagsEdited;
            btnSetting.Clicked += SettingsClicked;
        }

        private void SettingsClicked(object sender, EventArgs e)
        {
            SettingsPopup popup = new SettingsPopup(2);
            popup.RelativeTo = (Widget)sender;
            popup.Modal = true;
            popup.OnShowAbout += (s, a) =>
            {
                AboutDialog dialog = new AboutDialog();
                dialog.TransientFor = this;
                dialog.Modal = true;
                dialog.Run();
            };
            popup.Show();
        }

        private void TagsEdited(object sender, EventArgs e)
        {
            if (ReflectChanges)
            {
                JournalRowContent content = ((JournalRowWidget)activeJournalRow).Content;
                content.Journal.Tags = txtTags.Text;
                content.UpdateContent();
            }
        }

        private void TitleEdited(object sender, EventArgs e)
        {
            if (ReflectChanges)
            {
                JournalRowContent content = ((JournalRowWidget)activeJournalRow).Content;
                content.Journal.Title = txtTitle.Text;
                HeaderBar.Subtitle = txtTitle.Text;
                content.UpdateContent();
            }
        }

        private JournalRowWidget activeJournalRow;
        private bool ReflectChanges = false;

        void LoadJournal(JournalRowWidget rowWidget)
        {
            if (activeJournalRow == null)
            {
                ReflectChanges = false; // dont reflect changes when clearing text
                txtTitle.Text = "";
                lblDate.Text = "";
                txtTags.Text = "";
                HeaderBar.Subtitle = "";
                btnPickDate.Sensitive = false;
                txtTitle.Sensitive = false;
                txtTags.Sensitive = false;
                tvJournalContent.Sensitive = false;
            }
            else
            {
                Journal journal = ((JournalRowWidget)activeJournalRow).Content.Journal;
                txtTitle.Text = journal.Title;
                lblDate.Text = journal.Date.ToLongDateString();
                txtTags.Text = journal.Tags;
                ReflectChanges = true; //reflect changes after the text change to avoid loop
                HeaderBar.Subtitle = journal.Title;
                btnPickDate.Sensitive = true;
                txtTitle.Sensitive = true;
                txtTags.Sensitive = true;
                tvJournalContent.Sensitive = true;
            }
        }

        public JournalRowWidget ActiveJournalRow
        {
            get => activeJournalRow;
            set
            {
                activeJournalRow = value;
                LoadJournal(activeJournalRow);
            }
        }

        private void SelectedJournalChanged(object o, RowSelectedArgs args)
        {
            ActiveJournalRow = args.Row as JournalRowWidget;
        }

        private void CreateNewJournal(object sender, EventArgs e)
        {
            Journal newJournal = new Journal()
            {
                Title = "Untitled Journal",
                Date = DateTime.Now,
                Tags = "",
                Content = new Object()
            };
            JournalRowContent rowContent = new JournalRowContent(newJournal);
            JournalRowWidget rowWidget = new JournalRowWidget(rowContent);
            rowWidget.ButtonPressEvent += JournalClicked;
            lbJournalList.Add(rowWidget);
            lbJournalList.ShowAll();
        }

        private void JournalClicked(object o, ButtonPressEventArgs args)
        {
            if (args.Event.Button == 3) //right click
            {
                JournalContextMenu contextMenu = new JournalContextMenu(o as JournalRowWidget);
                contextMenu.OnEditJournal += (s, e) =>
                {
                    lbJournalList.SelectRow(e);
                };
                contextMenu.OnDeleteJournal += (s, e) =>
                {
                    RemoveJournal(e);
                };
                contextMenu.Show();
            }
        }

        private void RemoveJournal(JournalRowWidget e)
        {
            // lbJournalList.UnselectAll();
            lbJournalList.Remove(e);
        }

        private void ShowPickDatePopup(object sender, EventArgs e)
        {
            DatePickerPopup popup = new DatePickerPopup();
            popup.RelativeTo = btnPickDate;
            popup.Modal = true;
            popup.DatePicked += SetJournalDate;
            popup.Show();
        }

        private void SetJournalDate(object sender, DateTime e)
        {
            lblDate.Text = e.ToLongDateString();
            activeJournalRow.Content.Journal.Date = e;
            activeJournalRow.Content.UpdateContent();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }
}
