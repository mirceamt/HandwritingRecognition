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

        private void CreateNewCandidateWords()
        {
            // create new candidate words based on the current word
            m_candidateWords.Clear();
            m_candidateWords.Add(m_currentWord);

        }

        private void UpdateUI()
        {
            String allPredictedWords = this.ToString();
            UIUpdater.SetPredictedWordsText(allPredictedWords);
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
            UpdateUI();
        }
    }
}
