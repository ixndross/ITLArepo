using System;
using MemberNova.Admins;

namespace MemberNova.Helpers
{
    public class Benefits
    {
        public static void BenefitsSelection()
        {
            var context = new DataContext();
            List<Beneficio> Beneficios = context.Beneficios.ToList();

            bool BenState = true;

            while (BenState)
            {

                Console.Write("Portal de beneficios. Seleccione una de las siguientes opciones: \n");
                Console.Write("1. Añadir beneficios.\t\t2. Mostrar beneficios actuales.\t\t3. Modificar beneficios\t\t4. Eliminar beneficios\t\t5.Salir.\n\n");

                int BenefitsSelection = Int32.Parse(Console.ReadLine());

                switch (BenefitsSelection)
                {
                    case 1:
                        CrearBeneficio();

                        break;

                    case 2:
                        MostrarBeneficios();

                        break;

                    case 3:
                        ActualizarBeneficios();

                        break;

                    case 4:
                        RemoveBeneficios();

                        break;

                    case 5:
                        BenState = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Por favor, introducir una entrada válida.");
                        break;

                }
            }
        }


        static void PrintBenHeader()
        {
            Console.WriteLine($"\nID\t\tNombre\t\tTipo de membresia\t\tDescripcion\n");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintUsuario(int id)
        {
            var context = new DataContext();
            List<Beneficio> Beneficios = context.Beneficios.ToList();

            var beneficio = Beneficios.FirstOrDefault(p => p.BiD == id);
            Console.WriteLine($"{beneficio.BiD}\t\t{beneficio.Nombre}\t\t{beneficio.TipoMembresia}\t\t{beneficio.Descripcion}\n");

        }


        static void CrearBeneficio()
        {
            var context = new DataContext();
            List<Beneficio> Beneficios = context.Beneficios.ToList();

            Beneficio Beneficio = new Beneficio();

            Console.Write("¿Cual es el nombre de este beneficio?: ");
            Beneficio.Nombre = Console.ReadLine();
            Console.Write("¿En que tipo de membresia se ocupa este beneficio?: ");
            Beneficio.TipoMembresia = Console.ReadLine();
            Console.WriteLine("Descripcion de la membresia: ");
            Beneficio.Descripcion = Console.ReadLine();

            context.Beneficios.Add(Beneficio);

            context.SaveChanges();
        }


        static void MostrarBeneficios()
        {
            var context = new DataContext();
            List<Beneficio> Beneficios = context.Beneficios.ToList();

            PrintBenHeader();
            foreach (var beneficio in Beneficios)
            {
                PrintUsuario(beneficio.BiD);
            }
        }

        static void ActualizarBeneficios()
        {
            var context = new DataContext();
            List<Beneficio> Beneficios = context.Beneficios.ToList();

            MostrarBeneficios();

            Console.WriteLine("\nIntroduzca el numero de identificacion del beneficio a modificar: ");

            var id = Convert.ToInt32(Console.ReadLine());
            var ben = Beneficios.FirstOrDefault(c => c.BiD == id);

            Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del Beneficio, (1.Nombre, 2.Tipo de membresia, 3.Descripcion: ");
            var sel = Int32.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1:

                    Console.WriteLine($"\nEscriba el nuevo nombre para el beneficio {ben.Nombre}.");
                    string name = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el nombre de este beneficio? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        ben.Nombre = name;
                        Console.WriteLine("El nombre del beneficio ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el nombre de {ben.Nombre}");
                    }
                    break;

                case 2:

                    Console.WriteLine($"\nEscriba el nuevo tipo de membresia para el beneficio {ben.Nombre}.");
                    string Type = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el tipo de beneficio donde se aloja el beneficio {ben.Nombre} de \"{ben.TipoMembresia}\" a \"{Type}\"? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        ben.TipoMembresia = Type;
                        Console.WriteLine("El tipo de membresia ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el tipo de membresia de {ben.Nombre}");
                    }
                    break;

                case 3:
                    Console.WriteLine($"\nEscriba la nueva descripcion para {ben.Nombre}.");
                    string newDesc = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar la descripcion de \"{ben.Nombre}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        ben.Descripcion= newDesc;
                        Console.WriteLine("La descripcion ha sido actualizada con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar la descripcion de {ben.Nombre}");
                    }
                    break;

                default:
                    break;
            }
            context.SaveChanges();
        }


        static void RemoveBeneficios()
        {
            var context = new DataContext();
            List<Beneficio> Beneficios = context.Beneficios.ToList();

            Console.WriteLine("Por favor, digite el numero de identificacion del beneficio a eliminar.");

            MostrarBeneficios();

            var id = Convert.ToInt32(Console.ReadLine());
            var ben = Beneficios.FirstOrDefault(c => c.BiD == id);


            Console.WriteLine($"¿Esta seguro que quiere eliminar el beneficio {ben.Nombre}? \nPresione 1 para confirmar, 2 para denegar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                context.Beneficios.Remove(ben);

                Console.WriteLine("El contacto ha sido exitosamente eliminado.");

            }
            else
            {
                Console.WriteLine("El contacto no fue eliminado.");
            }

            context.SaveChanges();
        }


    }
}
