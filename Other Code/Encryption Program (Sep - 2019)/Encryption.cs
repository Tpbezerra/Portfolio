using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionProgram
{
    class Encryption
    {
        static void Main(string[] args)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyzåäö §½1!2@3#£4¤$5%6&7/{8([9)]0=}+?´`¨^~'*-_.:,;<>|";
            string alphabetCapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ §½1!2@3#£4¤$5%6&7/{8([9)]0=}+?´`¨^~'*-_.:,;<>|";
            string replacementAlphabet = "qwertyuioplkjhgfdsazxcvbnmöåä 025~,1.(/+{)4;87>%*:'§}3&¤@[-¨£$´^!`½<=?_]6|9#";
            string replacementAlphabetCapital = "QWERTYUIOPLKJHGFDSAZXCVBNMÖÅÄ 025~,1.(/+{)4;87>%*:'§}3&¤@[-¨£$´^!`½<=?_]6|9#";

            Start();

            void Start()
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Competition encryption program - Kindly use english as it is the most robust" + Environment.NewLine);
                Console.WriteLine("e = encode, d = decode, clear = clear the console");
            }

            int cursorStart = Console.CursorTop;

            Display();

            void Display()
            {
                bool encode = true;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Environment.NewLine + "Type: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string line = Console.ReadLine();
                switch (line)
                {
                    case "e":
                        break;
                    case "d":
                        encode = false;
                        break;
                    case "E":
                        break;
                    case "D":
                        encode = false;
                        break;
                    case "clear":
                        ClearConsole();
                        break;
                    default:
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                        Console.SetCursorPosition(0, Console.CursorTop - 1);

                        Display();
                        return;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Message: ");
                Console.ForegroundColor = ConsoleColor.Green;
                line = Console.ReadLine();
                if (line.Length == 0)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                    }

                    Console.SetCursorPosition(0, Console.CursorTop - 1);

                    Display();
                    return;
                }

                char[,] grid = new char[line.Length / 2 + 2,line.Length / 2 + 1];
                for (int x1 = 0; x1 < grid.GetLength(0); x1++)
                {
                    for (int y1 = 0; y1 < grid.GetLength(1); y1++)
                    {
                        grid[x1, y1] = '❁';
                    }
                }
                
                int velocityX = 1;
                int velocityY = -1;

                string result = "";

                if (encode)
                    result = Encode(grid, line, velocityX, velocityY);
                else
                    result = Decode(grid, line, velocityX, velocityY);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Result: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(result + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;

                Display();
            }

            string Encode(char[,] grid, string line, int velocityX, int velocityY)
            {
                string lineReplacement = "";
                for (int c = 0; c < line.Length; c++)
                {
                    for (int l = 0; l < alphabet.Length; l++)
                    {
                        if (line[c] == alphabet[l])
                        {
                            lineReplacement += replacementAlphabet[l];
                            break;
                        }
                        else if (line[c] == alphabetCapital[l])
                        {
                            lineReplacement += replacementAlphabetCapital[l];
                            break;
                        }
                    }
                }
                line = lineReplacement;

                int x = 0;
                int y = grid.GetLength(1) - 1;

                string value = "";

                grid[x, y] = line[0];

                x += velocityX;
                y += velocityY;

                for (int i = 1; i < line.Length; i++)
                {

                    grid[x, y] = line[i];

                    if (x == grid.GetLength(0) - 1 || x == 0)
                        velocityX *= -1;
                    if (y == grid.GetLength(1) - 1 || y == 0)
                        velocityY *= -1;

                    x += velocityX;
                    y += velocityY;

                    if (grid[x, y] != '❁')
                    {
                        if (x == grid.GetLength(0) - 1 || x == 0)
                            velocityX *= -1;
                        if (y == grid.GetLength(1) - 1 || y == 0)
                            velocityY *= -1;

                        x += velocityX;
                        y += velocityY;
                    }
                }

                for (int y1 = grid.GetLength(1) - 1; y1 >= 0; y1--)
                {
                    for (int x1 = 0; x1 < grid.GetLength(0); x1++)
                    {
                        if (grid[x1, y1] != '❁')
                            value += grid[x1, y1];
                    }
                }

                return value;
            }

            string Decode(char[,] grid, string line, int velocityX, int velocityY)
            {
                string lineReplacement = "";
                for (int c = 0; c < line.Length; c++)
                {
                    for (int l = 0; l < alphabet.Length; l++)
                    {
                        if (line[c] == replacementAlphabet[l])
                        {
                            lineReplacement += alphabet[l];
                            break;
                        }
                        else if (line[c] == replacementAlphabetCapital[l])
                        {
                            lineReplacement += alphabetCapital[l];
                            break;
                        }
                    }
                }
                line = lineReplacement;

                string value = "";

                int x = 0;
                int y = grid.GetLength(1) - 1;

                grid[x, y] = line[0];

                x += velocityX;
                y += velocityY;

                for (int i = 1; i < line.Length; i++)
                {
                    grid[x, y] = line[i];

                    if (x == grid.GetLength(0) - 1 || x == 0)
                        velocityX *= -1;
                    if (y == grid.GetLength(1) - 1 || y == 0)
                        velocityY *= -1;

                    x += velocityX;
                    y += velocityY;

                    if (grid[x, y] != '❁')
                    {
                        if (x == grid.GetLength(0) - 1 || x == 0)
                            velocityX *= -1;
                        if (y == grid.GetLength(1) - 1 || y == 0)
                            velocityY *= -1;

                        x += velocityX;
                        y += velocityY;
                    }
                }

                int i1 = 0;

                for (int y1 = grid.GetLength(1) - 1; y1 >= 0; y1--)
                {
                    for (int x1 = 0; x1 < grid.GetLength(0); x1++)
                    {
                        if (grid[x1, y1] != '❁')
                        {
                            grid[x1, y1] = line[i1];
                            i1++;
                        }
                    }
                }

                x = 0;
                y = grid.GetLength(1) - 1;
                
                value += grid[x, y];

                velocityX = 1;
                velocityY = -1;

                x += velocityX;
                y += velocityY;

                for (int i = 1; i < line.Length; i++)
                {
                    value += grid[x, y];
                    grid[x, y] = '❁';

                    if (x == grid.GetLength(0) - 1 || x == 0)
                        velocityX *= -1;
                    if (y == grid.GetLength(1) - 1 || y == 0)
                        velocityY *= -1;

                    x += velocityX;
                    y += velocityY;

                    if (grid[x, y] == '❁')
                    {
                        if (x == grid.GetLength(0) - 1 || x == 0)
                            velocityX *= -1;
                        if (y == grid.GetLength(1) - 1 || y == 0)
                            velocityY *= -1;

                        x += velocityX;
                        y += velocityY;
                    }
                }

                return value;
            }

            char[,] GetGrid(int xStart, int yStart, int velocityXStart, int velocityYStart, string line, char toTestFor, int xSize, int ySize)
            {
                char[,] value = new char[xSize, ySize];

                int x = xStart;
                int y = yStart;
                int velocityX = velocityXStart;
                int velocityY = velocityYStart;

                value[x, y] = line[0];

                x += velocityX;
                y += velocityY;

                for (int i = 1; i < line.Length; i++)
                {
                    value[x, y] = line[i];

                    if (x == value.GetLength(0) - 1 || x == 0)
                        velocityX *= -1;
                    if (y == value.GetLength(1) - 1 || y == 0)
                        velocityY *= -1;

                    x += velocityX;
                    y += velocityY;

                    if (value[x, y] != toTestFor)
                    {
                        if (x == value.GetLength(0) - 1 || x == 0)
                            velocityX *= -1;
                        if (y == value.GetLength(1) - 1 || y == 0)
                            velocityY *= -1;

                        x += velocityX;
                        y += velocityY;
                    }
                }

                return value;
            }

            void ClearConsole()
            {
                do
                {
                    ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                } while (Console.CursorTop > cursorStart);

                ClearCurrentConsoleLine();

                Display();
            }

            void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }
    }
}
