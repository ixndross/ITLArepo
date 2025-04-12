using System.Data.SqlTypes;
using System.Reflection.Metadata;
using MemberNova.Admins;

namespace MemberNova.Helpers
{
    public class Memberships
    {

        static List<Membership> Membresias = new List<Membership>();


        public static void MembershipSelection()
        {
            bool MemberState = true;

            while (MemberState)
            {

                Console.Write("Portal de membresias. Seleccione una de las siguientes opciones: \n");
                Console.WriteLine("1. Añadir Membresias.\t\t2. Mostrar membresias.\t\t3. Modificar detalles de membresia\t\t4. Eliminar membresia\t\t6. Salir.\n");

                int MemberSelection = Convert.ToInt32(Console.ReadLine());

                switch (MemberSelection)
                {
                    case 1:
                        NuevaMembresia(Membresias);

                        break;

                    case 2:
                        ShowMembresias(Membresias);

                        break;

                    case 3:
                        BuscarMembrias(Membresias);

                        break;

                    case 4:
                        ModificarMembresia(Membresias);

                        break;

                    case 5:
                        RemoverMembresias(Membresias);

                        break;

                    case 6:
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
            Console.WriteLine($"\nMember ID\t\tTipo\t\tDescripcion\t\tPrecio\t\tEstado de Excusividad\n");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintMembership(List<Membership> Membresias, int id)
        {
            var Membresia = Membresias.FirstOrDefault(p => p.MiD == id);
            Console.WriteLine($"{Membresia.MiD}\t\t{Membresia.Tipo}\t\t{Membresia.Total}\t\t{Membresia.IsExclusive}\n");

        }


        static void NuevaMembresia(List<Membership> Membresias)
        {
            var id = 1000 + (Membresias.Count * 10);
            var membresia = new Membership();
            

            membresia.MiD = id;
            Console.Write("Escriba el nombre de la nueva membresia: ");
            membresia.Tipo = Console.ReadLine();
            Console.WriteLine("Introduzca la descripcion de la nueva membresia.");
            membresia.Descripcion = Console.ReadLine();
            Console.WriteLine("Introduzca el costo total de la nueva membresia.");
            membresia.Total = SqlMoney.Parse(Console.ReadLine());
            membresia.IsExclusive = "N/A";
            Console.WriteLine("¿Es esta membresia exclusiva? Presione 1 para confirmar, o cualquier otro boton para rechazar.");

            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("Introduzca los parametros de la exclusividad de este tipo de membresia.");
                membresia.IsExclusive = Console.ReadLine();
            }

            Membresias.Add(membresia);
        }


        static void ShowMembresias(List<Membership> Membresias)
        {
            PrintMembershipheader();
            foreach (var mbship in Membresias)
            {
                PrintMembership(Membresias, mbship.MiD);
            }
        }


        static void BuscarMembrias(List<Membership> Membresias)
        {

            Console.WriteLine("Introduzca el parametro de busqueda: 1. ID 2. Nombre de la membresia: ");
            var op = Int32.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("Introduzca el numero de identificacion de la membresia: ");
                    int SelectedId = Convert.ToInt32(Console.ReadLine());
                    var membership = Membresias.FirstOrDefault(p => p.MiD == SelectedId);

                    PrintMembershipheader();
                    PrintMembership(Membresias, SelectedId);

                    break;

                case 2:
                    Console.WriteLine("Introduzca el tipo de la membresia: ");
                    string nameCrit = Console.ReadLine();

                    var nameFound = Membresias.Where(n => n.Tipo.ToUpper().Contains(nameCrit.ToUpper())).ToList();

                    PrintMembershipheader();
                    foreach (var name in nameFound)
                    {
                        PrintMembership(nameFound, name.MiD);
                    }
                    break;

                default:
                    Console.WriteLine("Favor introducir una entrada valida");
                    break;
            }


        }

        static void ModificarMembresia(List<Membership> Membresias)
        {

            PrintMembershipheader();
            Console.WriteLine("\nIntroduzca el numero de identificacion de la membresia a modificar: ");

            foreach (var membership in Membresias)
            {
                PrintMembership(Membresias, membership.MiD);
            }

            var id = Convert.ToInt32(Console.ReadLine());
            var mbship = Membresias.FirstOrDefault(c => c.MiD == id);

            Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del tipo de membresia, (1. Nombre de la membresia, 2. Descripcion 3. Precio...): ");
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
                    SqlMoney newPrice = SqlMoney.Parse(Console.ReadLine());

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
        }


        static void RemoverMembresias(List<Membership> Membresias)
        {
            
            ShowMembresias(Membresias);

            Console.WriteLine("\nPor favor, digite el numero de identificacion de la membresia a eliminar.");

            var id = Convert.ToInt32(Console.ReadLine());
            var mbship = Membresias.FirstOrDefault(c => c.MiD == id);


            Console.WriteLine($"¿Esta seguro que quiere eliminar esta membresia? \nPresione 1 para confirmar, 2 para denegar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                Membresias.Remove(mbship);

                Console.WriteLine("La membresia ha sido exitosamente eliminada.");
            }
            else
            {
                Console.WriteLine("La membresia no fue eliminada.");
            }
        }


    }
}

