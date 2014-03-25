using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NinjectFilterConstructor.Interface;

namespace NinjectFilterConstructor.Implementation
{
    public class Irish : IPeople
    {
        public string Name()
        {
            return "Hey mate!";
        }


        public bool IsAlive()
        {
            return false;
        }
    }
}