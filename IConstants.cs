using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public interface IConstants : IScannerConstants
    {
        public const int EPSILON = 0;
        public const int DOLLAR = 1;

        public const int t_Id = 2;
        public const int t_cte_int = 3;
        public const int t_cte_float = 4;
        public const int t_cte_string = 5;
        public const int t_palavra = 6;
        public const int t_main = 7;
        public const int t_end = 8;
        public const int t_if = 9;
        public const int t_elif = 10;
        public const int t_else = 11;
        public const int t_false = 12;
        public const int t_true = 13;
        public const int t_read = 14;
        public const int t_write = 15;
        public const int t_writeln = 16;
        public const int t_repeat = 17;
        public const int t_until = 18;
        public const int t_while = 19;
        public const int t_TOKEN_20 = 20; //"&&"
        public const int t_TOKEN_21 = 21; //"||"
        public const int t_TOKEN_22 = 22; //"!"
        public const int t_TOKEN_23 = 23; //"=="
        public const int t_TOKEN_24 = 24; //"!="
        public const int t_TOKEN_25 = 25; //"<"
        public const int t_TOKEN_26 = 26; //">"
        public const int t_TOKEN_27 = 27; //"+"
        public const int t_TOKEN_28 = 28; //"-"
        public const int t_TOKEN_29 = 29; //"*"
        public const int t_TOKEN_30 = 30; //"/"
        public const int t_TOKEN_31 = 31; //","
        public const int t_TOKEN_32 = 32; //";"
        public const int t_TOKEN_33 = 33; //"="
        public const int t_TOKEN_34 = 34; //"("
        public const int t_TOKEN_35 = 35; //")"
    }
}
