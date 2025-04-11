using MemberNova.Admins;

namespace MemberNova.Helpers
{
    public class UserHelper
    {

        static List<Usuario> Usuarios = new List<Usuario>();


        public static void UserSelection()
        {
            bool UserState = true;

            while (UserState) {

                Console.Write("Portal de usuarios. Seleccione una de las siguientes opciones: \n");
                Console.Write("1. Añadir usuarios.\t\t2. Mostrar usuarios.\t\t3. Buscar usuarios.\t\t4. Modificar usuarios\t\t5. Eliminar usuarios\t\t6.Salir.\n\n");

                int UserSelection = Int32.Parse(Console.ReadLine());

                switch (UserSelection)
                {
                    case 1:
                        AddUser(Usuarios);

                        break;

                    case 2:
                        ShowUsers(Usuarios);

                        break;

                    case 3:
                        SearchUsers(Usuarios);

                        break;

                    case 4:
                        UpdateUsers(Usuarios);

                        break;

                    case 5:
                        RemoveUsers(Usuarios);

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
            Console.WriteLine($"\nID\t\tNombre\t\tApellido\t\tTelefono\t\tEmail\t\tDireccion\t\tEdad ");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }


        static void PrintUsuario(List<Usuario> Usuario, int id)
        {
            var usuario = Usuario.FirstOrDefault(p => p.ID == id);
            Console.WriteLine($"{usuario.ID}		{usuario.Name.ToUpper()}      {usuario.LastName.ToUpper()}      {usuario.Phone}         {usuario.Email}     {usuario.BillingAddress}       {usuario.Age}\n");

        }


        static void AddUser(List<Usuario> Usuarios)
        {
            var id = Usuarios.Count + 1;
            var usuario = new Usuario();

            usuario.ID = id;
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

            Usuarios.Add(usuario);
        }


        static void ShowUsers(List<Usuario> Usuarios)
        {
            PrintUserHeader();
            foreach (var user in Usuarios)
            {
                PrintUsuario(Usuarios, user.ID);
            }
        }


        static void SearchUsers(List<Usuario> Usuarios)
        {

            Console.WriteLine("Introduzca el parametro de busqueda: 1. ID 2. Nombre 3. Apellido: ");
            var op = Int32.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("Introduzca el ID del usuario: ");
                    int SelectedId = Convert.ToInt32(Console.ReadLine());
                    var usuario = Usuarios.FirstOrDefault(p => p.ID == SelectedId);

                    PrintUserHeader();
                    PrintUsuario(Usuarios, SelectedId);

                    break;

                case 2:
                    Console.WriteLine("Introduzca el nombre del usuario: ");
                    string nameCrit = Console.ReadLine();

                    var nameFound = Usuarios.Where(n => n.Name.ToUpper().Contains(nameCrit.ToUpper())).ToList();

                    PrintUserHeader();
                    foreach (var name in nameFound)
                    {
                        PrintUsuario(nameFound, name.ID);
                    }
                    break;

                case 3:
                    Console.WriteLine("Introduzca el apellido del usuario: ");
                    var lastNCrit = Console.ReadLine();

                    var lastNameTBF = Usuarios.Where(l => l.LastName.ToUpper().Contains(lastNCrit.ToUpper())).ToList();
                    PrintUserHeader();

                    foreach (var lname in lastNameTBF)
                    {
                        PrintUsuario(lastNameTBF, lname.ID);
                    }
                    break;

                default:
                    Console.WriteLine("Favor introducir una entrada valida");
                    break;
            }


        }


        static void UpdateUsers(List<Usuario> Usuarios)
        {


            Console.WriteLine("\nIntroduzca el numero de identificacion del usuario a modificar: ");

            PrintUserHeader();
            foreach (var usuario in Usuarios)
            {
                PrintUsuario(Usuarios, usuario.ID);
            }

            var id = Convert.ToInt32(Console.ReadLine());
            var user = Usuarios.FirstOrDefault(c => c.ID == id);

            Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del usuario, (1. Nombre, 2. Apellido 3. Direccion...): ");
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
        }


        static void RemoveUsers(List<Usuario> Usuarios)
        {
            Console.WriteLine("Por favor, digite el numero de identificacion del contacto a eliminar.");

            ShowUsers(Usuarios);

            var id = Convert.ToInt32(Console.ReadLine());
            var user = Usuarios.FirstOrDefault(c => c.ID == id);


            Console.WriteLine($"¿Esta seguro que quiere eliminar el contacto {user.Name} {user.LastName}? \nPresione 1 para confirmar, 2 para denegar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                Usuarios.Remove(user);

                Console.WriteLine("El contacto ha sido exitosamente eliminado.");

            }
            else
            {
                Console.WriteLine("El contacto no fue eliminado.");
            }
        }


    }
}
