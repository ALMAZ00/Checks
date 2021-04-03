using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace CheckLibrary.Server
{
    class ServerUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
