using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.Interface
{
    public interface IPath
    {
        string getLocalPath(string forderName, string fileName);
        string getLocalPath(string Directory);
    }
}
