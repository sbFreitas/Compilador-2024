using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public class SemanticError : AnalysisError
    {
        public SemanticError(string msg, int position) : base(msg, position) { }

        public SemanticError(string msg) : base(msg) { }
    }
}
