using MemberNova.Admins;
using Spectre.Console;

namespace MemberNova.Helpers
{
    public class RegUserHelpers
    {
        public static void RegularUserSelection()
        {
            Console.WriteLine("Bienvenido al portal de usuarios regulares.\n");
            bool UserState = true;

            while (UserState)
            {
                try
                {
                    Console.Write("Seleccione una de las siguientes opciones: \n");
                    Console.Write("1. Añadir usuarios.\n2. Mostrar usuarios.\n3. Buscar usuarios.\n4. Modificar usuarios\n5. Ver pagos\n6. Salir del menu.\n\n");

                    int UserSelection = Int32.Parse(Console.ReadLine());

                    switch (UserSelection)
                    {

                        case 1:
                            AddRegUser();

                            break;

                        case 2:
                            ShowRegUsers();

                            break;

                        case 3:
                            SearchRegUsers();

                            break;

                        case 4:
                            UpdateRegUsers();

                            break;

                        case 5:

                            VerPagosRegulares();
                            break;

                        case 6:
                            UserState = false;
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

        static Table UserTable()
        {
            var table = new Table();
            table.Border = TableBorder.Square;
            table.ShowRowSeparators = true;
            table.AddColumn("ID");
            table.AddColumn("Nombre");
            table.AddColumn("Telefono");
            table.AddColumn("Email");
            table.AddColumn("Direccion");
            table.AddColumn("Edad");
            table.AddColumn("Tipo de membresia");

            return table;
        }

        static void PrintUsuario(int id, Table table)
        {
            var context = new MemberNova_DataContext();
            var usuario = context.UsuariosRegulares.FirstOrDefault(p => p.ID == id);


            table.AddRow($"{usuario.ID}", $"{usuario.GetFullName()}", $"{usuario.Phone}", $"{usuario.Email}", $"{usuario.BillingAddress}", $"{usuario.Age}", $"{context.Membresias.FirstOrDefault(p => p.MiD == usuario.TipoMembresia).Tipo}");

        }


        static void AddRegUser()
        {
            var context = new MemberNova_DataContext();

            try
            {

                var usuarioRegular = new UsuariosRegulares();
                Console.Write("Nombre: ");
                usuarioRegular.Name = Console.ReadLine();
                Console.Write("Apellido: ");
                usuarioRegular.LastName = Console.ReadLine();
                Console.Write("Numero de telefono: ");
                usuarioRegular.Phone = Console.ReadLine();
                Console.Write("Dirección de correo eléctronico: ");
                usuarioRegular.Email = Console.ReadLine();
                Console.Write("Dirección: ");
                usuarioRegular.BillingAddress = Console.ReadLine();
                Console.Write("Digite la edad de la persona en números: ");
                usuarioRegular.Age = Convert.ToInt32(Console.ReadLine());

                if (String.IsNullOrWhiteSpace(usuarioRegular.Age.ToString()))
                {
                    throw new ArgumentNullException("Edad del usuario", "La edad introducida no es valida.\nFavor introducir nuevamente.");
                }

                Console.WriteLine("Confirme el tipo de membresia del usuario");
                Memberships.ShowMembresias();
                usuarioRegular.TipoMembresia = Convert.ToInt32(Console.ReadLine());

                context.UsuariosRegulares.Add(usuarioRegular);

                context.SaveChanges();

                Console.WriteLine("El usuario ha sido creado exitosamente.");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("ERROR AL CREAR EL USUARIO.\n\nIntroduzca un valor valido.");
            }
            finally
            {
                Console.ReadKey(true);
            }
        }


        public static void ShowRegUsers()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosRegulares> Usuarios = context.UsuariosRegulares.ToList();

            var table = UserTable();
            foreach (var user in Usuarios)
            {
                PrintUsuario(user.ID, table);
            }
            AnsiConsole.Write(table);

        }


        internal static void SearchRegUsers()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosRegulares> Usuarios = context.UsuariosRegulares.ToList();

            var table = UserTable();

            Console.WriteLine("Introduzca el parametro de busqueda: 1. Nombre 2. Apellido 3. Telefono 4. Email 5. Direccion: ");
            var op = Int32.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("Introduzca el nombre del usuario: ");
                    string nameCrit = Console.ReadLine();

                    var nameFound = Usuarios.Where(n => n.Name.ToUpper().Contains(nameCrit.ToUpper())).ToList();

                    foreach (var name in nameFound)
                    {
                        PrintUsuario(name.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 2:
                    Console.WriteLine("Introduzca el apellido del usuario: ");
                    var lastNCrit = Console.ReadLine();

                    var lastNameTBF = Usuarios.Where(l => l.LastName.ToUpper().Contains(lastNCrit.ToUpper())).ToList();

                    foreach (var lname in lastNameTBF)
                    {
                        PrintUsuario(lname.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 3:
                    Console.WriteLine("Introduzca el numero de telefono del usuario: ");
                    var PNCrit = Console.ReadLine();

                    var PNTBF = Usuarios.Where(l => l.Phone.Contains(PNCrit)).ToList();

                    foreach (var PN in PNTBF)
                    {
                        PrintUsuario(PN.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 4:
                    Console.WriteLine("Introduzca la direccion de correo electronico del usuario: ");
                    var emailCrit = Console.ReadLine();

                    var emailTBF = Usuarios.Where(l => l.Email.Contains(emailCrit)).ToList();

                    foreach (var email in emailTBF)
                    {
                        PrintUsuario(email.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 5:
                    Console.WriteLine("Introduzca la direccion fisica del usuario: ");
                    var AddyCrit = Console.ReadLine();

                    var AddTBF = Usuarios.Where(l => l.BillingAddress.ToLower().Contains(AddyCrit.ToLower())).ToList();

                    foreach (var add in AddTBF)
                    {
                        PrintUsuario(add.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;
                default:
                    Console.WriteLine("Favor introducir una entrada valida.");
                    break;
            }

            Console.ReadKey(true);
        }


        static void UpdateRegUsers()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosRegulares> Usuarios = context.UsuariosRegulares.ToList();

            var table = UserTable();


            Console.WriteLine("\nIntroduzca el numero de identificacion del usuario a modificar: ");

            foreach (var usuario in Usuarios)
            {
                PrintUsuario(usuario.ID, table);
            }

            AnsiConsole.Write(table);

            Console.WriteLine("\nIntroduzca el numero de identificacion del usuario a modificar: \n");

            var id = Convert.ToInt32(Console.ReadLine());

            var user = Usuarios.FirstOrDefault(c => c.ID == id);

            if (user is null)
            {

                Console.WriteLine("El usuario introducido no pudo ser encontrado.\nIntente de nuevo mas tarde.\n");
                return;
            }

            Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del usuario, (1. Nombre, 2. Apellido 3. Teléfono...): ");
            var sel = Int32.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1:

                    Console.WriteLine($"\nEscriba el nuevo nombre para {user.Name}.");
                    string newName = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el nombre de \"{user.Name} {user.LastName}\" por \"{newName}\"? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        user.Name = newName;
                        Console.WriteLine("El nombre del usuario ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el nombre de {user.Name}");
                    }
                    break;

                case 2:
                    Console.WriteLine($"\nEscriba el nuevo apellido para {user.Name}.");
                    string newLname = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el apellido de \"{user.Name} {user.LastName}\" por \"{newLname}\"? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        user.LastName = newLname;
                        Console.WriteLine("El apellido del usuario ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el apellido de {user.Name}");
                    }
                    break;

                case 3:
                    Console.WriteLine($"\nEscriba el nuevo no. de telefono para {user.Name}.");
                    string newTelef = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el numero de \"{user.Name} {user.LastName} \" de  {user.Phone} por {newTelef}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        user.Phone = newTelef;
                        Console.WriteLine("El número de telefono del usuario ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el número de telefono de {user.Name}");
                    }
                    break;

                case 4:

                    Console.WriteLine($"\nEscriba la nueva direccion de correo electronico para {user.Name}.");
                    string newEmail = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar la direccion de correo electronico de \"{user.Name} {user.LastName}\" de {user.Email} por {newEmail}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        user.Email = newEmail;
                        Console.WriteLine("La direccion de correo electronico del usuario ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar la direccion de correo electronico de {user.Name}");
                    }
                    break;

                case 5:
                    Console.WriteLine($"\nEscriba la nueva direccion para {user.Name}.");
                    string newAddy = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar la direccion de \"{user.Name} {user.LastName}\" en {user.BillingAddress} por {newAddy}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        user.BillingAddress = newAddy;
                        Console.WriteLine("La direccion del usuario ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar la direccion de {user.Name}");
                    }
                    break;

                case 6:

                    Console.WriteLine($"Lo sentimos. La edad del usuario no puede ser alterada.");
                    break;

                default:
                    Console.WriteLine("Introduzca una entrada valida.");
                    break;

            }

            Console.ReadKey(true);

            context.SaveChanges();
        }

        static void VerPagosRegulares()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosRegulares> Usuarios = context.UsuariosRegulares.ToList();
            var table = new Table();

            table.AddColumns("ID", "Fecha", "Concepto", "Subtotal", "Descuento", "TOTAL");

            Console.WriteLine("Introduzca el numero de identificacion del usuario a verificar: ");

            ShowRegUsers();

            var id = Convert.ToInt32(Console.ReadLine());
            var user = Usuarios.FirstOrDefault(c => c.ID == id);

            if (user is null)
            {

                Console.WriteLine("El usuario introducido no pudo ser encontrado.\nIntente de nuevo mas tarde.\n");
                return;

            }

            var PagosRealizados = context.PagosRegulares.Where(p => p.UserChargedID == user.ID).ToList();

            foreach (var Pago in PagosRealizados)
            {

                table.AddRow($"{Pago.PayID}", $"{Pago.Fecha}", $"{Pago.Concepto}", $"{Pago.Subtotal}", $"{Pago.Descuento}", $"{Pago.GetTotal()}");

            }
            AnsiConsole.Write(table);
        }

    }

}
