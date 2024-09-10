using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public class SyntaticError : AnalysisError
    {
        public SyntaticError(String msg, int position)
        {
            super(msg, position);
        }

        public SyntaticError(String msg)
        {
            super(msg);
        }
    }
}
