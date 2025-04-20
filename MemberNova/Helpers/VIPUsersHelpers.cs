using MemberNova.Admins;
using Spectre.Console;

namespace MemberNova.Helpers
{
    public class VIPUsersHelpers
    {
        public static void VIPUserSelection()
        {
            Console.WriteLine("Bienvenido al portal de usuarios VIP.\n");
            bool UserState = true;

            while (UserState)
            {
                try
                {
                    Console.Write("Seleccione una de las siguientes opciones: \n");
                    Console.Write("1. Añadir usuarios VIP.\n2. Mostrar usuarios VIP.\n3. Buscar usuarios VIP.\n4. Modificar usuarios VIP\n5. Ver pagos de usuarios VIP.\n6. Salir del menu.\n\n");

                    int UserSelection = Int32.Parse(Console.ReadLine());

                    switch (UserSelection)
                    {

                        case 1:
                            AddVIPUser();

                            break;

                        case 2:
                            ShowVIPUsers();

                            break;

                        case 3:
                            SearchVIPUsers();

                            break;

                        case 4:
                            UpdateVIPUsers();

                            break;

                        case 5:

                            VerPagosVIP();
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

        static void PrintUsuarioVIP(int id, Table table)
        {
            var context = new MemberNova_DataContext();
            var usuario = context.UsuariosVIP.FirstOrDefault(p => p.ID == id);


            table.AddRow($"{usuario.ID}", $"{usuario.GetFullName()}", $"{usuario.Phone}", $"{usuario.Email}", $"{usuario.BillingAddress}", $"{usuario.Age}", $"{context.Membresias.FirstOrDefault(p => p.MiD == usuario.TipoMembresia).Tipo}");

        }


        static void AddVIPUser()
        {
            var context = new MemberNova_DataContext();

            try
            {
                //List<UsuariosVIP> Usuarios = context.UsuariosVIP.ToList();

                var usuarioVIP = new UsuariosVIP();
                Console.Write("Nombre: ");
                usuarioVIP.Name = Console.ReadLine();
                Console.Write("Apellido: ");
                usuarioVIP.LastName = Console.ReadLine();
                Console.Write("Numero de telefono: ");
                usuarioVIP.Phone = Console.ReadLine();
                Console.Write("Dirección de correo eléctronico: ");
                usuarioVIP.Email = Console.ReadLine();
                Console.Write("Dirección: ");
                usuarioVIP.BillingAddress = Console.ReadLine();
                Console.Write("Digite la edad de la persona en números: ");
                usuarioVIP.Age = Convert.ToInt32(Console.ReadLine());

                if (String.IsNullOrWhiteSpace(usuarioVIP.Age.ToString()))
                {
                    Console.WriteLine("La edad introducida no es valida." +
                        "Favor intentar de nuevo.");
                    return;
                }

                Console.WriteLine("Confirme el tipo de membresia del usuario VIP.");
                Memberships.ShowMembresias();
                usuarioVIP.TipoMembresia = Convert.ToInt32(Console.ReadLine());

                context.UsuariosVIP.Add(usuarioVIP);

                context.SaveChanges();

                Console.WriteLine("El usuario VIP ha sido creado exitosamente.");
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("ERROR AL CREAR EL USUARIO VIP.\n\nIntroduzca un valor valido.");
            }
            finally
            {
                Console.ReadKey(true);
            }
        }


        public static void ShowVIPUsers()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosVIP> Usuarios = context.UsuariosVIP.ToList();

            var table = UserTable();
            foreach (var user in Usuarios)
            {
                PrintUsuarioVIP(user.ID, table);
            }
            AnsiConsole.Write(table);

        }


        internal static void SearchVIPUsers()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosVIP> Usuarios = context.UsuariosVIP.ToList();

            var table = UserTable();

            Console.WriteLine("Introduzca el parametro de busqueda: 1. Nombre 2. Apellido 3. Telefono 4. Email 5. Direccion: ");
            var op = Int32.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("Introduzca el nombre del usuario VIP: ");
                    string nameCrit = Console.ReadLine();

                    var nameFound = Usuarios.Where(n => n.Name.ToUpper().Contains(nameCrit.ToUpper())).ToList();

                    foreach (var name in nameFound)
                    {
                        PrintUsuarioVIP(name.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 2:
                    Console.WriteLine("Introduzca el apellido del usuario VIP: ");
                    var lastNCrit = Console.ReadLine();

                    var lastNameTBF = Usuarios.Where(l => l.LastName.ToUpper().Contains(lastNCrit.ToUpper())).ToList();

                    foreach (var lname in lastNameTBF)
                    {
                        PrintUsuarioVIP(lname.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 3:
                    Console.WriteLine("Introduzca el numero de telefono del usuario VIP: ");
                    var PNCrit = Console.ReadLine();

                    var PNTBF = Usuarios.Where(l => l.Phone.Contains(PNCrit)).ToList();

                    foreach (var PN in PNTBF)
                    {
                        PrintUsuarioVIP(PN.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 4:
                    Console.WriteLine("Introduzca la direccion de correo electronico del usuario VIP: ");
                    var emailCrit = Console.ReadLine();

                    var emailTBF = Usuarios.Where(l => l.Email.Contains(emailCrit)).ToList();

                    foreach (var email in emailTBF)
                    {
                        PrintUsuarioVIP(email.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;

                case 5:
                    Console.WriteLine("Introduzca la direccion fisica del usuario VIP: ");
                    var AddyCrit = Console.ReadLine();

                    var AddTBF = Usuarios.Where(l => l.BillingAddress.ToLower().Contains(AddyCrit.ToLower())).ToList();

                    foreach (var add in AddTBF)
                    {
                        PrintUsuarioVIP(add.ID, table);
                    }
                    AnsiConsole.Write(table);

                    break;
                default:
                    Console.WriteLine("Favor introducir una entrada valida.");
                    break;
            }

            Console.ReadKey(true);
        }


        static void UpdateVIPUsers()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosVIP> Usuarios = context.UsuariosVIP.ToList();

            var table = UserTable();


            Console.WriteLine("\nIntroduzca el numero de identificacion del usuario VIP a modificar: ");

            foreach (var usuario in Usuarios)
            {
                PrintUsuarioVIP(usuario.ID, table);
            }

            AnsiConsole.Write(table);

            Console.WriteLine("\nIntroduzca el numero de identificacion del usuario VIP a modificar: \n");

            var id = Convert.ToInt32(Console.ReadLine());

            var user = Usuarios.FirstOrDefault(c => c.ID == id);

            if (user is null)
            {

                Console.WriteLine("El usuarioVIP introducido no pudo ser encontrado.\nIntente de nuevo mas tarde.\n");
                return;
            }

            Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del usuario VIP, (1. Nombre, 2. Apellido 3. Teléfono...): ");
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
                        Console.WriteLine("El nombre del usuario VIP ha sido actualizado con exito.");
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
                        Console.WriteLine("El apellido del usuario VIP ha sido actualizado con exito.");
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
                        Console.WriteLine("El número de telefono del usuario VIP ha sido actualizado con exito.");
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
                        Console.WriteLine("La direccion de correo electronico del usuario VIP ha sido actualizado con exito.");
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
                        Console.WriteLine("La direccion del usuario VIP ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar la direccion de {user.Name}");
                    }
                    break;

                case 6:

                    Console.WriteLine($"Lo sentimos. La edad del usuario VIP no puede ser alterada.");
                    break;

                default:
                    Console.WriteLine("Introduzca una entrada valida.");
                    break;

            }

            Console.ReadKey(true);

            context.SaveChanges();
        }

        static void VerPagosVIP()
        {
            var context = new MemberNova_DataContext();
            List<UsuariosVIP> Usuarios = context.UsuariosVIP.ToList();
            var table = new Table();

            table.AddColumns("ID", "Fecha", "Concepto", "Subtotal", "Descuento", "TOTAL");

            Console.WriteLine("Introduzca el numero de identificacion del usuario VIP a verificar: ");

            ShowVIPUsers();

            var id = Convert.ToInt32(Console.ReadLine());
            var user = Usuarios.FirstOrDefault(c => c.ID == id);

            if (user is null)
            {

                Console.WriteLine("El usuario VIP introducido no pudo ser encontrado.\nIntente de nuevo mas tarde.\n");
                return;

            }

            var PagosRealizados = context.PagosVIP.Where(p => p.VIPUserChargedID == user.ID).ToList();

            foreach (var Pago in PagosRealizados)
            {

                table.AddRow($"{Pago.PayID}", $"{Pago.Fecha}", $"{Pago.Concepto}", $"{Pago.Subtotal}", $"{Pago.Descuento}", $"{Pago.GetTotal()}");

            }
            AnsiConsole.Write(table);
        }

    }

}
