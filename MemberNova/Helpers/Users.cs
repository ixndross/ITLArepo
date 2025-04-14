using MemberNova.Admins;

namespace MemberNova.Helpers
{
    public class Users
    {
        public static void UserSelection()
        {
            var context = new DataContext();
            var Usuario = context.Usuarios.ToList();

            bool UserState = true;

            while (UserState)
            {

                Console.Write("Portal de usuarios. Seleccione una de las siguientes opciones: \n");
                Console.Write("1. Añadir usuarios.\t\t2. Mostrar usuarios.\t\t3. Buscar usuarios.\t\t4. Modificar usuarios\t\t5. Eliminar usuarios\t\t6.Salir.\n\n");

                int UserSelection = Int32.Parse(Console.ReadLine());

                switch (UserSelection)
                {
                    case 1:
                        AddUser();

                        break;

                    case 2:
                        ShowUsers();

                        break;

                    case 3:
                        SearchUsers();

                        break;

                    case 4:
                        UpdateUsers();

                        break;

                    case 5:
                        RemoveUsers();

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
        }


        static void PrintUserHeader()
        {
            Console.WriteLine($"\nID\t\tNombre\t\tTelefono\t\tEmail\t\tDireccion\t\tEdad ");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintUsuario(int id)
        {
            var context = new DataContext();
            var Usuario = context.Usuarios.ToList();

            var usuario = Usuario.FirstOrDefault(p => p.ID == id);
            Console.WriteLine($"{usuario.ID}\t\t{usuario.GetFullName()}\t\t{usuario.Phone}\t\t{usuario.Email}\t\t{usuario.BillingAddress}\t\t{usuario.Age}\n");

        }


        static void AddUser()
        {
            var context = new DataContext();

            var usuario = new Usuario();

            Console.Write("Nombre: ");
            usuario.Name = Console.ReadLine();
            Console.Write("Apellido: ");
            usuario.LastName = Console.ReadLine();
            Console.Write("Numero de telefono: ");
            usuario.Phone = Console.ReadLine();
            Console.Write("Dirección de correo eléctronico: ");
            usuario.Email = Console.ReadLine();
            Console.Write("Dirección: ");
            usuario.BillingAddress = Console.ReadLine();
            Console.Write("Digite la edad de la persona en números: ");
            usuario.Age = Convert.ToInt32(Console.ReadLine());

            context.Usuarios.Add(usuario);

            context.SaveChanges();

            Console.Clear();
        }


        static void ShowUsers()
        {
            var context = new DataContext();
            List<Usuario> Usuarios = context.Usuarios.ToList();
            PrintUserHeader();
            foreach (var user in Usuarios)
            {
                PrintUsuario(user.ID);
            }
        }


        static void SearchUsers()
        {
            var context = new DataContext();
            List<Usuario> Usuarios = context.Usuarios.ToList();

            Console.WriteLine("Introduzca el parametro de busqueda: 1. ID 2. Nombre 3. Apellido: ");
            var op = Int32.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("Introduzca el ID del usuario: ");
                    int SelectedId = Convert.ToInt32(Console.ReadLine());
                    var usuario = Usuarios.FirstOrDefault(p => p.ID == SelectedId);

                    PrintUserHeader();
                    PrintUsuario(SelectedId);

                    break;

                case 2:
                    Console.WriteLine("Introduzca el nombre del usuario: ");
                    string nameCrit = Console.ReadLine();

                    var nameFound = Usuarios.Where(n => n.Name.ToUpper().Contains(nameCrit.ToUpper())).ToList();

                    PrintUserHeader();
                    foreach (var name in nameFound)
                    {
                        PrintUsuario(name.ID);
                    }
                    break;

                case 3:
                    Console.WriteLine("Introduzca el apellido del usuario: ");
                    var lastNCrit = Console.ReadLine();

                    var lastNameTBF = Usuarios.Where(l => l.LastName.ToUpper().Contains(lastNCrit.ToUpper())).ToList();
                    PrintUserHeader();

                    foreach (var lname in lastNameTBF)
                    {
                        PrintUsuario(lname.ID);
                    }
                    break;

                default:
                    Console.WriteLine("Favor introducir una entrada valida");
                    break;
            }


        }


        static void UpdateUsers()
        {
            var context = new DataContext();
            List<Usuario> Usuarios = context.Usuarios.ToList();


            Console.WriteLine("\nIntroduzca el numero de identificacion del usuario a modificar: ");

            PrintUserHeader();
            foreach (var usuario in Usuarios)
            {
                PrintUsuario(usuario.ID);
            }

            var id = Convert.ToInt32(Console.ReadLine());
            var user = Usuarios.FirstOrDefault(c => c.ID == id);

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
                    break;
            }

            context.SaveChanges();
        }


        static void RemoveUsers()
        {

            var context = new DataContext();
            List<Usuario> Usuarios = context.Usuarios.ToList();

            Console.WriteLine("Por favor, digite el numero de identificacion del usuario a eliminar.");

            ShowUsers();

            var id = Convert.ToInt32(Console.ReadLine());
            var user = Usuarios.FirstOrDefault(c => c.ID == id);


            Console.WriteLine($"¿Esta seguro que quiere eliminar el usuario {user.Name} {user.LastName}? \nPresione 1 para confirmar, 2 para denegar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                context.Usuarios.Remove(user);

                Console.WriteLine("El usuario ha sido exitosamente eliminado.");

                context.SaveChanges();

            }
            else
            {
                Console.WriteLine("El usuario no fue eliminado.");
            }
        }


    }
}
