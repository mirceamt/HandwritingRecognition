using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Writing
{
    class Word
    {
        private Dictionary<int, List<String>> m_possibleChars = null;
        private Dictionary<int, List<int>> m_positionsOfChosenChars = null;
        
        public Word()
        {
            m_possibleChars = new Dictionary<int, List<string>>();
            m_positionsOfChosenChars = new Dictionary<int, List<int>>();
        }



    }
}
