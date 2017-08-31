using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoodooPOS.objects
{
    public class SecurityLevelClass
    {
        int id = -1;
        int securityLevel = -1;
        string name = "";

        public SecurityLevelClass(string name, int SecurityLevel)
        {
            this.securityLevel = SecurityLevel;
            this.name = name;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int SecurityLevel
        {
            get { return securityLevel; }
            set { securityLevel = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
