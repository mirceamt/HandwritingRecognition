using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

// TODO: uncomment these lines
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.Util;

using HandwritingRecognition.ImageProcessing;

namespace HandwritingRecognition.Classification
{
    class KNNClassifier
    {
        public KNNClassifier()
        {

        }

        public void Train(string path)
        {
            m_classifier.DefaultK = 10;
            m_classifier.IsClassifier = true;

            Matrix<float> trainData = null;
            Matrix<float> labelData = null;


            CreateTrainData(path, ref trainData, ref labelData, 10.0f);

            Console.WriteLine("Started training the KNN Classifier...");

            m_classifier.Train(trainData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, labelData);

            Console.WriteLine("Finished training the KNN Classifier.");

        }

        public string Classify(ConnectedComponent connectedComponent)
        {
            //TODO
            return "";
        }

        private void CreateTrainData(string path, ref Matrix<float> trainData, ref Matrix<float> labelData, float percent)
        {
            if (percent < 0.0f)
            {
                percent = 0.0f;
            }
            if (percent > 100.0f)
            {
                percent = 100.0f;
            }

            int trainDataCount = GetTotalNumberOfTrainingDataSamples(path, percent);
            int width = 32;
            int height = 32;

            trainData = new Matrix<float>(trainDataCount, width * height);
            labelData = new Matrix<float>(trainDataCount, 1);

            int trainDataRow = 0;
            int trainDataColumn = 0;

            DirectoryInfo allTrainDataDirectory = new DirectoryInfo(path);

            IEnumerable<DirectoryInfo> hexDirectories = allTrainDataDirectory.EnumerateDirectories();

            IEnumerator<DirectoryInfo> iterator = hexDirectories.GetEnumerator();
            while (iterator.MoveNext())
            {
                DirectoryInfo currentHexDirectory = iterator.Current;

                IEnumerable<DirectoryInfo> directoriesInHexDirectory = currentHexDirectory.EnumerateDirectories();

                IEnumerator<DirectoryInfo> iterator2 = directoriesInHexDirectory.GetEnumerator();

                Console.WriteLine("Reading from: " + currentHexDirectory.Name);

                while (iterator2.MoveNext())
                {
                    DirectoryInfo currentDirectoryInHex = iterator2.Current;

                    if (currentDirectoryInHex.Name == "train")
                    {
                        IEnumerable<FileInfo> trainFilesInCurrentHexDirectory = currentDirectoryInHex.EnumerateFiles("*.png");

                        int totalNumberOfTrainData = trainFilesInCurrentHexDirectory.Count();
                        int neededDataCount = (int)((percent / 100.0f) * totalNumberOfTrainData);
                        int count = 1;

                        IEnumerator<FileInfo> iterator3 = trainFilesInCurrentHexDirectory.GetEnumerator();

                        while (iterator3.MoveNext() && count <= neededDataCount)
                        {
                            FileInfo currentImageFile = iterator3.Current;
                            string currentImageFileFullName = currentImageFile.FullName;
                            string className = currentHexDirectory.Name;
                            float classNumber = (float) Convert.ToInt32(className, 16);

                            Bitmap currentRawImage = new Bitmap(currentImageFileFullName);

                            trainDataColumn = 0;
                            for (int i = 0; i < currentRawImage.Height; i++)
                            {
                                for (int j = 0; j < currentRawImage.Width; j++)
                                {
                                    Color currentPixel = currentRawImage.GetPixel(j, i);
                                    float pixelColor = 0;
                                    if (currentPixel.ToArgb() == Color.Black.ToArgb())
                                    {
                                        pixelColor = 0.0f;
                                    }
                                    else if (currentPixel.ToArgb() == Color.White.ToArgb())
                                    {
                                        pixelColor = 1.0f;
                                    }
                                    else
                                    {
                                        throw new Exception("a color different from black and white");
                                    }
                                    trainData[trainDataRow, trainDataColumn] = pixelColor;
                                    labelData[trainDataRow, 0] = classNumber;

                                    trainDataColumn++;
                                }
                            }
                            trainDataRow++;

                            currentRawImage.Dispose();

                            count++;
                        }
                        iterator3.Dispose();
                    }
                }
                iterator2.Dispose();
            }
            iterator.Dispose();

        }

        int GetTotalNumberOfTrainingDataSamples(string path, float percent = 100.0f)
        {
            int trainDataCount = 0;
            DirectoryInfo allTrainDataDirectory = new DirectoryInfo(path);

            IEnumerable<DirectoryInfo> hexDirectories = allTrainDataDirectory.EnumerateDirectories();

            IEnumerator<DirectoryInfo> iterator = hexDirectories.GetEnumerator();
            while (iterator.MoveNext())
            {
                DirectoryInfo currentHexDirectory = iterator.Current;

                IEnumerable<DirectoryInfo> directoriesInHexDirectory = currentHexDirectory.EnumerateDirectories();

                IEnumerator<DirectoryInfo> iterator2 = directoriesInHexDirectory.GetEnumerator();

                while(iterator2.MoveNext())
                {
                    DirectoryInfo currentDirectoryInHex = iterator2.Current;

                    if (currentDirectoryInHex.Name == "train")
                    {
                        IEnumerable<FileInfo> trainFilesInCurrentHexDirectory = currentDirectoryInHex.EnumerateFiles("*.png");

                        int truncatedAmount = (int) ((percent / 100.0f) * trainFilesInCurrentHexDirectory.Count());

                        trainDataCount += truncatedAmount;
                    }
                }
                iterator2.Dispose();
            }
            iterator.Dispose();

            return trainDataCount;
        }


        #region Testing

        public void TrainTest()
        {
            m_classifier.DefaultK = 5;
            m_classifier.IsClassifier = true;

            Matrix<float> trainDataMatrix = new Matrix<float>(100 * 100, 2);
            Matrix<int> labelDataMatrix = new Matrix<int>(100 * 100, 1);

            int k = 0;
            int n = 100;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    trainDataMatrix[k, 0] = (float)i;
                    trainDataMatrix[k, 1] = (float)j;

                    int cadran = 0;
                    if (i <= n / 2)
                    {
                        if (j <= n / 2)
                        {
                            cadran = 1;
                        }
                        else
                        {
                            cadran = 4;
                        }
                    }
                    else
                    {
                        if (j <= n / 2)
                        {
                            cadran = 2;
                        }
                        else
                        {
                            cadran = 3;
                        }
                    }
                    labelDataMatrix[k, 0] = (int)cadran;

                    k++;
                }
            }
            m_classifier.Train(trainDataMatrix, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, labelDataMatrix);
        }

        public float PredictTest(float x, float y)
        {
            Matrix<float> sample = new Matrix<float>(1, 2);
            sample[0, 0] = x;
            sample[0, 1] = y;
            return m_classifier.Predict(sample);
        }

        #endregion


        // TODO: uncomment these lines
        private KNearest m_classifier = new KNearest();
    }
}
