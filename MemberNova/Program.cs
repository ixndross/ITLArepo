using MemberNova.Helpers;
using Spectre.Console;

namespace Program
{

    public class Program
    {
        public static void Main()
        {
            bool running = true;

            var context = new DataContext();

            while (running)
            {
                Console.Clear();

                Console.WriteLine("Bienvenido a MemberNova.\n"); //Try_catch needed

                Helpers.Header();

                int Seleccion = Int32.Parse(Console.ReadLine());

                switch (Seleccion)
                {
                    case 1:

                        Users.UserSelection();
                            
                        break;

                    case 2:
                        Memberships.MembershipSelection();
                        break;

                    case 3:
                        Payments.PaymentPortal();

                        break;

                    case 4:
                        Benefits.BenefitsSelection();

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


