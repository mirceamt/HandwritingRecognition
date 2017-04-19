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

        public ClassifyTool Instance
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

        private KNNClassifier m_knnClassifier = new KNNClassifier();



    }
}
