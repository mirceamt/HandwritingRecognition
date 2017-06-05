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
        List<Word> m_finishedWords = new List<Word>();
        List<Word> m_candidateWords = new List<Word>();
        Word m_currentWord = null;

        public WritingObserver()
        {
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
            return true;
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

        private void GenerateCandidateWordsWithBacktracking(int k, Word w, List<int> keysList, Dictionary<int, List<int>> positionsOfChosenCharsSolution)
        {
            //this backtracking assumes that each connected component has only one letter
            if (k >= keysList.Count)
            {
                // solution
                if (DifferentDictionaries(w.GetPositionsOfChosenCharsDictionary(), positionsOfChosenCharsSolution)) // TODO: eliminate this function if possible
                {
                    Dictionary<int, List<int>> solution = new Dictionary<int, List<int>>(positionsOfChosenCharsSolution); // TODO fix this creation by doing it manually
                    Word newWord = new Word(w, solution);

                    m_candidateWords.Add(newWord);
                }
                return;
            }
            int currentKey = keysList[k];

            List<int> currentList = new List<int>();

            for (int i = 0; i < 2; i++)
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
            m_candidateWords.Add(m_currentWord);

            Dictionary<int, List<String>> possibleChars = m_currentWord.GetPossibleCharsDictionary();
            List<int> keysList = possibleChars.Keys.ToList();
            Dictionary<int, List<int>> positonsOfChosenCharsSolution = new Dictionary<int,List<int>>();

            GenerateCandidateWordsWithBacktracking(0, m_currentWord, keysList, positonsOfChosenCharsSolution);
        }

        private void UpdateUI()
        {
            String allPredictedWords = this.ToString();
            UIUpdater.SetPredictedWordsText(allPredictedWords);

            UIUpdater.CreateLabelsForCandidateWords(m_candidateWords);
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

        public void FinishCurrentWord()
        {
            FinishCurrentWordInternal();
            CreateNewCandidateWords();
            UpdateUI();
        }
    }
}
