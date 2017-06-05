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
        private static TextBox predictedWords = null;
        public static void Initialize(TextBox textBox)
        {
            predictedWords = textBox;
        }

        public static void UpdatePredictedWord(String s)
        {
            if (predictedWords.IsHandleCreated)
            {
                try
                {
                    predictedWords.Invoke((MethodInvoker)delegate
                    {
                        AddLetterToLastPredictedWord(s[0]);
                    });
                }
                catch (ObjectDisposedException)
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
            String existingText = predictedWords.Text;
            existingText += ch;
            predictedWords.Text = existingText;
        }

        public static void ResetPredictedWordLabel()
        {
            if (predictedWords.IsHandleCreated)
            {
                try
                {
                    predictedWords.Invoke((MethodInvoker)delegate
                    {
                        predictedWords.Text = "";
                    });
                }
                catch (ObjectDisposedException)
                {

                }
            }
            else
            {
                predictedWords.Text = "";
            }
        }

        internal static void SetPredictedWordsText(string allPredictedWords)
        {
            if (predictedWords.IsHandleCreated)
            {
                try
                {
                    predictedWords.Invoke((MethodInvoker)delegate
                    {
                        predictedWords.Text = allPredictedWords;
                    });
                }
                catch (ObjectDisposedException)
                {

                }
            }
            else
            {
                predictedWords.Text = allPredictedWords;
            }
        }
    }
}
