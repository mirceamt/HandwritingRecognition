using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Utils
{
    class CommonUtils
    {
        public static byte[] TransformIntTo4Bytes(int x)
        {
            return BitConverter.GetBytes(x);
        }

        public static int Transform4BytesToInt(byte[] b)
        {
            return BitConverter.ToInt32(b, 0);
        }
    }
}
