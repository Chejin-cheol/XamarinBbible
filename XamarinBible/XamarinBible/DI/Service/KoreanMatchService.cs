using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.DI.Service
{
    public class KoreanMatchService : IKoreanMatchService
    {
        private static char[] Cheosung = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
        private int Base = 44032;

        public bool isChoseong(char c)
        {
            if (c == Cheosung[0] || c == Cheosung[1] || c == Cheosung[2] || c == Cheosung[3]
                || c == Cheosung[4] || c == Cheosung[5] || c == Cheosung[6] || c == Cheosung[7]
                || c == Cheosung[8] || c == Cheosung[9] || c == Cheosung[10] || c == Cheosung[11]
                || c == Cheosung[12] || c == Cheosung[13] || c == Cheosung[14] || c == Cheosung[15]
                || c == Cheosung[16] || c == Cheosung[17] || c == Cheosung[18])
                {
                    return true;
                }
             return false;
        }

        public bool Match(string source, string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (source[i] != ' ')
                {
                    if (isChoseong(word[i]))
                    {
                        int item = Convert.ToInt32(source[i]);
                        int uniVal = (item - Base);
                        int index = uniVal / (21 * 28);

                        if (Cheosung[index] != word[i])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (source[i] != word[i])
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (source[i] != word[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
