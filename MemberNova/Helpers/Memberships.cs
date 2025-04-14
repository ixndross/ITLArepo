using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace MemberNova.Helpers
{ 
    public class Memberships
    {
        public static void MembershipSelection()
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            bool MemberState = true;

            while (MemberState)
            {

                Console.Write("Portal de membresias. Seleccione una de las siguientes opciones: \n");
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


        static void PrintMembershipheader()
        {
            Console.WriteLine($"\nMbshipID\t\tTipo\t\tDescripcion\t\tPrecio\t\tEstado de Excusividad\n");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintMembership(int id)
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            var Membresia = Membresias.FirstOrDefault(p => p.MiD == id);
            Console.WriteLine($"{Membresia.MiD}\t\t{Membresia.Tipo}\t\t{Membresia.Descripcion}\t\t{Membresia.Total}\t\t{Membresia.IsExclusive}\n");

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
            Console.WriteLine("¿Es esta membresia exclusiva? Presione 1 para confirmar, o cualquier otro boton para rechazar.");

            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Introduzca los parametros de la exclusividad de este tipo de membresia.");
                membresia.IsExclusive = Console.ReadLine();
            }

            context.Membresias.Add(membresia);

            context.SaveChanges();
        }


        static void ShowMembresias()
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList();

            PrintMembershipheader();
            foreach (var mbship in Membresias)
            {
                PrintMembership(mbship.MiD);
            }
        }

        static void ModificarMembresia()
        {
            var context = new DataContext();
            List<Membresia> Membresias = context.Membresias.ToList(); 
            
            PrintMembershipheader();

            foreach (var membership in Membresias)
            {
                PrintMembership(membership.MiD);
            }
            Console.WriteLine("\nIntroduzca el numero de identificacion de la membresia a modificar: ");

            var id = Convert.ToInt32(Console.ReadLine());
            var mbship = Membresias.FirstOrDefault(c => c.MiD == id);

            Console.WriteLine("\n Seleccione el parametro a modificar:\n1. Nombre de la membresia.\n2. Descripcion. \n3. Precio): ");
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
                        Console.WriteLine("El nombre de la membresia ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el nombre de la membresia");
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

