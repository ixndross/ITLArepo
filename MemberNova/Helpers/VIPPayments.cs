using MemberNova.Admins;
using Spectre.Console;

namespace MemberNova.Helpers
{
    public class VIPPayments
    {

        public static void VIPPaymentPortal()
        {
            bool PaymentState = true;

            Console.WriteLine("\nPortal de pagos VIP.\n");

            while (PaymentState)
            {
                try
                {
                    Console.Write("Seleccione una de las siguientes opciones: \n");
                    Console.WriteLine("1. Mostrar Pagos VIP. \n2. Crear pago. \n3. Buscar pago VIP (por numero de identificacion).\n4. Reembolsar Pago VIP  \n5. Salir.\n");

                    int PaymentSelection = Convert.ToInt32(Console.ReadLine());

                    switch (PaymentSelection)
                    {
                        case 1:
                            MostrarPagosVIP();

                            break;

                        case 2:
                            PagoVIPManual();

                            break;

                        case 3:
                            BuscarPagosVIP();

                            break;

                        case 4:
                            ReembolsarPagosVIP();

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
                    Console.ReadKey(true);
                    return;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ha ocurrido un error inesperado. Por favor intente de nuevo.");
                    Console.ReadKey(true);
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


        public static void Print_VIP_Payments(int id, Table table)
        {
            var context = new MemberNova_DataContext();
            var pago = context.PagosVIP.FirstOrDefault(p => p.PayID == id);

            table.AddRow($"{pago.PayID}", $"{pago.Fecha}", $"{pago.Concepto}", $"{context.UsuariosVIP.FirstOrDefault(p => p.ID == pago.VIPUserChargedID).GetFullName()}", $"{pago.Subtotal}", $"{pago.Descuento}", $"{pago.GetTotal()}");
        }


        //Aqui se podria incluir alguna API de procesamiento de pagos automaticos desde un metodo de pago del usuario.

        //static void Payments() { }


        static void PagoVIPManual()
        {
            var context = new MemberNova_DataContext();
            List<PagosVIP> pagos = context.PagosVIP.ToList();

            var pago = new PagosVIP();

            pago.Fecha = DateTime.Now;
            Console.Write("Escriba el concepto del nuevo pago: ");
            pago.Concepto = Console.ReadLine();

            Console.WriteLine("Escriba el numero de identificacion del usuario VIP que ha realizado el pago: ");

            VIPUsersHelpers.ShowVIPUsers();

            pago.VIPUserChargedID = Convert.ToInt32(Console.ReadLine());
            var usuario = context.UsuariosVIP.FirstOrDefault(u => u.ID == pago.VIPUserChargedID);

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

            context.PagosVIP.Add(pago);

            context.SaveChanges();
        }



        static void MostrarPagosVIP()
        {
            var context = new MemberNova_DataContext();
            List<PagosVIP> Pagos = context.PagosVIP.ToList();

            var table = PaymentTable();

            foreach (var pago in Pagos)
            {
                Print_VIP_Payments(pago.PayID, table);
            }
            AnsiConsole.Write(table);
        }


        static void BuscarPagosVIP()
        {
            var context = new MemberNova_DataContext();
            List<PagosVIP> Pagos = context.PagosVIP.ToList();

            var table = PaymentTable();

            Console.WriteLine("Introduzca el numero de identificacion del pago: ");
            try
            {
                int SelectedId = Convert.ToInt32(Console.ReadLine());
                var pago = Pagos.Where(p => p.PayID.ToString().Contains(SelectedId.ToString())).ToList();

                foreach (var pag in pago)
                {
                    Print_VIP_Payments(pag.PayID, table);
                }
                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo encontrar la identificacion del pago introducido.\nPor favor, intente de nuevo.");

            }
        }

        static void ReembolsarPagosVIP()
        {
            var context = new MemberNova_DataContext();
            List<PagosVIP> Pagos = context.PagosVIP.ToList();

            var table = PaymentTable();

            Console.WriteLine("Introduzca el numero de identificacion del pago a reembolsar: ");

            foreach (var pago in Pagos)
            {
                Print_VIP_Payments(pago.PayID, table);
            }
            AnsiConsole.Write(table);

            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var Pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            if (Pago is null)
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

