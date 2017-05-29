using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandwritingRecognition.Utils
{
    class LogModel
    {
        private String message;

        public LogModel()
        {
            message = "";
        }

        public LogModel(String messageParam)
        {
            this.message = messageParam;
        }

        public String Message
        {
            get
            {
                return message;
            }
            set
            {
                this.message = value;
            }
        }
    }
}
