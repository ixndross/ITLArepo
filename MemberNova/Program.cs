using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Timers;
using MemberNova.Helpers;

namespace Program
{

    public class Program
    {
        public static void Main()
        {
            bool running = true;
            Console.WriteLine("Bienvenido a MemberNova.\n");

            

            while (running)
            {
                Helpers.Selection();
                int opcion = Int32.Parse(Console.ReadLine());

                UserHelper.PrintUserHeader();

                switch (opcion)
                {
                    case 1:

                    case 5:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Por favor, introducir una entrada válida.");
                        break;
                }
            }
        }
    }
}


