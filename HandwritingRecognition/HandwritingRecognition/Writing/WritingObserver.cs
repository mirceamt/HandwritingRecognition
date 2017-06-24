using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HandwritingRecognition.ImageProcessing;
using HandwritingRecognition.Utils;

namespace HandwritingRecognition.Writing
{
    class WritingObserver
    {
        class WordComparerByPriority : Comparer<Word>
        {
            public override int Compare(Word x, Word y)
            {
                if (x.Priority > y.Priority)
                {
                    return -1;
                }
                if (x.Priority < y.Priority)
                {
                    return 1;
                }
                return 0;
            }
        }

        List<Word> m_finishedWords = new List<Word>();
        //List<Word> m_candidateWords = new List<Word>();
        Dictionary<String, Word> m_candidateWords = new Dictionary<String, Word>();
        Word m_currentWord = null;
        LanguageDictionary languageDictionary = null;

        public WritingObserver()
        {
        }

        public void Initialize(LanguageDictionary languageDictionaryParam)
        {
            languageDictionary = languageDictionaryParam;
        }

        public void Clear()
        {
            m_finishedWords.Clear();
            m_candidateWords.Clear();
            
            m_currentWord = null;
        }

        private void CreateNewCurrentWord(List<ConnectedComponent> latestRemovedComponents, ConnectedComponent latestAddedComponent, List<String> possibleChars, List<int> positionsOfChosenChars = null)
        {
            m_currentWord = new Word();
            if (positionsOfChosenChars == null)
            {
                positionsOfChosenChars = new List<int>();
                for (int i = 0; i < possibleChars.Count; i++)
                {
                    positionsOfChosenChars.Add(0);
                }
            }
            m_currentWord.AddConnectedComponent(latestAddedComponent.ID, latestAddedComponent, possibleChars, positionsOfChosenChars);
        }

        private bool DifferentLists(List<int> a, List<int> b)
        {
            if (a.Count != b.Count)
            {
                return true;
            }

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    return true;
                }
            }

