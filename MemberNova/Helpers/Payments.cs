using System.Data.SqlTypes;

namespace MemberNova.Helpers
{
    public class Payments
    {

        public static void PaymentPortal()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            bool PaymentState = true;

            while (PaymentState)
            {

                Console.Write("Portal de pagos. Seleccione una de las siguientes opciones: \n");
                Console.WriteLine("1. Mostrar Pagos. \t\t2. Crear pago. \t\t3. Buscar pago (por numero de identificacion).\t\t4. Reembolsar Pago  \t\t5. Salir.\n");

                int PaymentSelection = Convert.ToInt32(Console.ReadLine());

                switch (PaymentSelection)
                {
                    case 1:
                        MostrarPagos();

                        break;

                    case 2:
                        NewPaymentManual();

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
        }


        static void PrintPaymentsHeader()
        {
            Console.WriteLine($"\nFecha\t\tIdentificacion de pago\t\tConcepto\t\tSubtotal\t\tDescuento\t\tTotal\n");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintPayments(int id)
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            var pago = Pagos.FirstOrDefault(p => p.PayID == id);
            Console.WriteLine($"{pago.Fecha}\t\t{pago.PayID}\t\t{pago.Concepto}\t\t{pago.Subtotal}\t\t{pago.Descuento}\t\t{pago.GetTotal()}\n");

        }


        static void NewPaymentManual()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            var pago = new Pago();

            pago.Fecha = DateTime.Now;
            Console.Write("Escriba el concepto del nuevo pago: ");
            pago.Concepto = Console.ReadLine();
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

            PrintPaymentsHeader();
            foreach (var pago in Pagos)
            {
                PrintPayments(pago.PayID);
            }
        }


        static void BuscarPagos()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            Console.WriteLine("Introduzca el numero de identificacion de la pago: ");
            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            PrintPaymentsHeader();
            PrintPayments(SelectedId);
        }

        static void ReembolsarPagos()
        {
            var context = new DataContext();
            List<Pago> Pagos = context.Pagos.ToList();

            Console.WriteLine("Introduzca el numero de identificacion de la pago: ");
            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            Console.WriteLine("Introduzca el monto a reembolsar: ");
            pago.Descuento += decimal.Parse(Console.ReadLine());

            context.SaveChanges();
        }


    }
}

