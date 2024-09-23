using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public class Token
    {
        private int id;
        private String lexeme;
        private int position;

        public Token(int id, String lexeme, int position)
        {
            this.id = id;
            this.lexeme = lexeme;
            this.position = position;
        }

        public int GetId()
        {
            return id;
        }

        public String GetLexeme()
        {
            return lexeme;
        }

        public int GetPosition()
        {
            return position;
        }

        public override String ToString()
        {
            return id + " ( " + lexeme + " ) @ " + position;
        }
    }
}
