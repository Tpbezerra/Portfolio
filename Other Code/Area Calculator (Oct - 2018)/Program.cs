using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool powerShell = true;
            
            if (powerShell)
            {
                int sum = 1;
                int tal = 0;
                string equation = "";
                string calculation = "";

                Console.WriteLine();

                for (int i = 0; i < args.Length; i++)
                {
                    int num = 1;

                    if (int.TryParse(args[i], out num))
                    {
                        sum *= num;

                        Console.WriteLine("Tal " + (tal + 1) + " = " + num);

                        if (tal == 0)
                            equation += num;
                        else
                            equation += " * " + num;

                        tal++;
                    }
                }

                switch (tal)
                {
                    case (0):
                        calculation = "Error!";
                        break;
                    case (1):
                        calculation = "Width = ";
                        break;
                    case (2):
                        calculation = "Area = ";
                        break;
                    case (3):
                        calculation = "Volume = ";
                        break;
                    default:
                        calculation = "Product = ";
                        break;
                }

                if (tal != 0)
                    Console.WriteLine(calculation + equation + " = " + sum);
                else
                    Console.WriteLine(calculation);

                Console.WriteLine();
            }
            else
            {
                CreateCalc();
            }
        }

        public static void CreateCalc()
        {
            int area, width, height;
            Console.Write("Ange Längd: ");

            if (!int.TryParse(Console.ReadLine(), out height))
            {
                Console.WriteLine("Error!");
                Console.WriteLine();
                CreateCalc();
            }

            Console.Write("Ange Höjd: ");

            if (!int.TryParse(Console.ReadLine(), out width))
            {
                Console.WriteLine("Error!");
                Console.WriteLine();
                CreateCalc();
            }

            area = width * height;
            Console.WriteLine("Area = " + width + " * " + height + " = " + area);
            Console.WriteLine();
            CreateCalc();
        }
    }
}
