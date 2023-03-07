using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class MyDependency : IMyDependency
    {
        public bool WriteMessage(string message)
        {
            return true;
        }
    }
}