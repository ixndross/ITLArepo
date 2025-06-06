﻿using MemberNova.Admins;
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
                    Console.WriteLine("1. Mostrar Pagos regulares. \n2. Crear pago. \n3. Buscar pago (por numero de identificacion).\n4. Reembolsar Pago  \n5. Salir.\n");

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
            var context = new MemberNova_DataContext();
            var pago = context.PagosRegulares.FirstOrDefault(p => p.PayID == id);

            table.AddRow($"{pago.PayID}", $"{pago.Fecha}", $"{pago.Concepto}", $"{context.UsuariosRegulares.FirstOrDefault(p => p.ID == pago.UserChargedID).GetFullName()}", $"{pago.Subtotal}", $"{pago.Descuento}", $"{pago.GetTotal()}");
        }


        //Payment processor's API can be included in this section as an authomatic way of charging users and members.

        //static void Payments() { }


        static void PagoManual()
        {
            var context = new MemberNova_DataContext();
            List<PagoRegular> Pagos = context.PagosRegulares.ToList();

            var pago = new PagoRegular();

            pago.Fecha = DateTime.Now;
            Console.Write("Escriba el concepto del nuevo pago: ");
            pago.Concepto = Console.ReadLine();

            Console.WriteLine("Escriba el numero de identificacion del usuario regular que ha realizado el pago: ");
            RegUserHelpers.ShowRegUsers();
            pago.UserChargedID = Convert.ToInt32(Console.ReadLine());

            var usuario = context.UsuariosRegulares.FirstOrDefault(u => u.ID == pago.UserChargedID);

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

            context.PagosRegulares.Add(pago);

            context.SaveChanges();
        }


        static void MostrarPagos()
        {
            var context = new MemberNova_DataContext();
            List<PagoRegular> Pagos = context.PagosRegulares.ToList();

            var table = PaymentTable();

            foreach (var pago in Pagos)
            {
                PrintPayments(pago.PayID, table);
            }
            AnsiConsole.Write(table);
        }


        static void BuscarPagos()
        {
            var context = new MemberNova_DataContext();
            List<PagoRegular> Pagos = context.PagosRegulares.ToList();

            var table = PaymentTable();

            Console.WriteLine("Introduzca el numero de identificacion del pago: ");

            try
            {
                int SelectedId = Convert.ToInt32(Console.ReadLine());
                var pago = Pagos.Where(p => p.PayID.ToString().Contains(SelectedId.ToString())).ToList();

                foreach (var pag in pago)
                {
                    PrintPayments(pag.PayID, table);
                }

                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo encontrar la identificacion del pago introducido.\nPor favor, intente de nuevo.\n\n");

            }
        }

        static void ReembolsarPagos()
        {
            var context = new MemberNova_DataContext();
            List<PagoRegular> Pagos = context.PagosRegulares.ToList();

            var table = PaymentTable();

            Console.WriteLine("Introduzca el numero de identificacion del pago a reembolsar: ");

            foreach (var pago in Pagos)
            {
                PrintPayments(pago.PayID, table);
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