            return false;
        }

        private bool DifferentDictionaries(Dictionary<int, List<int>> a, Dictionary<int, List<int>> b)
        {
            // TODO!!!!
            //return true;
            if (a.Count != b.Count)
            {
                return true;
            }
            List<int> keysA = a.Keys.ToList();
            List<int> keysB = b.Keys.ToList();

            for (int i = 0; i < keysA.Count; i++)
            {
                int currentKey = keysA[i];

                if (b.ContainsKey(currentKey))
                {
                    if (DifferentLists(a[currentKey], b[currentKey]))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            Dictionary<int, List<int>> aux = null;
            aux = a;
            a = b;
            b = aux;

            for (int i = 0; i < keysA.Count; i++)
            {
                int currentKey = keysA[i];

                if (b.ContainsKey(currentKey))
                {
                    if (DifferentLists(a[currentKey], b[currentKey]))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        Dictionary<int, List<int>> CreateNewDictionary(Dictionary<int, List<int>> oldDictionary)
        {
            Dictionary<int, List<int>> ret = new Dictionary<int, List<int>>();
            foreach(KeyValuePair<int, List<int>> entry in oldDictionary)
            {
                List<int> newList = new List<int>(entry.Value);
                ret.Add(entry.Key, newList);
            }

            return ret;
        }

        private void GenerateCandidateWordsWithBacktracking(int k, Word w, List<int> keysList, Dictionary<int, List<int>> positionsOfChosenCharsSolution)
        {
            Dictionary<int, List<int>> solution = CreateNewDictionary(positionsOfChosenCharsSolution);
            Word newWord = new Word(w, solution);
            String newWordString = newWord.ToString();
            LanguageDictionary.PrefixType prefixType = languageDictionary.GetPrefixType(newWordString.ToLower());
            if (prefixType == LanguageDictionary.PrefixType.NotExists)
            {
                return;
            }

            //this backtracking assumes that each connected component has only one letter
            if (k >= keysList.Count)
            {
                // solution
                //if (DifferentDictionaries(w.GetPositionsOfChosenCharsDictionary(), positionsOfChosenCharsSolution)) // TODO: eliminate this function if possible
                // the if statement from above is unnecessary since we were comparing the solution word with the initial word by their
                // positions of chosen chars.
                if (newWordString.ToLower() != w.ToString().ToLower())
                {
                    newWord.SetPriority(prefixType);
                    m_candidateWords[newWordString] = newWord;
                    //m_candidateWords.Add(newWord);
                }
                return;
            }
            int currentKey = keysList[k];

            List<int> currentList = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                currentList.Clear();
                currentList.Add(i);
                positionsOfChosenCharsSolution.Add(currentKey, currentList);
                GenerateCandidateWordsWithBacktracking(k + 1, w, keysList, positionsOfChosenCharsSolution);
                positionsOfChosenCharsSolution.Remove(currentKey);
            }
        }

        private void CreateNewCandidateWords()
        {
            if (m_currentWord == null)
            {
                return;
            }

            // create new candidate words based on the current word
            m_candidateWords.Clear();

            //LanguageDictionary.PrefixType prefixTypeOfCurrentWord = languageDictionary.GetPrefixType(m_currentWord.ToString().ToLower());
            //m_currentWord.SetPriority(prefixTypeOfCurrentWord);
            m_currentWord.Priority = 15; // show the first predicted word after those ewords that exist in the dictionary
            m_candidateWords[m_currentWord.ToString()] = m_currentWord;

            Dictionary<int, List<String>> possibleChars = m_currentWord.GetPossibleCharsDictionary();
            List<int> keysList = possibleChars.Keys.ToList();
            Dictionary<int, List<int>> positonsOfChosenCharsSolution = new Dictionary<int,List<int>>();

            GenerateCandidateWordsWithBacktracking(0, m_currentWord, keysList, positonsOfChosenCharsSolution);
        }

        private List<Word> SortCandidateWordsByPriority()
        {
            List<Word> sortedWordsByPriority = new List<Word>();

            foreach(KeyValuePair<string, Word> pair in m_candidateWords)
            {
                sortedWordsByPriority.Add(pair.Value);
            }

            sortedWordsByPriority.Sort(new WordComparerByPriority());

            return sortedWordsByPriority;
        }

        private void UpdateUI()
        {
            List<Word> sortedWordsByPriority = SortCandidateWordsByPriority();
            if (sortedWordsByPriority.Count > 0)
            {
                m_currentWord = sortedWordsByPriority[0];
            }

            String allPredictedWordsText = this.ToString();
            UIUpdater.SetPredictedWordsText(allPredictedWordsText);

            UIUpdater.CreateLabelsForCandidateWords(sortedWordsByPriority);
        }

        public void AdjustExistingWords(List<ConnectedComponent> latestRemovedComponents, ConnectedComponent latestAddedComponent, List<String> possibleChars, List<int> positionsOfChosenChars = null)
        { 
            if (m_currentWord == null)
            {
                CreateNewCurrentWord(latestRemovedComponents, latestAddedComponent, possibleChars, positionsOfChosenChars);
            }
            else
            {
                float averageWidthOfLetter = m_currentWord.GetAverageWidthOfLetter();
                int globalRightOfCurrentWord = m_currentWord.GetGlobalRight();
                int globalLeftOfLatestAddedComponent = latestAddedComponent.GlobalLeft;
                int distanceBetweenNewComponentAndWord = globalLeftOfLatestAddedComponent - globalRightOfCurrentWord;
                const float ratioForSpaceRecognition = 0.65f; // TODO: to be adjusted
                if (1.0f * distanceBetweenNewComponentAndWord > averageWidthOfLetter * ratioForSpaceRecognition)
                {
                    // if the distance between new connectedComponent (i.e. new letter) and the remaining word is greater than ratio * averageWidthOfLetter_inTheRemainingWord
                    // then create a new word
                    FinishCurrentWordInternal();
                    CreateNewCurrentWord(latestRemovedComponents, latestAddedComponent, possibleChars, positionsOfChosenChars);
                }
                else
                {
                    // else add this new connectedComponent to the existing word
                    m_currentWord.RemoveConnectedComponents(latestRemovedComponents);

                    if (positionsOfChosenChars == null)
                    {
                        positionsOfChosenChars = new List<int>();
                        for (int i = 0; i < possibleChars.Count; i++)
                        {
                            positionsOfChosenChars.Add(0);
                        }
                    }
                    m_currentWord.AddConnectedComponent(latestAddedComponent.ID, latestAddedComponent, possibleChars, positionsOfChosenChars);
                }
            }
            CreateNewCandidateWords();
            UpdateUI();
        }

        public override String ToString()
        {
            String ret = "";
            for (int i = 0; i < m_finishedWords.Count; i++)
            {
                ret += m_finishedWords[i].ToString();
                ret += " ";
            }
            if (m_currentWord != null)
            {
                ret += m_currentWord.ToString();
            }
            else
            {
                ret += " ";
            }
            return ret;
        }

        private void FinishCurrentWordInternal()
        {
            m_finishedWords.Add(m_currentWord);
            m_currentWord = null;

            m_candidateWords.Clear();
        }

        public void FinishCurrentWord(string textOnLabel)
        {
            foreach(KeyValuePair<String, Word> pair in m_candidateWords)
            {
                if (pair.Key == textOnLabel)
                {
                    m_currentWord = pair.Value;
                    break;
                }
            }
            FinishCurrentWordInternal();
            CreateNewCandidateWords();
            UpdateUI();
        }
    }
}
