using Spectre.Console;

namespace MemberNova.Helpers
{
    public class Memberships
    {
        public static void MembershipSelection()
        {
            //var context = new DataContext();

            bool MemberState = true;

            Console.WriteLine("\nPortal de membresias.");

            while (MemberState)
            {
                try
                {
                    Console.Write("\nSeleccione una de las siguientes opciones: \n");
                    Console.WriteLine("1. Añadir Membresias.\n2. Mostrar membresias.\n3. Modificar detalles de membresia\n4. Salir.\n");

                    int MemberSelection = Convert.ToInt32(Console.ReadLine());

                    switch (MemberSelection)
                    {
                        case 1:
                            NuevaMembresia();

                            break;

                        case 2:
                            ShowMembresias();

                            break;

                        case 3:
                            ModificarMembresia();

                            break;

                        case 4:
                            MemberState = false;
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

        static Table MembershipTable()
        {
            var table = new Table();
            table.Border = TableBorder.Square;
            table.ShowRowSeparators = true;
            table.AddColumn("ID");
            table.AddColumn("Nombre");
            table.AddColumn("Descripcion");
            table.AddColumn("Precio");
            table.AddColumn("Parametros de exclusividad");
            table.AddColumn("Numero de miembros");

            return table;
        }


        static void PrintMembership(int id, Table table)
        {
            var context = new MemberNova_DataContext();

            var Membresia = context.Membresias.FirstOrDefault(p => p.MiD == id);

            table.AddRow($"{Membresia.MiD}", $"{Membresia.Tipo}", $"{Membresia.Descripcion}", $"{Membresia.Total}", $"{Membresia.IsExclusive}", $"{context.UsuariosRegulares.Where(p => p.TipoMembresia == Membresia.MiD).ToList().Count +
                                                                                                                                                   context.UsuariosVIP.Where(p => p.TipoMembresia == Membresia.MiD).ToList().Count()}");
        }
        static void NuevaMembresia()
        {

            //try
            //{
            var context = new MemberNova_DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();
            var membresia = new Membresia();
            Console.Write("Escriba el nombre de la nueva membresia: ");
            membresia.Tipo = Console.ReadLine();
            Console.WriteLine("Introduzca la descripcion de la nueva membresia.");
            membresia.Descripcion = Console.ReadLine();
            Console.WriteLine("Introduzca el costo total de la nueva membresia.");
            membresia.Total = decimal.Parse(Console.ReadLine());
            membresia.IsExclusive = "N/A";
            Console.WriteLine("¿Es esta membresia exclusiva?\nPresione 1 para confirmar. Presione otro boton para rechazar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Introduzca los parametros de la exclusividad de este tipo de membresia.");
                membresia.IsExclusive = Console.ReadLine();
            }
            else
            {
                membresia.IsExclusive = "N/A";
            }

            membresia.NumMiembros = context.UsuariosRegulares.Where(p => p.TipoMembresia == membresia.MiD).ToList().Count();

            context.Membresias.Add(membresia);
            context.SaveChanges();
            //}
            //catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            //{
            //    Console.WriteLine("ERROR AL CREAR LA MEMBRESIA.\n\nIntroduzca una entrada valida.");
            //    Console.ReadKey();
            //}
            //finally
            //{
            //    Console.Clear();
            //}

        }


        internal static void ShowMembresias()
        {
            var context = new MemberNova_DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            var table = MembershipTable();

            foreach (var mbship in Membresias)
            {
                PrintMembership(mbship.MiD, table);
            }
            AnsiConsole.Write(table);

            context.SaveChanges();
        }

        static void ModificarMembresia()
        {
            var context = new MemberNova_DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            Console.WriteLine("\nIntroduzca el numero de identificacion de la membresia a modificar: ");

            var table = MembershipTable();
            ShowMembresias();

            Console.WriteLine("\nIntroduzca el numero de identificacion de la membresia a modificar: ");

            var id = Convert.ToInt32(Console.ReadLine());
            var mbship = Membresias.FirstOrDefault(c => c.MiD == id);

            if (mbship == null)
            {
                Console.WriteLine("No se ha encontrado la membresia seleccionada.\n");
                return;
            }

            Console.WriteLine("\n Seleccione el parametro a modificar:\n1. Nombre de la membresia.\n2. Descripcion. \n3. Precio: ");
            var sel = Int32.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1:

                    Console.WriteLine($"\nEscriba el nuevo nombre para {mbship.Tipo}.");
                    string newType = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el nombre esta membresia? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        mbship.Tipo = newType;
                        Console.WriteLine("El nombre de la membresia ha sido actualizado con exito.\n");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el nombre de la membresia.\n");
                    }
                    break;

                case 2:
                    Console.WriteLine($"\nEscriba la nueva descripcion para {mbship.Tipo}.");
                    string newDesc = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar la descripcion de esta membresia, {mbship.Tipo.ToUpper()}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        mbship.Descripcion = newDesc;
                        Console.WriteLine("La descripcion de la membresia ha sido actualizada con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha cambiado la descripcion de {mbship.Tipo}");
                    }
                    break;

                case 3:
                    Console.WriteLine($"\nEscriba el nuevo precio de la membresia:");
                    decimal newPrice = decimal.Parse(Console.ReadLine());

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el precio de {mbship.Tipo.ToUpper()} de {mbship.Total} a {newPrice}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        mbship.Total = newPrice;
                        Console.WriteLine($"El precio de esta membresia ha sido actualizado con exito.\n PRECIO ACTUAL: {mbship.Total}");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha cambiado el precio de {mbship.Tipo}.\nPRECIO ACTUAL: {mbship.Total}");
                    }
                    break;

                default:
                    Console.WriteLine("Introduzca una entrada valida.");
                    break;

            }
            context.SaveChanges();

        }

    }
}

