using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public class AnalysisError : Exception
    {
        private int position;

        public AnalysisError(String msg, int position) : base(msg)
        { 
            this.position = position;
        }

        public AnalysisError(String msg) : base(msg)
        {
            this.position = -1;
        }

        public int GetPosition()
        {
            return position;
        }

        public String ToString()
        {
            return base.ToString() + ", @ " + position;
        }
    }
}
