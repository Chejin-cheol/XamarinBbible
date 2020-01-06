using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.Model
{
    public class Sermon
    {
        public int StartParagraph { get; set; }
        public int EndParagraph { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
    }
}
