using appCompilador;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace appCompilador;
public class Sintatico : Constants
{
    private Stack<int> stack = new Stack<int>();
    private Token currentToken;
    private Token previousToken;
    private Lexico scanner;
    private Semantico semanticAnalyser;
    private static ParserConstants pc = new ParserConstants();
    private Form1 form;
    public Token GetToken()
    {
        return currentToken;
    }
    public void SetForm(Form1 form)
    {
        this.form = form;
    }
    private static bool IsTerminal(int x)
    {
        return x < pc.FIRST_NON_TERMINAL;
    }

    private static bool IsNonTerminal(int x)
    {
        return x >= pc.FIRST_NON_TERMINAL && x < pc.FIRST_SEMANTIC_ACTION;
    }

    private static bool IsSemanticAction(int x)
    {
        return x >= pc.FIRST_SEMANTIC_ACTION;
    }

    private bool Step()
    {
        if (currentToken == null)
        {
            int pos = 0;
            if (previousToken != null)
                pos = previousToken.GetPosition() + previousToken.GetLexeme().Length;

            currentToken = new Token(DOLLAR, "EOF", pos);
        }

        int x = stack.Pop();
        int a = currentToken.GetId();
        
        if (x == EPSILON)
        {
            return false;
        }
        else if (IsTerminal(x))
        {
            if (x == a)
            {
                if (stack.Count == 0)
                    return true;
                else
                {
                    previousToken = currentToken;
                    currentToken = scanner.nextToken();
                    return false;
                }
            }
            else
            {
                throw new SyntaticError(pc.PARSER_ERROR[x], currentToken.GetPosition());
            }
        }
        else if (IsNonTerminal(x))
        {
            if (PushProduction(x, a))
                return false;
            else
                throw new SyntaticError(pc.PARSER_ERROR[x], currentToken.GetPosition());
        }
        else // IsSemanticAction(x)
        {
            semanticAnalyser.ExecuteAction(x - pc.FIRST_SEMANTIC_ACTION, previousToken);
            return false;
        }
    }
    private bool PushProduction(int topStack, int tokenInput)
    {
        int p = pc.PARSER_TABLE[topStack - pc.FIRST_NON_TERMINAL, tokenInput - 1];
        if (p >= 0)
        {
            if (p == 100)
            {
                stack.Push(0);
                return true;
            }
            int[] production = pc.PRODUCTIONS[p];
            
            for (int i = production.Length - 1; i >= 0; i--)
            {
                stack.Push(production[i]);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Parse(Lexico scanner, Semantico semanticAnalyser)
    {
        this.scanner = scanner;
        this.semanticAnalyser = semanticAnalyser;

        stack.Clear();
        stack.Push(DOLLAR);
        stack.Push(pc.START_SYMBOL);

        currentToken = scanner.nextToken();

        while (!Step())
        {
            // Continue parsing until step() returns true
        }
    }
}
