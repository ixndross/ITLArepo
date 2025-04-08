using System.ComponentModel.DataAnnotations;
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

                Console.Write("Seleccione la opción deseada:\n\n1. Administrar usuarios\n2. Administrar membresias\n3. Administrar pagos\n4. Administrar Beneficios.\n5. Salir del programa.\n");
                int opcion = Int32.Parse(Console.ReadLine());

                UserHelper.PrintUserHeader();

                switch (opcion)
                {
                    case 5:
                        running = false;
                        break;
                }
            }
        }
    }
}


