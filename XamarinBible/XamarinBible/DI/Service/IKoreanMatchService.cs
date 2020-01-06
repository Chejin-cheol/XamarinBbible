using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.DI.Service
{
    public interface IKoreanMatchService
    {
        bool isChoseong(char c);
        bool Match(string source, string word);
    }
}
