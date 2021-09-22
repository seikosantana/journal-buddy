using System;
using Gtk;

public class Journal
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Tags { get; set; }
    public object Content { get; set; }
}

public class JournalRowWidget : ListBoxRow
{
    private JournalRowContent content;
    public JournalRowContent Content
    {
        get => content;
        set => content = value;
    }

    public JournalRowWidget(Journal journal) : this(new JournalRowContent(journal)) { 
        
    }

    public JournalRowWidget(JournalRowContent rowContent)
    {
        Content = rowContent;
        this.Add(rowContent);
    }
}

public class JournalRowContent : EventBox
{
    Label title, date, tags;
    public Journal Journal { get; set; }
    public JournalRowContent(Journal journal)
    {
        title = new Label()
        {
            Halign = Align.Start
        };
        date = new Label()
        {
            Halign = Align.Start
        };
        tags = new Label()
        {
            Halign = Align.Start
        };
        this.Journal = journal;
        title.Attributes = new Pango.AttrList();
        title.Attributes.Change(new Pango.AttrWeight(Pango.Weight.Semibold));

        title.Ellipsize = Pango.EllipsizeMode.End;
        date.Ellipsize = Pango.EllipsizeMode.End;
        tags.Ellipsize = Pango.EllipsizeMode.End;

        title.Attributes.Change(new Pango.AttrGravity(Pango.Gravity.West));
        title.Attributes.Change(new Pango.AttrGravityHint(Pango.GravityHint.Strong));

        this.MarginStart = 8;
        Box box = new Box(Orientation.Vertical, 0);
        box.PackStart(title, true, true, 0);
        box.PackStart(date, true, true, 0);
        box.PackStart(tags, true, true, 0);
        this.Add(box);
        UpdateContent();
    }

    private void Pressed(object o, ButtonPressEventArgs args)
    {
        Console.WriteLine("pressed");
    }

    public void UpdateContent()
    {
        this.title.Text = Journal.Title;
        this.date.Text = Journal.Date.ToLongDateString();
        this.tags.Text = Journal.Tags;
    }
}