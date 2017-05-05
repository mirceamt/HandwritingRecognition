using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HandwritingRecognition.ImageProcessing;

namespace HandwritingRecognition.Classification
{
    class ClassifyTool
    {
        private static ClassifyTool m_instance = new ClassifyTool();

        private ClassifyTool() { }

        public static ClassifyTool Instance
        {
            get
            {
                return m_instance;
            }
        }

        public void TrainKNN(string path)
        {
            m_knnClassifier.Train(path);
        }

        public string ClassifyUsingKNN(ConnectedComponent connectedComponent)
        {
            // TODO
            return "";
        }

        #region Testing

        public void TrainTestKNN()
        {
            m_knnClassifier.TrainTest();
        }

        public float PredictTestKNN(float x, float y)
        {
            return m_knnClassifier.PredictTest(x, y);
        }

        #endregion

        private KNNClassifier m_knnClassifier = new KNNClassifier();



    }
}
