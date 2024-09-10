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

        public AnalysisError(String msg, int position)
        {
            super(msg);
            this.position = position;
        }

        public AnalysisError(String msg)
        {
            super(msg);
            this.position = -1;
        }

        public int getPosition()
        {
            return position;
        }

        public String toString()
        {
            return super.toString() + ", @ " + position;
        }
    }
}
