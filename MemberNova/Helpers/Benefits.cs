using MemberNova.Admins;
using Spectre.Console;

namespace MemberNova.Helpers
{
    public class Benefits
    {
        public static void BenefitsSelection()
        {
            Console.WriteLine("\nPortal de beneficios\n");
            bool BenState = true;

            while (BenState)
            {
                try
                {
                    Console.Write("Seleccione una de las siguientes opciones: \n");
                    Console.Write("1. Añadir beneficios.\n2. Mostrar beneficios actuales.\n3. Modificar beneficios\n4. Eliminar beneficios\n5.Salir.\n\n");

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

            static Table BenefitsTable()
            {
                var table = new Table();
                table.Border = TableBorder.Square;
                table.ShowRowSeparators = true;
                table.AddColumn("ID");
                table.AddColumn("Nombre");
                table.AddColumn("Descripcion");
                table.AddColumn("Tipo de membresia");

                return table;
            }
            static void PrintBeneficio(int id, Table table)
            {

                var context = new DataContext();
                List<Beneficio> Beneficios = context.Beneficios.ToList();

                var beneficio = Beneficios.FirstOrDefault(p => p.BiD == id);

                table.AddRow($"{beneficio.BiD}", $"{beneficio.Nombre}", $"{beneficio.Descripcion}", $"{context.Membresias.FirstOrDefault(p => p.MiD == beneficio.TipoMembresia).Tipo}");
            }


            static void CrearBeneficio()
            {
                try
                {
                    var context = new DataContext();
                    List<Beneficio> Beneficios = context.Beneficios.ToList();

                    Beneficio Beneficio = new Beneficio();

                    Console.Write("¿Cual es el nombre de este beneficio?: ");
                    Beneficio.Nombre = Console.ReadLine();

                    Console.WriteLine("Descripcion del beneficio: ");
                    Beneficio.Descripcion = Console.ReadLine();

                    Console.Write("¿En que tipo de membresia se ocupa este beneficio?: ");
                    Memberships.ShowMembresias();
                    Beneficio.TipoMembresia = Convert.ToInt32(Console.ReadLine());

                    context.Beneficios.Add(Beneficio);

                    context.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    Console.WriteLine("ERROR AL CREAR EL BENEFICIO.\n\nIntroduzca una entrada valida.");
                    Console.ReadKey();
                }
                finally
                {
                    Console.Clear();
                }
            }


            static void MostrarBeneficios()
            {
                var context = new DataContext();
                List<Beneficio> Beneficios = context.Beneficios.ToList();

                var table = BenefitsTable();

                foreach (var beneficio in Beneficios)
                {
                    PrintBeneficio(beneficio.BiD, table);
                }
                AnsiConsole.Write(table);
            }

            static void ActualizarBeneficios()
            {
                var context = new DataContext();
                List<Beneficio> Beneficios = context.Beneficios.ToList();

                MostrarBeneficios();

                Console.WriteLine("\nIntroduzca el numero de identificacion del beneficio a modificar: ");

                var id = Convert.ToInt32(Console.ReadLine());
                var ben = Beneficios.FirstOrDefault(c => c.BiD == id);

                if (ben is null)
                {
                    Console.WriteLine("No se pudo encontrar el beneficio seleccionado\nIntente de nuevo mas tarde.");
                    return;
                }

                Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del Beneficio, (1.Nombre, 2.Descripcion, 3.Tipo de membresia: ");
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
                        Console.WriteLine($"\nEscriba la nueva descripcion para {ben.Nombre}.");
                        string newDesc = Console.ReadLine();

                        Console.WriteLine($"¿Estas seguro de que quieres cambiar la descripcion de \"{ben.Nombre}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            ben.Descripcion = newDesc;
                            Console.WriteLine("La descripcion ha sido actualizada con exito.");
                        }
                        else
                        {
                            Console.WriteLine($"No se ha podido cambiar la descripcion de {ben.Nombre}");
                        }

                        break;

                    case 3:


                        Console.WriteLine($"\nEscriba el nuevo tipo de membresia para el beneficio {ben.Nombre}.");
                        Memberships.ShowMembresias();
                        int Type = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine($"¿Estas seguro de que quieres cambiar el tipo de beneficio donde se aloja el beneficio {ben.Nombre} a \"{context.Membresias.FirstOrDefault(p => p.MiD == Type).Tipo}\"? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
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

                    default:
                        Console.WriteLine("Introduzca una entrada valida.");
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

                if (ben is null)
                {
                    Console.WriteLine("No se pudo encontrar el beneficio seleccionado\nIntente de nuevo mas tarde.");
                    return;
                }

                Console.WriteLine($"¿Esta seguro que quiere eliminar el beneficio \"{ben.Nombre}\"? \nPresione 1 para confirmar, 2 para denegar.");
                if (Int32.Parse(Console.ReadLine()) == 1)
                {
                    context.Beneficios.Remove(ben);

                    Console.WriteLine("El beneficio ha sido exitosamente eliminado.");

                }
                else
                {
                    Console.WriteLine("El beneficio no fue eliminado.");
                }

                context.SaveChanges();
            }


        }
    }
}
