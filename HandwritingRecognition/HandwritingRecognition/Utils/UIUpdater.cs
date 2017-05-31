using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandwritingRecognition.Utils
{
    class UIUpdater
    {
        private static Label predictedWord = null;
        public static void Initialize(Label label)
        {
            predictedWord = label;
        }

        public static void UpdatePredictedWord(String s)
        {
            if (predictedWord.IsHandleCreated)
            {
                try
                {
                    predictedWord.Invoke((MethodInvoker)delegate
                    {
                        AddLetterToLastPredictedWord(s[0]);
                    });
                }
                catch (ObjectDisposedException e)
                {

                }
            }
            else
            {
                AddLetterToLastPredictedWord(s[0]);
            }
        }

        public static void AddLetterToLastPredictedWord(char ch)
        {
            String existingText = predictedWord.Text;
            existingText += ch;
            predictedWord.Text = existingText;
        }

        public static void ResetPredictedWordLabel()
        {
            if (predictedWord.IsHandleCreated)
            {
                try
                {
                    predictedWord.Invoke((MethodInvoker)delegate
                    {
                        predictedWord.Text = "";
                    });
                }
                catch (ObjectDisposedException e)
                {

                }
            }
            else
            {
                predictedWord.Text = "";
            }
        }
    }
}
