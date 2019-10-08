using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptRunner.Config
{
    public class Script
    {
        public Script()
        {
            Params = new List<Param>();
        }

        public string Name { get; set; }

        public string Command { get; set; }

        public string Path { get; set; }

        public List<Param> Params { get; set; }
    }
}
