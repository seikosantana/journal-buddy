using System;
using System.IO;
using System.Threading.Tasks;
using LiteDB;

namespace JournalBuddy.Data
{
    public class DataManager
    {
        public readonly string DatabaseLocation = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "journal.db");
        LiteDatabase db;

        public DataManager()
        {
            db = new LiteDatabase(DatabaseLocation);
        }

        public async Task<Journal> AddJournal(Journal journal)
        {
            var journalCollection = db.GetCollection<Journal>("journals");
            journalCollection.Insert(journal);
            return journalCollection.FindOne(j => j.Date == journal.Date);
        }

        ~DataManager()
        {
            db.Dispose();
        }
    }
}