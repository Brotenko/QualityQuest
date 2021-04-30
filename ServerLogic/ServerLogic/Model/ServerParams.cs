using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model
{
    public class ServerParams
    {
        public string ServerURL;
        public int PAWebPagePort;
        public string MCWebSocketPort;
        public string CertFilePath;
        public string CertPW;
        public string PWHash;
        public string Salt;
        public int LogLevel;
        public int LogOutPutType;
    }
}
