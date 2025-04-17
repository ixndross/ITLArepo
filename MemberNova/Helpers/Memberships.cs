using System.Data.SqlTypes;
using MemberNova.Admins;
using Microsoft.EntityFrameworkCore.Storage.Json;
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

                Console.Write("\nSeleccione una de las siguientes opciones: \n");
                Console.WriteLine("1. Añadir Membresias.\t\t2. Mostrar membresias.\t\t3. Modificar detalles de membresia\t\t4. Eliminar membresia\t\t5. Salir.\n");

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
                        RemoverMembresias();

                        break;

                    case 5:
                        MemberState = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Por favor, introducir una entrada válida.");
                        break;

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
            var context = new DataContext();

            var Membresia = context.Membresias.FirstOrDefault(p => p.MiD == id);

            table.AddRow($"{Membresia.MiD}", $"{Membresia.Tipo}", $"{Membresia.Descripcion}", $"{Membresia.Total}", $"{Membresia.IsExclusive}", $"{context.Usuarios.Where(p => p.TipoMembresia == Membresia.MiD).ToList().Count}");
        }
        static void NuevaMembresia()
        {
            var context = new DataContext();
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
            context.Membresias.Add(membresia);
            context.SaveChanges();
        }


        internal static void ShowMembresias()
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            var table = MembershipTable();

            foreach (var mbship in Membresias)
            {
                PrintMembership(mbship.MiD, table);
            }
            AnsiConsole.Write(table);
        }

        static void ModificarMembresia()
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            Console.WriteLine("\nIntroduzca el numero de identificacion de la membresia a modificar: ");

            var table = MembershipTable();
            ShowMembresias();

            Console.WriteLine("\nIntroduzca el numero de identificacion de la membresia a modificar: ");

            

            var id = Convert.ToInt32(Console.ReadLine());
            var mbship = Membresias.FirstOrDefault(c => c.MiD == id);

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
                    break;

            }
            context.SaveChanges();
            
        }


        static void RemoverMembresias()
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            Console.WriteLine("\nPor favor, digite el numero de identificacion de la membresia a eliminar.");

            ShowMembresias();

            Console.WriteLine("\nPor favor, digite el numero de identificacion de la membresia a eliminar.");

            var id = Convert.ToInt32(Console.ReadLine());
            var mbship = Membresias.FirstOrDefault(c => c.MiD == id);


            Console.WriteLine($"¿Esta seguro que quiere eliminar esta membresia? \nPresione 1 para confirmar, 2 para denegar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                context.Membresias.Remove(mbship);

                Console.WriteLine("La membresia ha sido exitosamente eliminada.");
            }
            else
            {
                Console.WriteLine("La membresia no fue eliminada.");
            }

            context.SaveChanges();
        }


    }
}

