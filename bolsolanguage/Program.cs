using System;
using System.Collections.Generic;

using System.IO;
using System.Text.RegularExpressions;

namespace bolsolanguage
{
    class Program
    {
        static Dictionary<string, int> varInt = new Dictionary<string, int>();
        static void CodeInterpreter(string code__)
        {
            string codigo__refact = String.Empty;
            string codigo = String.Empty;
            string codigoFixed = String.Empty;
            string codigo_func = String.Empty;

            string codigo__l = String.Empty;

            string memoryLines = String.Empty;

            //Refact
            {
                string[] _1 = code__.Split(' ');
                codigo_func = _1[0];
                for (int x = 0; x < _1.Length; x++)
                {

                    if (x != 0)
                    {
                        codigo = codigo + _1[x];
                    }
                }
                code__ = codigo;

            }


            string concSave = String.Empty;

            bool hasConc = false;
            int saveConcLine = 0;

            for (int i = 0; i < code__.Length; i++)
            {
                if (code__[i] == '+')
                {
                    saveConcLine = i;


                    //CheckIfHasConc
                    {
                        if (code__[saveConcLine - 1] == ' ' || code__[saveConcLine - 1] == '"')
                        {
                            hasConc = true;
                            concSave = concSave + $": {saveConcLine} : ";
                        }
                    }

                }
            }

            //Read
            {
                string[] concSplit = concSave.Split(':');
                bool stop = false;
                if (concSplit.Length > 1)
                {
                    int numb = int.Parse(concSplit[1].Replace(" ", String.Empty));

                    for (int x = 0; x < numb + 32; x++)
                    {
                        if (stop == false)
                        {
                            if (code__[x] != '"')
                            {
                                codigo__refact = codigo__refact + code__[x];
                            }
                            else if (code__[x] == '"' && code__[x + 1] == ';')
                            {
                                stop = true;
                            }
                        }
                    }

                }
            }

            //Step2
            for (int i = 0; i < codigo__refact.Length; i++)
            {
                if (codigo__refact[i] == ';' || codigo__refact[i] == '+')
                {
                    //
                }
                else
                {
                    codigoFixed = codigoFixed + codigo__refact[i];
                }
            }
            //
            List<string> Codes = new List<string>();
            for (int i = 0; i < codigo.Length; i++)
            {

                if (codigo[i] == ';')
                {
                    memoryLines = memoryLines + $" : {i} : ";
                }
            }

            {

                string[] memoryLinesSplit = memoryLines.Split(':');
                if (memoryLinesSplit.Length > 1)
                {
                    string codexx = String.Empty;
                    int numbLine = int.Parse(memoryLinesSplit[1].Replace(" ", String.Empty));
                    for (int i = 0; i < numbLine; i++)
                    {
                        codexx = codexx + codigo[i];
                    }

                    if (codexx.Contains("+"))
                    {
                        int posX = 0;
                        for (int x = 0; x < codexx.Length; x++)
                        {
                            if (codexx[x] == '+')
                            {
                                posX = x;
                            }
                        }
                        if (codexx[posX] == '+' && codexx[posX - 1] == '"' || codexx[posX] == '+' && codexx[posX + 1] == ' ')
                        {
                            int save = 0;
                            bool stopthis = false;
                            codexx = codexx.Replace("\"", String.Empty).Replace("\\", String.Empty);
                            for (int l = 0; l < codigoFixed.Length; l++)
                            {
                                if (stopthis == false)
                                {
                                    if (codigoFixed[l] == codexx[l])
                                    {
                                        save = l;
                                    }
                                    else
                                    {
                                        stopthis = true;
                                    }
                                }
                            }
                            save = save + 1;



                            char Last = codexx[codexx.Length - 1];
                            bool stopaa = false;
                            for (int x = 0; x < save + 32; x++)
                            {
                                if (stopaa == false)
                                {
                                    if (codexx[x] != '+')
                                    {
                                        codigo__l = codigo__l + codexx[x];
                                        if (codexx[x] == Last)
                                        {
                                            stopaa = true;
                                        }
                                    }
                                }
                            }
                            //     Console.WriteLine(codigo__l);
                        }
                    }
                }

                codigo = codigo.Replace("\"", String.Empty).Replace(";", String.Empty);



                if (codigo_func.Contains("PACIFICAMENTE"))
                {
                    if (hasConc)

                        Console.WriteLine(codigo__l);
                    else
                    {
                        if (codigo[0] == '&')
                        {
                            codigo = varInt[codigo.Substring(1)].ToString();
                        }
                        Console.WriteLine(codigo);
                    }
                }

                if (codigo_func.Contains("OLHA"))
                {
                    string nameVar = String.Empty;
                    string byteV = String.Empty;
                    int valueVar = 0;

                    string x = codigo.Substring(2);

                    bool findName = false;
                    bool findValue = false;

                    for (int i = 0; i < x.Length; i++)
                    {
                        if (findName == false)
                        {
                            if (x[i] != '=')
                            {
                                nameVar = nameVar + x[i];
                            }
                            else
                            {
                                findName = true;
                            }
                        }
                        if (findName == true && findValue == false)
                        {
                            if (x[i] != 'T')
                            {
                                byteV = byteV + x[i];
                            }
                            else
                            {
                                findValue = true;
                            }
                        }
                    }
                    byteV = byteV.Substring(1);
                    valueVar = int.Parse(byteV);
                    varInt.Add(nameVar, valueVar);
                }

                if (codigo_func.StartsWith("DA"))
                {
                    codigo = codigo.Replace("QEUTEDOOUTRA", String.Empty);


                    //https://stackoverflow.com/questions/1968049/how-to-separate-character-and-number-part-from-string
                    Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                    Match result = re.Match(codigo);

                    string alphaPart = result.Groups[1].Value;
                    string numberPart = result.Groups[2].Value;
                    varInt[alphaPart] = varInt[alphaPart] + int.Parse(numberPart);



                }




            }

        }


        static void Main(string[] args)
        {
            string[] Codigo = File.ReadAllLines("teste.bl");

            for (int i = 0; i < Codigo.Length; i++)
            {
                CodeInterpreter(Codigo[i]);
            }

            Console.ReadKey();


        }
    }
}
