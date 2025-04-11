using MemberNova.Admins;
using MemberNova.Helpers;

namespace Program
{

    public class Program
    {
        public static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Bienvenido a MemberNova.\n");

                Helpers.Header();

                int Seleccion = Int32.Parse(Console.ReadLine());

                switch (Seleccion)
                {
                    case 1:

                        UserHelper.UserSelection();
                            
                        break;

                    case 2:
                        MembershipHelper.MembershipSelection();
                        break;

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


