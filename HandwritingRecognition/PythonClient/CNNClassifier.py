import tensorflow as tf
import DataTools

class CNNClassifier:
    def __init__(self):
        self.sess = tf.Session()
        print("Created Tensorflow session")
        pass

    def Load(self, cnnPathFilePath):
        print("Started loading the cnn...")
        pathToRestoreCheckpoints = ""
        checkpointName = ""
        checkpointStep = ""
        with open(cnnPathFilePath, "r") as f:
            pathToRestoreCheckpoints = f.readline().rstrip()
            checkpointName = f.readline().rstrip()
            checkpointStep = f.readline().rstrip()
        newSaver = tf.train.import_meta_graph(pathToRestoreCheckpoints + checkpointName + "-" + checkpointStep + ".meta")
        print("Loaded CNN meta graph")
        newSaver.restore(self.sess, pathToRestoreCheckpoints + checkpointName + "-" + checkpointStep)
        print("Loaded CNN parameters")
        
        graph = tf.get_default_graph()

        #self.x = graph.get_tensor_by_name('Placeholder:0')
        #self.y_ = graph.get_tensor_by_name('Placeholder_1:0')
        #self.y_conv = graph.get_tensor_by_name('add_4:0')
        #self.keep_prob = graph.get_tensor_by_name('Placeholder_2:0')

        self.x = graph.get_tensor_by_name('x:0')
        self.y_ = graph.get_tensor_by_name('y_:0')
        self.y_conv = graph.get_tensor_by_name('y_conv:0')
        self.keep_prob = graph.get_tensor_by_name('keep_prob:0')


    def ReadSimpleExample(self):
        ret = []
        label = ""
        with open(r"C:\HandwritingRecognition\CommonResources\simpleExample.txt") as f:
            str = f.readline().rstrip()
            label = f.readline().rstrip()

            for ch in str:
                x = 1.0 if ch == '1' else 0.0
                ret.append(x)
        return (ret, label)

    def Classify(self, example, label = None):
        #send example as an array of 1024 floats of 0.0 / 1.0 meaning the vectorized version of the square image of 32x32
        yPredictedVector = self.sess.run(self.y_conv, feed_dict={self.x: [example], self.keep_prob: 1.0})

        predictedCharactersMultiplePossibilites = DataTools.classVectorToMultipleCharacters(yPredictedVector[0].tolist())
        return predictedCharactersMultiplePossibilites

