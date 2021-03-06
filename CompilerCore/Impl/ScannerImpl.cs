﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CompilerCore.Impl
{
    internal class ScannerImpl : IScanner
    {
        private string Path { get; set; } // the path to the file that will be scanned
        private List<string> Lines { get; set; } // the list of lines (with line endings removed) in the file
        private List<Tuple<string, int>> Tokens { get; set; } // useful for debugging to make sure tokens get recognized correctly

        private int LineSeek { get; set; }
        private int CharSeek { get; set; }
        
        private string CurrLine { get { return Lines[LineSeek]; } }
        private char CurrChar { get { return CurrLine[CharSeek]; } }

        private bool IsAtEol { get { return CharSeek == CurrLine.Length; } }
        private bool IsAtEof { get { return LineSeek == Lines.Count; } }

        internal ScannerImpl(string path)
        {
            Path = path;
            Reload();
        }

        public string GetNextToken()
        {
            if (IsAtEof)
            {
                throw new InvalidOperationException("The EOF has been reached.");
            }

            var lexeme = "";

            while (!IsAtEol)
            {
                var ch = CurrChar;

                if (char.IsLetter(ch))
                {
                    if (lexeme == "" || char.IsLetter(lexeme[0]))
                    {
                        lexeme += ch;
                        MoveSeekForward();
                    }
                    else
                    {
                        break;
                    }
                }
                else if (char.IsDigit(ch))
                {
                    // In C#, a string is an IEnumerable<char>.
                    // What I am passing into the IEnumerable<T>.Any methods are known as method references.
                    // One of the overloads of the .Any method takes in a Func<char,bool> parameter.
                    // This means that I can pass as an argument to .Any a reference to a method that
                    // takes a char as a parameter and returns a bool.
                    // IsDigit and IsLetter are static methods of the char (Character) value type.
                    // And since they both take in a char and return a bool, I can pass them in to .Any.
                    // I could have also passed in what is known as a lambda expression like so:
                    // lexeme.Any(c => char.IsDigit(c))
                    // The => operator can be read as "goes to" or "such that".
                    // So the above condition can be read as:
                    // "... lexeme has any characters such that the character is a digit ...
                    if (lexeme == "" || lexeme.Any(char.IsDigit) || lexeme.Any(char.IsLetter))
                    {
                        lexeme += ch;
                        MoveSeekForward();
                    }
                    else
                    {
                        break;
                    }
                }
                else if (char.IsWhiteSpace(ch))
                {
                    // The lexeme cannot be empty at this point
                    // because whitespaces are eaten at construction
                    // and at the end of each token fetch.
                    Debug.Assert(lexeme != "");

                    break;
                }
                else
                {
                    // ch is an operator at this point
                    if (lexeme == "")
                    {
                        lexeme += ch;
                        MoveSeekForward();
                    }
                    else
                    {
                        if (lexeme=="." && ch=='.')
                        {
                            lexeme += ch;
                            MoveSeekForward();
                        }
                        else if (lexeme == ":" && ch == '=')
                        {
                            lexeme += ch;
                            MoveSeekForward();
                        }

                        break;
                    }
                }
            }

            // Create the tuple first, AND THEN eat the whitespace so that way
            // the tuple gets created with the correct line number passed in;
            // when whitespace gets eaten, the line seek can move forward to
            // different lines.
            var tuple = Tuple.Create(lexeme, LineSeek + 1);
            Tokens.Add(tuple);

            EatWhitespace();

            return lexeme;
        }

        public bool HasNextToken()
        {
            return !IsAtEof;
        }

        /// <summary>
        /// (Re-)reads the file that this ScannerImpl is associated with.
        /// Resets the seek to the beginning of the file and then
        /// moves the seek to right before the first non-whitespace character
        /// (or EOF if no such character is found).
        /// This method is called by the constructor.
        /// </summary>
        private void Reload()
        {
            Lines = File.ReadLines(Path).ToList();
            Tokens = new List<Tuple<string, int>>();
            LineSeek = 0;
            CharSeek = 0;
            EatWhitespace();
        }

        private void EatWhitespace()
        {
            while (!IsAtEof && (IsAtEol || char.IsWhiteSpace(CurrChar)))
            {
                MoveSeekForward(true);
            }
        }

        private void MoveSeekForward(bool passLineEndings = false)
        {
            if (!IsAtEol)
            {
                CharSeek++;
            }
            
            while (passLineEndings && !IsAtEof && IsAtEol)
            {
                LineSeek++;
                CharSeek = 0;
            }
        }
    }
}
