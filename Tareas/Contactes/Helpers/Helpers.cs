namespace ContactosOOP
{

    public class HeaderHelpers
    {

        public static void Selection()
        {
            Console.Write("1. Agregar Contacto      ");
            Console.Write("2. Ver Contactos     ");
            Console.Write("3. Buscar Contactos      ");
            Console.Write("4. Modificar Contacto        ");
            Console.Write("5. Eliminar Contacto     ");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");
        }


        public static void AddContact(List<Contact> contacto)
        {
            var id = contacto.Count + 1;
            var contact = new Contact();

            contact.ID = id;
            Console.WriteLine("Digite el nombre de la persona:");
            contact.Name = Console.ReadLine();
            Console.WriteLine("Digite el apellido de la persona:");
            contact.LastName = Console.ReadLine();
            Console.WriteLine("Digite la dirección de la persona:");
            contact.Address = Console.ReadLine();
            Console.WriteLine("Digite el numero de telefono de la persona: ");
            contact.Email = Console.ReadLine();
            Console.WriteLine("Digite la direccion de correo electronico de la persona:");
            contact.Phone = Console.ReadLine();
            Console.WriteLine("Digite la edad de la persona en números: ");
            contact.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");
            contact.IsFavorite = Convert.ToInt32(Console.ReadLine()) == 1;

            contacto.Add(contact);


        }

        public static void ShowContacts(List<Contact> contacto)
        {
            PrintContactHeader();
            foreach (var cont in contacto)
            {
                PrintContact(contacto, cont.ID);
            }
        }

        public static void SearchContacts(List<Contact> contacto)
        {

            Console.WriteLine("Introduzca el parametro de busqueda: 1. ID 2. Nombre 3. Apellido: ");
            var op = Int32.Parse(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Console.WriteLine("Introduzca el ID del contacto: ");
                    int SelectedId = Convert.ToInt32(Console.ReadLine());
                    var contact = contacto.FirstOrDefault(p => p.ID == SelectedId);

                    PrintContactHeader();
                    PrintContact(contacto, SelectedId);

                    break;

                case 2:
                    Console.WriteLine("Introduzca el nombre del contacto: ");
                    string nameCrit = Console.ReadLine();

                    var nameFound = contacto.Where(n => n.Name.ToUpper().Contains(nameCrit.ToUpper())).ToList();

                    PrintContactHeader();
                    foreach (var name in nameFound)
                    {
                        PrintContact(nameFound, name.ID);
                    }
                    break;

                case 3:
                    Console.WriteLine("Introduzca el apellido del contacto: ");
                    var lastNCrit = Console.ReadLine();

                    var lastNameTBF = contacto.Where(l => l.LastName.ToUpper().Contains(lastNCrit.ToUpper())).ToList();
                    PrintContactHeader();

                    foreach (var lname in lastNameTBF)
                    {
                        PrintContact(lastNameTBF, lname.ID);
                    }
                    break;

                default:
                    Console.WriteLine("Favor introducir una entrada valida");
                    break;
            }


        }

        public static void ModifyContacts(List<Contact> contacto)
        {


            Console.WriteLine("\nIntroduzca el numero de identificacion del contacto a modificar: ");

            PrintContactHeader();
            foreach (var cont in contacto)
            {
                PrintContact(contacto, cont.ID);
            }

            var id = Convert.ToInt32(Console.ReadLine());
            var contact = contacto.FirstOrDefault(c => c.ID == id);

            Console.WriteLine("\nSeleccione que parametro desea modificar en el orden numerico de los datos del contacto, (1. Nombre, 2. Apellido 3. Direccion...): ");
            var sel = Int32.Parse(Console.ReadLine());

            switch (sel)
            {
                case 1:

                    Console.WriteLine($"\nEscriba el nuevo nombre para {contact.Name}.");
                    string newName = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el nombre de \"{contact.Name} {contact.LastName}\" por \"{newName}\"? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        contact.Name = newName;
                        Console.WriteLine("El nombre del contacto ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el nombre de {contact.Name}");
                    }
                    break;

                case 2:
                    Console.WriteLine($"\nEscriba el nuevo apellido para {contact.Name}.");
                    string newLname = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el apellido de \"{contact.Name} {contact.LastName}\" por \"{newLname}\"? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        contact.LastName = newLname;
                        Console.WriteLine("El apellido del contacto ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el apellido de {contact.Name}");
                    }
                    break;

                case 3:
                    Console.WriteLine($"\nEscriba la nueva direccion para {contact.Name}.");
                    string newAddy = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar la direccion de \"{contact.Name} {contact.LastName}\" en {contact.Address} por {newAddy}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        contact.Address = newAddy;
                        Console.WriteLine("La direccion del contacto ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar la direccion de {contact.Name}");
                    }
                    break;

                case 4:
                    Console.WriteLine($"\nEscriba el nuevo no. de telefono para {contact.Name}.");
                    string newTelef = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar el numero de \"{contact.Name} {contact.LastName} \" de  {contact.Address} por {newTelef}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        contact.Phone = newTelef;
                        Console.WriteLine("El número de telefono del contacto ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar el número de telefono de {contact.Name}");
                    }
                    break;

                case 5:

                    Console.WriteLine($"\nEscriba la nueva direccion de correo electronico para {contact.Name}.");
                    string newEmail = Console.ReadLine();

                    Console.WriteLine($"¿Estas seguro de que quieres cambiar la direccion de correo electronico de \"{contact.Name} {contact.LastName}\" de {contact.Email} por {newEmail}? \nPresione 1 para confirmar el cambio, y 2 para descartarlo.");
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        contact.Email = newEmail;
                        Console.WriteLine("La direccion de correo electronico del contacto ha sido actualizado con exito.");
                    }
                    else
                    {
                        Console.WriteLine($"No se ha podido cambiar la direccion de correo electronico de {contact.Name}");
                    }
                    break;

                case 6:

                    Console.WriteLine($"No se puede cambiar la edad del contacto {contact.Name}.");
                    break;

                case 7:

                    if (contact.IsFavorite)
                    {
                        Console.WriteLine($"¿Desea remover el estado de \'mejor amigo\' a {contact.Name}? :( \nConfirme con 1. Rechace con 2.");
                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            contact.IsFavorite = false;
                            Console.WriteLine($"{contact.Name} ha sido removido de la lista de mejores amigos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"¿Desea promover a {contact.Name} en tu lista de mejores amigos? \nConfirme con 1. Rechace con 2.");
                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            contact.IsFavorite = true;
                            Console.WriteLine($"{contact.Name} ha sido promovido a \'mejor amigo\'.");
                        }
                    }
                    break;
            }

        }

        public static void DeleteContacts(List<Contact> contacto)
        {
            Console.WriteLine("Por favor, digite el numero de identificacion del contacto a eliminar.");

            ShowContacts(contacto);

            var id = Convert.ToInt32(Console.ReadLine());
            var contact = contacto.FirstOrDefault(c => c.ID == id);


            Console.WriteLine($"¿Esta seguro que quiere eliminar el contacto {contact.Name} {contact.LastName}? \nPresione 1 para confirmar, 2 para denegar.");
            if (Int32.Parse(Console.ReadLine()) == 1)
            {
                contacto.Remove(contact);

                Console.WriteLine("El contacto ha sido exitosamente eliminado.");

            }
            else
            {
                Console.WriteLine("El contacto no fue eliminado.");
            }
        }

        static void PrintContactHeader()
        {
            Console.WriteLine($"\nID		Nombre          Apellido            Dirección           Telefono            Email           Edad            Es Mejor Amigo?");
            Console.WriteLine($"___________________________________________________________________________________________________________________________________\n");

        }

        static void PrintContact(List<Contact> contacto, int id)
        {
            var contact = contacto.FirstOrDefault(p => p.ID == id);
            string isBestFriendStr = (contact.IsFavorite) ? "Si" : "No";
            Console.WriteLine($"{contact.ID}		{contact.Name.ToUpper()}      {contact.LastName.ToUpper()}      {contact.Phone}         {contact.Email}     {contact.Address}       {contact.Age}       {contact.IsFavorite}\n");


        }
    }
}

