using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticForceCalc
{
    public class Program
    {
        static void Main(string[] args)
        {
            Calculation calculation;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "Kinetic Energy Calculator";

            Console.Write("Type 'store' after equation to keep it. Type 'clear' to clear the console.");
            Console.WriteLine(Environment.NewLine + "Calculations - Kinetic = 'k', potential = 'p'" + Environment.NewLine);

            int cursorStartPos = Console.CursorTop;

            Display();

            void Display()
            {
                double mass, velocity, answer;

                Console.Write("Calculation: ");
                string line = Console.ReadLine();
                if (line == "k" || line == "K")
                    calculation = Calculation.kinetic;
                else if (line == "p" || line == "P")
                    calculation = Calculation.potential;
                else
                {
                    if (line == "clear")
                        ClearCalculations();

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();

                    Display();
                }

                Console.Write("Enter mass: ");
                line = Console.ReadLine();
                if (!double.TryParse(line, out mass))
                {
                    if (line == "clear")
                        ClearCalculations();

                    for (int i = 1; i <= 2; i++)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                    }

                    Display();
                }

                if (calculation == Calculation.kinetic)
                    Console.Write("Enter velocity: ");
                else if (calculation == Calculation.potential)
                    Console.Write("Enter Height: ");

                line = Console.ReadLine();
                if (!double.TryParse(line, out velocity))
                {
                    if (line == "clear")
                        ClearCalculations();

                    for (int i = 1; i <= 3; i++)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                    }

                    Display();
                }

                answer = Calculations.KineticEnergy(mass, velocity);

                if (calculation == Calculation.potential)
                    answer = Calculations.PotentialEnergy(mass, velocity);

                if (answer >= 1000000000)
                    Console.WriteLine("Energy = " + (answer / 1000000000).ToString() + "GJ");
                else if (answer >= 1000000)
                    Console.WriteLine("Energy = " + (answer / 1000000).ToString() + "MJ");
                else if (answer >= 1000)
                    Console.WriteLine("Energy = " + (answer / 1000).ToString() + "kJ");
                else
                    Console.WriteLine("Energy = " + answer.ToString() + "J");

                line = Console.ReadLine();

                if (line == "store")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                    Console.WriteLine();
                    Display();
                }
                else if (line == "clear")
                {
                    ClearCalculations();
                }
                else if (line == "hejjagheterjonas")
                {
                    while (true)
                    {
                        Console.BackgroundColor = GetRandomColor();
                        Console.WriteLine(" ");
                    }
                }

                for (int i = 1; i <= 5; i++)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();
                }
                
                Display();
            }

            void ClearCalculations()
            {
                do
                {
                    ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                } while (Console.CursorTop > cursorStartPos);

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

        public static ConsoleColor GetRandomColor()
        {
            Array array = Enum.GetValues(typeof(ConsoleColor));
            Random r = new Random();
            return (ConsoleColor)array.GetValue(r.Next(array.Length));
        }
    }

    public enum Calculation { kinetic, potential };

}
