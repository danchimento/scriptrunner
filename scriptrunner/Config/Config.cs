using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Config
{
    public class Config
    {
        public Config()
        {
            Scripts = new List<Script>();
        }

        public List<Script> Scripts { get; set; }
    }
}
