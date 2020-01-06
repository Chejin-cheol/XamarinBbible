using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.Interface
{
    public interface IBibleConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
