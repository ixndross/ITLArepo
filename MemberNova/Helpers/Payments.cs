using MemberNova.Admins;
using Spectre.Console;

namespace MemberNova.Helpers
{
    public class Payments
    {

        public static void PaymentPortal()
        {
            bool PaymentState = true;

            Console.WriteLine("\nPortal de pagos.\n");

            while (PaymentState)
            {
                try
                {
                    Console.Write("Seleccione una de las siguientes opciones: \n");
                    Console.WriteLine("1. Mostrar Pagos. \n2. Crear pago. \n3. Buscar pago (por numero de identificacion).\n4. Reembolsar Pago  \n5. Salir.\n");

                    int PaymentSelection = Convert.ToInt32(Console.ReadLine());

                    switch (PaymentSelection)
                    {
                        case 1:
                            MostrarPagos();

                            break;

                        case 2:
                            PagoManual();

                            break;

                        case 3:
                            BuscarPagos();

                            break;

                        case 4:
                            ReembolsarPagos();

                            break;

                        case 5:
                            PaymentState = false;
                            Console.Clear();
                            break;

                        default:
                            Console.WriteLine("Por favor, introducir una entrada válida.");
                            break;

                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: Formato de entrada no válido.\nIntente nuevamente.\n {ex.Message}");
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ha ocurrido un error inesperado. Por favor intente de nuevo.");
                    return;
                }
            }
        }

        static Table PaymentTable()
        {
            var table = new Table();
            table.Border = TableBorder.Square;
            table.ShowRowSeparators = true;
            table.AddColumn("ID");
            table.AddColumn("Fecha");
            table.AddColumn("Concepto");
            table.AddColumns("Usuario cobrado");
            table.AddColumn("Subtotal");
            table.AddColumn("Descuento");
            table.AddColumn("TOTAL");

            return table;
        }


        public static void PrintPayments(int id, Table table)
        {
            var context = new DataContext();
            var pago = context.Pagos.FirstOrDefault(p => p.PayID == id);

            table.AddRow($"{pago.PayID}", $"{pago.Fecha}", $"{pago.Concepto}", $"{context.Usuarios.FirstOrDefault(p => p.ID == pago.UserChargedID).GetFullName()}", $"{pago.Subtotal}", $"{pago.Descuento}", $"{pago.GetTotal()}");
        }


        //Payment processor's API can be included in this section as an authomatic way of charging users and members.

        //static void Payments() { }


        static void PagoManual()
        {   
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            var pago = new Pago();

            pago.Fecha = DateTime.Today;
            Console.Write("Escriba el concepto del nuevo pago: ");
            pago.Concepto = Console.ReadLine();
            Console.WriteLine("Escriba el numero de identificacion del usuario que ha realizado el pago: ");
            Users.ShowUsers();
            pago.UserChargedID = Convert.ToInt32(Console.ReadLine());
            var usuario = context.Usuarios.FirstOrDefault(u => u.ID == pago.UserChargedID);

            if (usuario is null)
            {
                Console.WriteLine("No se ha encontrado el usuario.");
                return;
            }

            Console.WriteLine("Introduzca el total del nuevo pago.");
            pago.Subtotal = decimal.Parse(Console.ReadLine());
            pago.Descuento = 0;
            Console.WriteLine("¿Presenta un reembolso este pago? Presione 1 para confirmar, o cualquier otro boton para rechazar.");
            if (Console.ReadLine() == "1")
            {
                Console.WriteLine("Introduzca el monto del reembolso: ");
                pago.Descuento = decimal.Parse(Console.ReadLine());
            }
            else
            {
                pago.Descuento = 0;
            }

            pago.Total = pago.GetTotal();

            context.Pagos.Add(pago);

            context.SaveChanges();
        }


        static void MostrarPagos()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            var table = PaymentTable();

            foreach (var pago in Pagos)
            {
                PrintPayments(pago.PayID, table);
            }
            AnsiConsole.Write(table);
        }


        static void BuscarPagos()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            var table = PaymentTable();

            Console.WriteLine("Introduzca el numero de identificacion de la pago: ");
            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            PrintPayments(SelectedId, table);
        }

        static void ReembolsarPagos()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            var table = PaymentTable();

            Console.WriteLine("Introduzca el numero de identificacion del pago a reembolsar: ");

            foreach(var pago in Pagos)
            {
                PrintPayments(pago.PayID, table);
            }
            AnsiConsole.Write(table);

            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var Pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            if(Pago is null)
            {
                Console.WriteLine("No se ha encontrado el pago solicitado. Intente de nuevo.");
                return;
            }

            Console.WriteLine("Introduzca el monto a reembolsar: ");
            Pago.Descuento += decimal.Parse(Console.ReadLine());

            context.SaveChanges();
        }


    }
}

