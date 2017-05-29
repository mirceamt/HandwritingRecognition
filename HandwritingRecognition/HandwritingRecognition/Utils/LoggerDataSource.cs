using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandwritingRecognition.Utils
{
    class LoggerDataSource
    {
        private List<LogModel> m_messages = null;
        private BindingList<LogModel> m_bindingList = null;
        private BindingSource m_dataSource = null;
        
        public LoggerDataSource()
        {
            m_messages = new List<LogModel>();
            m_bindingList = new BindingList<LogModel>(m_messages);
            m_dataSource = new BindingSource();
            m_dataSource.DataSource = m_messages;
        }

        public void AddMessage(String message)
        {
            this.m_messages.Add(new LogModel(message));
        }

        public List<LogModel> GetAllMessages()
        {
            return m_messages;
        }

        public List<LogModel> RawLogs
        {
            get
            {
                return m_messages;
            }
        }

        public BindingList<LogModel> BindingListOfLogs
        {
            get
            {
                return m_bindingList;
            }
        }

        public BindingSource DataSource
        {
            get
            {
                return m_dataSource;
            }
        }
    
    }
}
