using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCompilador
{
    public class Semantico : Constants
    {
        // declarar os registros semânticos como atributos do analisador semântico
        private List<string> codigo = new List<string>();
        private List<string> idList = new List<string>();
        private Stack<string> pilha_tipos = new Stack<string>();
        private Stack<string> labelStack = new Stack<string>();
        private string relOperator = " ";
        private Dictionary<string, string> symbolsTable = new Dictionary<string, string>();
        private int labelCount = 0;

        public List<string> GetCodigo { get { return codigo; } }

        public void ExecuteAction(int action, Token token)
        {
            Console.Write("Ação #" + action + ", Token: " + token);
            switch (action)
            {

                case 100: Acao100(); break;
                case 101: Acao101(); break;
                case 102: Acao102(token); break;
                case 103: Acao103(token); break;
                case 104: Acao104(token); break;
                case 105: Acao105(token); break;
                case 106: Acao106(token); break;
                case 107: Acao107(); break;
                case 108: Acao108(token); break;
                case 109: Acao109(); break;
                case 110: Acao110(); break;
                case 111: Acao111(); break;
                case 112: Acao112(); break;
                case 113: Acao113(); break;
                case 114: Acao114(); break;
                case 115: Acao115(); break;
                case 116: Acao116_117("&&"); break;
                case 117: Acao116_117("||"); break;
                case 118: Acao118(); break;
                case 119: Acao119(); break;
                case 120: Acao120(); break;
                case 121: Acao121(token); break;
                case 122: Acao122(token); break;
                case 123: Acao123_124_125_126("+", token); break;
                case 124: Acao123_124_125_126("-", token); break;
                case 125: Acao123_124_125_126("*", token); break;
                case 126: Acao123_124_125_126("/", token); break;
                case 127: Acao127(token); break;
                case 128: Acao128(token); break;
                case 129: Acao129(token); break;
                case 130: Acao130(token); break;
                case 131: Acao131(); break;
                default:
                    throw new SemanticError($"Invalid action: {action}");




            }
        }
        public void Acao100()
        {
            codigo.Add(".assembly extern mscorlib {}");
            codigo.Add(".assembly _codigo_objeto {}");
            codigo.Add(".module _codigo_objeto.exe");
            codigo.Add(".class public UNICA{");
            codigo.Add(".method static public void _principal() {");
            codigo.Add(".entrypoint");
        }
        public void Acao101()
        {
            codigo.Add("ret");
            codigo.Add("}");
            codigo.Add("}");
        }

        private void Acao102(Token token)
        {
            foreach (string id in idList)
            {
                if (symbolsTable.ContainsKey(id))
                    throw new SemanticError($"Linha: {token.GetLine()}: {token.GetLexeme()} já declarado");;

                //determina tipo com base no prefixo(i_, f_, s_, b_)
                string type = SetType(id);

                //add id na tabela de símbolos
                symbolsTable[id] = type;

                codigo.Add($".locals ({type} {id})");

            }

            idList.Clear();
        }

        private void Acao103(Token token)
        {
            if (pilha_tipos.Count == 0)
                throw new SemanticError($"Linha {token.GetLine()}: nenhum tipo encontrado na pilha_tipos para a expressão.");

            //desempilha tipo
            string type = pilha_tipos.Pop();

            if (type == "int64")
                codigo.Add("conv.i8");
            //duplicar o valor da expressão p/ múltiplas atribuições
            int n = idList.Count - 1;
            for (int i = 0; i < n; i++)
                codigo.Add("dup");

            foreach (string id in idList)
            {
                if (!symbolsTable.ContainsKey(id))
                    throw new SemanticError($"Linha: {token.GetLine()}: {token.GetLexeme()} não declarado"); ;

                codigo.Add($"stloc {id}");

                if (symbolsTable[id] == "int64" && type != "int64")
                    codigo.Add("conv.i8");
            }

            idList.Clear();
        }

        private void Acao104(Token token)
        {
            //add lexeme na lista de ids
            idList.Add(token.GetLexeme());

        }

        private void Acao105(Token token)
        {
            if (!symbolsTable.ContainsKey(token.GetLexeme()))
                throw new SemanticError($"Linha: {token.GetLine()}: {token.GetLexeme()} 2 não declarado");

            string type = symbolsTable[token.GetLexeme()];
            string instructionsEntry = GenerateReadInstruction(token, token.GetLexeme());

            codigo.Add(instructionsEntry);
        }

        private void Acao106(Token token)
        {
            //código p/ gerar a cte_string
            codigo.Add($"ldstr {token.GetLexeme()}");

            //escreve cte
            codigo.Add("call void [mscorlib]System.Console::Write(string)");
        }

        private void Acao107()
        {
            codigo.Add("call void [mscorlib]System.Console::WriteLine()");
        }

        private void Acao108(Token token)
        {
            if (pilha_tipos.Count == 0)
                throw new SemanticError($"Linha: {token.GetLine()}: pilha_tipos vazia, nenhum tipo encontrado para processar.");

            string type = pilha_tipos.Pop();

            //converte int64 p/ float64
            if (type == "int64")
                codigo.Add("conv.i8");

            string instruction = GenerateWriteInstruction(type);
            codigo.Add(instruction);
        }

        private void Acao109()
        {
            //cria rótulo p/ instrução depois do "end"
            string newLabel1 = GenerateLabel();
            labelStack.Push(newLabel1);

            //cria rótulo paro o "if"
            string newLabel2 = GenerateLabel();

            //caso condição do "if" for false
            codigo.Add($"brfalse {newLabel2}");

            labelStack.Push(newLabel2);
        }

        private void Acao110()
        {
            if (labelStack.Count < 2)
                throw new SemanticError("Erro: rótulos insuficientes na pilha para a ação #110.");

            //desempilha os rótulos
            string unstackLabel2 = labelStack.Pop();
            string unstackLabel1 = labelStack.Pop();

            //desvia p/ instrução depois do "end"
            codigo.Add($"br {unstackLabel1}");

            //reempilha unstackLabel1 p/ resolver depois
            labelStack.Push(unstackLabel1);

            //rotula próxima instrução com o rótulo 2
            codigo.Add($"{unstackLabel2}:");
        }

        private void Acao111()
        {
            if (pilha_tipos.Count == 0)
                throw new SemanticError("Erro: pilha_tipos vazia, nenhum tipo encontrado para processar.");

            //desempilha rótulo
            string unstackLabel = labelStack.Pop();

            //rotula proxima instrução
            codigo.Add($"{unstackLabel}:");
        }

        private void Acao112()
        {
            //gera novo rótulo
            string newLabel = GenerateLabel();

            //caso condição do "elif" for false
            codigo.Add($"brfalse {newLabel}");

            //reempilha unstackLabel1 p/ resolver depois
            labelStack.Push(newLabel);
        }

        private void Acao113()
        {
            //gera novo rótulo
            string newLabel = GenerateLabel();

            //rotula próxima instrução com o rótulo criado
            codigo.Add($"{newLabel}:");

            //reempilha unstackLabel1 p/ resolver depois
            labelStack.Push(newLabel);
        }

        private void Acao114()
        {
            if (labelStack.Count == 0)
                throw new SemanticError("Erro: pilha de rótulos vazia, nenhum tipo encontrado para processar.");

            //desempilha rótulo
            string unstackLabel = labelStack.Pop();

            //caso condição do comando for true desvia para o primeiro comando do comando <repetição>
            codigo.Add($"brtrue {unstackLabel}");
        }

        private void Acao115()
        {
            if (labelStack.Count == 0)
                throw new SemanticError("Erro: pilha de rótulos vazia, nenhum tipo encontrado para processar.");

            //desempilha rótulo
            string unstackLabel = labelStack.Pop();

            //caso condição do comando for false desvia para o primeiro comando do comando <repetição>
            codigo.Add($"brfalse {unstackLabel}");
        }

        private void Acao116_117(string operationType)
        {
            if (pilha_tipos.Count < 2)
                throw new SemanticError("Erro: tipos insuficientes na pilha para a ação #116.");

            //desempilha os tipos
            string operationType1 = pilha_tipos.Pop();
            string operationType2 = pilha_tipos.Pop();

            //verifica compatibilidade
            if (operationType1 != "bool" || operationType2 != "bool")
                throw new SemanticError($"Erro: operação lógica binária {operationType} requer operandos do tipo bool.");

            //empilha os tipos
            pilha_tipos.Push("bool");

            // se operationType == "&&" for true utiliza o "and" na variável instruction, caso for false utiliza o "or"
            string instruction = operationType == "&&" ? "and" : "or";

            codigo.Add(instruction);
        }
        private void Acao120() // if 4 == 5 && !(true)
        {
            if (pilha_tipos.Count == 0)
                throw new SemanticError("Erro: pilha de tipos vazia, nenhum operando para o operador '!'.");

            ////verifica se o tipo do operador é bool
            //string operatorType = pilha_tipos.Pop();

            //if (operatorType != "bool")
            //    throw new SemanticError($"Erro: operador '!' requer operando do tipo 'bool', encontrado '{operatorType}'.");

            //gera o código correspondente p/ negação lógica
            codigo.Add("ldc.i4.1");
            codigo.Add("xor");
        }

        // Relacionais
        private void Acao121(Token token)
        {
            relOperator = token.GetLexeme();
        }

        private void Acao122(Token token)
        {
            string tipo1 = pilha_tipos.Pop();
            string tipo2 = pilha_tipos.Pop();

            if (tipo1 != tipo2)
            {
                throw new SemanticError($"Linha {token.GetLine()}: tipos inválidos {tipo2} e {tipo1}");
            }

            switch (relOperator)
            {
                case "==":

                    codigo.Add("ceq");
                    break;
                case "!=":
                    codigo.Add("ceq");
                    codigo.Add("ldc.i4.0");
                    codigo.Add("ceq");
                    break;
                case ">":
                    codigo.Add("cgt");
                    break;
                case "<":
                    codigo.Add("clt");
                    break;
                }
            pilha_tipos.Push("bool");
        }
        private void Acao123_124_125_126(string operation, Token token)
        {
            if (pilha_tipos.Count < 0)
                throw new SemanticError("Erro: tipos insuficientes na pilha para a ação.");

            //desempilha os tipos
            string operationType1 = pilha_tipos.Pop();
            string operationType2 = pilha_tipos.Pop();

            //determina o tipo resultante
            string resultType = SetResultType(operationType1, operationType2, operation);

            //empilha o tipo resultante
            pilha_tipos.Push(resultType);
            operation = GetArithmeticCode(operation);
            //gera código objeto correspondente 
            codigo.Add(operation);
        }

        private void Acao127(Token token)
        {
            string lexeme = token.GetLexeme();

            if (!symbolsTable.ContainsKey(lexeme))
                throw new SemanticError($"Linha: {token.GetLine()}: {lexeme} 3 não declarado.");
             
            string type = symbolsTable[lexeme];

            // Empilhar o tipo na pilha de tipos
            pilha_tipos.Push(type);

            // Gerar código para carregar o valor
            codigo.Add($"ldloc {lexeme}");

            // Se for int64, converter para float64
            if (type == "int64")
                codigo.Add("conv.r8");
        }
        private void Acao128(Token token)
        {
            pilha_tipos.Push("int64");
            codigo.Add($"ldc.i8 {token.GetLexeme()}");
            codigo.Add("conv.r8");
        }
        private void Acao129(Token token)
        {
            pilha_tipos.Push("float64");
            string lexeme = token.GetLexeme();
            lexeme = lexeme.Replace(',', '.');
            codigo.Add($"ldc.r8 {lexeme}");
        }
        private void Acao130(Token token)
        {
            pilha_tipos.Push("string");
            codigo.Add($"ldstr {token.GetLexeme()}");
        }
        private void Acao118()
        {
            pilha_tipos.Push("bool");
            codigo.Add("ldc.i4.1");
        }
        private void Acao119()
        {
            pilha_tipos.Push("bool");
            codigo.Add("ldc.i4.0");
        }
        private void Acao131()
        {
            codigo.Add("ldc.r8 -1.0");
            codigo.Add("mul");
        }
        private string GenerateLabel()
        {
            return $"L{labelCount++}";
        }

        private string GetArithmeticCode(string operation)
        {
            switch (operation)
            {
                case "+":
                    return "add";
                case "-":
                    return "sub";
                case "*":
                    return "mul";
                case "/":
                    return "div";
                default:
                    throw new SemanticError($"Erro: operador {operation} inválido");
            }
        }
        private string SetResultType(string operationType1, string operationType2, string operation)
        {
            if (operation == "/")
                return "float64";
            //regra p/ converção de tipo
            else if (operationType1 == "float64" || operationType2 == "float64")
                return "float64";
            else if (operationType1 == "int64" || operationType2 == "int64")
                return "int64";
            else
                throw new SemanticError($"Erro: operação {operation} inválida para tipos {operationType1} e {operationType2}.");
        }
        private string GenerateWriteInstruction(string type)
        {
            if (type == "int64")
                return "call void [mscorlib]System.Console::Write(int64)";
            else if (type == "float64")
                return "call void [mscorlib]System.Console::Write(float64)";
            else if (type == "string")
                return "call void [mscorlib]System.Console::Write(string)";
            else if (type == "bool")
                return "call void [mscorlib]System.Console::Write(bool)";
            else
                throw new SemanticError($"Erro: tipo desconhecido {type}.");
        }

        private string GenerateReadInstruction(Token token, string type)
        {
            if (type.StartsWith("i_"))
                return "call string [mscorlib]System.Console::ReadLine()\n call int64 [mscorlib]System.Int64::Parse(string)\n stloc " + token.GetLexeme();
            else if (type.StartsWith("f_"))
                return "call string [mscorlib]System.Console::ReadLine()\n call float64 [mscorlib]System.Double::Parse(string)\n stloc " + token.GetLexeme();
            else if (type.StartsWith("s_"))
                return "call string [mscorlib]System.Console::ReadLine()\n stloc " + token.GetLexeme();
            else if (type.StartsWith("b_"))
                return "call string [mscorlib]System.Console::ReadLine()\n call bool [mscorlib]System.Boolean::Parse(string)\n stloc " + token.GetLexeme();
            else
                throw new SemanticError($"Erro: tipo desconhecido {type} para o identificador {token.GetLexeme()}.");
        }

        private string SetType(string id)
        {
            if (id.StartsWith("i_"))
                return "int64";
            else if (id.StartsWith("f_"))
                return "float64";
            else if (id.StartsWith("s_"))
                return "string";
            else if (id.StartsWith("b_"))
                return "bool";
            else
                throw new SemanticError($"Erro: identificador {id} com prefixo inválido.");
        }

    }
}