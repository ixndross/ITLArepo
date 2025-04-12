using System.Data.SqlTypes;

namespace MemberNova.Helpers
{
    public class Payments
    {

        static List<Pago> Pagos = new List<Pago>();


        public static void PaymentPortal()
        {
            bool PaymentState = true;

            while (PaymentState)
            {

                Console.Write("Portal de pagos. Seleccione una de las siguientes opciones: \n");
                Console.WriteLine("1. Mostrar Pagos. \t\t2. Crear pago. \t\t3. Reembolsar Pago \t\t4. Buscar pago (por numero de identificacion). \t\t4. Salir.\n");

                int PaymentSelection = Convert.ToInt32(Console.ReadLine());

                switch (PaymentSelection)
                {
                    case 1:
                        MostrarPagos(Pagos);

                        break;

                    case 2:
                        NewPaymentManual(Pagos);

                        break;

                    case 3:
                        BuscarPagos(Pagos);

                        break;

                    case 4:
                        ReembolsarPagos(Pagos);

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
            Console.WriteLine($"\nFecha\t\tIdentificacion de pago\t\tConcepto\t\tTotal\t\tDescuento\n");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintPayments(List<Pago> Pagos, int id)
        {
            var pago = Pagos.FirstOrDefault(p => p.PayID == id);
            Console.WriteLine($"{pago.Fecha}\t\t{pago.PayID}\t\t{pago.Concepto}\t\t{pago.Total}\t\t{pago.Descuento}\n");

        }


        static void NewPaymentManual(List<Pago> Pagos)
        {
            var payid = 100000 + (Pagos.Count * 11);
            var pago = new Pago();

            pago.PayID = payid;
            Console.Write("Escriba el concepto del nuevo pago: ");
            pago.Concepto = Console.ReadLine();
            Console.WriteLine("Introduzca el total del nuevo pago.");
            pago.Descuento = 0;
            Console.WriteLine("¿Presenta un reembolso este pago? Presione 1 para confirmar, o cualquier otro boton para rechazar.");
            pago.Descuento = Convert.ToInt16(Console.ReadLine()) == 1 ? SqlMoney.Parse(Console.ReadLine()) : 0;

            Pagos.Add(pago);
        }


        static void MostrarPagos(List<Pago> Pagos)
        {
            PrintPaymentsHeader();
            foreach (var pago in Pagos)
            {
                PrintPayments(Pagos, pago.PayID);
            }
        }


        static void BuscarPagos(List<Pago> Pagos)
        {
            Console.WriteLine("Introduzca el numero de identificacion de la pago: ");
            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            PrintPaymentsHeader();
            PrintPayments(Pagos, SelectedId);
        }

        static void ReembolsarPagos(List<Pago> Pagos)
        {
            Console.WriteLine("Introduzca el numero de identificacion de la pago: ");
            int SelectedId = Convert.ToInt32(Console.ReadLine());
            var pago = Pagos.FirstOrDefault(p => p.PayID == SelectedId);

            Console.WriteLine("Introduzca el monto a reembolsar: ");
            pago.Descuento = SqlMoney.Parse(Console.ReadLine());
            pago.Total -= pago.Descuento;
        }


    }
}

