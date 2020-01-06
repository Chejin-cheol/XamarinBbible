using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.Interface
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection(string dbName);
    }
}
