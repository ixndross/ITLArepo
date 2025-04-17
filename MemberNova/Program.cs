using System.Data.SqlTypes;
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
                try
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
                catch (SqlNullValueException)
                {
                    Console.WriteLine("Error: No se puede acceder a la base de datos.\nIntente nuevamente con una conexion segura.");
                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Formato de entrada no válido.\nIntente nuevamente.");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("Error durante la asignacion de valores: " + ex.Message + "\nIntente de nuevo.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error inesperado: " + ex.Message);
                    return;
                }
                
            }
        }
    }
}


