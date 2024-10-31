using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public class Constants : ScannerConstants
    {
        public readonly int EPSILON = 0;
        public readonly int DOLLAR = 1;

        public readonly int t_Id = 2;
        public readonly int t_cte_int = 3;
        public readonly int t_cte_float = 4;
        public readonly int t_cte_string = 5;
        public readonly int t_palavra = 6;
        public readonly int t_main = 7;
        public readonly int t_end = 8;
        public readonly int t_if = 9;
        public readonly int t_elif = 10;
        public readonly int t_else = 11;
        public readonly int t_false = 12;
        public readonly int t_true = 13;
        public readonly int t_read = 14;
        public readonly int t_write = 15;
        public readonly int t_writeln = 16;
        public readonly int t_repeat = 17;
        public readonly int t_until = 18;
        public readonly int t_while = 19;
        public readonly int t_TOKEN_20 = 20; //"&&"
        public readonly int t_TOKEN_21 = 21; //"||"
        public readonly int t_TOKEN_22 = 22; //"!"
        public readonly int t_TOKEN_23 = 23; //"=="
        public readonly int t_TOKEN_24 = 24; //"!="
        public readonly int t_TOKEN_25 = 25; //"<"
        public readonly int t_TOKEN_26 = 26; //">"
        public readonly int t_TOKEN_27 = 27; //"+"
        public readonly int t_TOKEN_28 = 28; //"-"
        public readonly int t_TOKEN_29 = 29; //"*"
        public readonly int t_TOKEN_30 = 30; //"/"
        public readonly int t_TOKEN_31 = 31; //","
        public readonly int t_TOKEN_32 = 32; //";"
        public readonly int t_TOKEN_33 = 33; //"="
        public readonly int t_TOKEN_34 = 34; //"("
        public readonly int t_TOKEN_35 = 35; //")"
    }
}
