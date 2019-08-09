using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFChat
{
    class ServerUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public OperationContext OperationContext { get; set; }
    }
}
