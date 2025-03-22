Console.WriteLine("Bienvenido a mi lista de Contactes");


//names, lastnames, addresses, telephones, emails, ages, bestfriend
bool runing = true;
List<int> ids = new List<int>();
Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> lastnames = new Dictionary<int, string>();
Dictionary<int, string> addresses = new Dictionary<int, string>();
Dictionary<int, string> telephones = new Dictionary<int, string>();
Dictionary<int, string> emails = new Dictionary<int, string>();
Dictionary<int, int> ages = new Dictionary<int, int>();
Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();


while (runing)
{
    try
    {
        Console.WriteLine("1. Agregar Contacto     2. Ver Contactos    3. Buscar Contactos     4. Modificar Contacto   5. Eliminar Contacto    6. Salir");
        Console.WriteLine("Digite el número de la opción deseada");

        int typeOption = Convert.ToInt32(Console.ReadLine());

        switch (typeOption)
        {
            case 1:
                {
                    AddContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);

                }
                break;
            case 2:
                {
                    ShowContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                }
                break;
            case 3:
                {
                    SearchContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                }
                break;
            case 4:
                {
                    ModifyContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                }
                break;
            case 5:
                {
                    DeleteContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                }
                break;
            case 6:
                runing = false;
                break;
            default:
                Console.WriteLine("Favor introducir una entrada valida");
                break;
        }
    }
    catch (NullReferenceException)
    {
        Console.WriteLine("El contacto seleccionado no existe.\n");
    }
    catch (ArgumentException)
    {
        Console.WriteLine("El contacto seleccionado no existe.\n");
    }
    catch (FormatException)
    {
        Console.WriteLine("Ha ocurrido un problema. Por favor, intente mas tarde.\n");
        runing = false;
    }
}



static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("Digite el nombre de la persona");
    string name = Console.ReadLine();
    Console.WriteLine("Digite el apellido de la persona");
    string lastname = Console.ReadLine();
    Console.WriteLine("Digite la dirección");
    string address = Console.ReadLine();
    Console.WriteLine("Digite el telefono de la persona");
    string phone = Console.ReadLine();
    Console.WriteLine("Digite el email de la persona");
    string email = Console.ReadLine();
    Console.WriteLine("Digite la edad de la persona en números");
    int age = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");

    bool isBestFriend = Convert.ToInt32(Console.ReadLine()) == 1;

    var id = ids.Count + 1;
    ids.Add(id);
    names.Add(id, name);
    lastnames.Add(id, lastname);
    addresses.Add(id, address);
    telephones.Add(id, phone);
    emails.Add(id, email);
    ages.Add(id, age);
    bestFriends.Add(id, isBestFriend);
}

static void ShowContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    PrintContactHeader();
    foreach (var id in ids)
    {
        string isBestFriendStr = (bestFriends[id]) ? "Si" : "No";
        Console.WriteLine($"{names[id]}         {lastnames[id]}         {addresses[id]}         {telephones[id]}            {emails[id]}            {ages[id]}          {isBestFriendStr}");
    }
}

static void SearchContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("Introduzca el parametro de busqueda: 1. ID 2. Nombre 3. Apellido: ");
    var op = Int32.Parse(Console.ReadLine());

    switch (op)
    {
        case 1:
            Console.WriteLine("Introduzca el ID del contacto: ");
            int id = Int32.Parse(Console.ReadLine());

            PrintContactHeader();
            string isBestFriendStr = (bestFriends[id]) ? "Si" : "No";
            Console.WriteLine($"{names[id]}         {lastnames[id]}         {addresses[id]}         {telephones[id]}            {emails[id]}            {ages[id]}          {isBestFriendStr}");

            break;

        case 2:
            Console.WriteLine("Introduzca el nombre del contacto: ");
            string nameCrit = Console.ReadLine();

            var nameFound = names.Where(n => n.Value == nameCrit).ToList();
            PrintContactHeader();

            foreach (var name in nameFound)
            {
                string isBestFriendStr2 = (bestFriends[name.Key]) ? "Si" : "No";
                Console.WriteLine($"{names[name.Key]}         {lastnames[name.Key]}         {addresses[name.Key]}         {telephones[name.Key]}            {emails[name.Key]}            {ages[name.Key]}          {isBestFriendStr2}");
            }
            break;

        case 3:
            Console.WriteLine("Introduzca el apellido del contacto: ");
            var lastNCrit = Console.ReadLine();

            var lastName = lastnames.Where(l => l.Value == lastNCrit).ToList();
            PrintContactHeader();

            foreach (var lname in lastName)
            {
                string isBestFriendStr2 = (bestFriends[lname.Key]) ? "Si" : "No";
                Console.WriteLine($"{names[lname.Key]}         {lastnames[lname.Key]}         {addresses[lname.Key]}         {telephones[lname.Key]}            {emails[lname.Key]}            {ages[lname.Key]}          {isBestFriendStr2}");
            }
            break;

        default:
            Console.WriteLine("Favor introducir una entrada valida");
            break;
    }


}

static void ModifyContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("Introduzca el numero de identfiicacion del contacto a modificar: ");
    Console.WriteLine($"ID      Nombre          Apellido            Dirección           Telefono            Email           Edad            Es Mejor Amigo?");
    Console.WriteLine($"___________________________________________________________________________________________________________________________________");
    foreach (var id in ids)
    {
        string isBestFriendStr = (bestFriends[id]) ? "Si" : "No";
        Console.WriteLine($"{ids[id - 1]}       {names[id]}         {lastnames[id]}         {addresses[id]}         {telephones[id]}            {emails[id]}            {ages[id]}          {isBestFriendStr}");
    }


    var ConID = Int32.Parse(Console.ReadLine());

    Console.WriteLine("Seleccione que parametro desea modificar en el orden numerico de los datos del contacto, (1. Nombre, 2. Apellido 3. Direccion...): ");
    var sel = Int32.Parse(Console.ReadLine());

    switch (sel)
    {
        case 1:

            Console.WriteLine($"Escriba el nuevo nombre para {names[ConID]}.");
            string newName = Console.ReadLine();

            Console.WriteLine($"¿Estas seguro de que quieres cambiar el nombre de \"{names[ConID]} {lastnames[ConID]}\" por \"{newName}\"? \n Presione 1 para confirmar el cambio, y 2 para descartarlo.");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                names[ConID] = newName;
                Console.WriteLine("El nombre del contacto ha sido actualizado con exito.");
            }
            else
            {
                Console.WriteLine($"No se ha podido cambiar el nombre de {names[ConID]}");
            }
            break;

        case 2:
            Console.WriteLine($"Escriba el nuevo apellido para {names[ConID]}.");
            string newLname = Console.ReadLine();

            Console.WriteLine($"¿Estas seguro de que quieres cambiar el apellido de \"{names[ConID]} {lastnames[ConID]}\" por \"{newLname}\"? \n Presione 1 para confirmar el cambio, y 2 para descartarlo.");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                names[ConID] = newLname;
                Console.WriteLine("El apellido del contacto ha sido actualizado con exito.");
            }
            else
            {
                Console.WriteLine($"No se ha podido cambiar el apellido de {names[ConID]}");
            }
            break;

        case 3:
            Console.WriteLine($"Escriba la nueva direccion para {names[ConID]}.");
            string newAddy = Console.ReadLine();

            Console.WriteLine($"¿Estas seguro de que quieres cambiar la direccion de \"{names[ConID]} {lastnames[ConID]}\" en {addresses[ConID]} por {newAddy}? \n Presione 1 para confirmar el cambio, y 2 para descartarlo.");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                names[ConID] = newAddy;
                Console.WriteLine("La direccion del contacto ha sido actualizado con exito.");
            }
            else
            {
                Console.WriteLine($"No se ha podido cambiar la direccion de {names[ConID]}");
            }
            break;

        case 4:
            Console.WriteLine($"Escriba el nuevo no. de telefono para {names[ConID]}.");
            string newTelef = Console.ReadLine();

            Console.WriteLine($"¿Estas seguro de que quieres cambiar el numero de \"{names[ConID]} {lastnames[ConID]}\" de {telephones[ConID]} por {newTelef}? \n Presione 1 para confirmar el cambio, y 2 para descartarlo.");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                names[ConID] = newTelef;
                Console.WriteLine("El número de telefono del contacto ha sido actualizado con exito.");
            }
            else
            {
                Console.WriteLine($"No se ha podido cambiar el número de telefono de {names[ConID]}");
            }
            break;

        case 5:

            Console.WriteLine($"Escriba la nueva direccion de correo electronico para {names[ConID]}.");
            string newEmail = Console.ReadLine();

            Console.WriteLine($"¿Estas seguro de que quieres cambiar la direccion de correo electronico de \"{names[ConID]} {lastnames[ConID]}\" de {emails[ConID]} por {newEmail}? \n Presione 1 para confirmar el cambio, y 2 para descartarlo.");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                names[ConID] = newEmail;
                Console.WriteLine("La direccion de correo electronico del contacto ha sido actualizado con exito.");
            }
            else
            {
                Console.WriteLine($"No se ha podido cambiar la direccion de correo electronico de {names[ConID]}");
            }
            break;

        case 6:

            Console.WriteLine($"No se puede cambiar la edad del contacto {names[ConID]}");
            break;

        case 7:

            if (bestFriends[ConID])
            {
                Console.WriteLine($"¿Desea remover el estado de \'mejor amigo\' a {names[ConID]}? :( \n Confirme con 1. Rechace con 2.");
                if (int.Parse(Console.ReadLine()) == 1)
                {
                    bestFriends[ConID] = false;
                    Console.WriteLine($"{names[ConID]} ha sido removido de la lista de mejores amigos.");
                }
            }
            else
            {
                Console.WriteLine($"¿Desea promover a {names[ConID]} en tu lista de mejores amigos? \n Confirme con 1. Rechace con 2.");
                if (int.Parse(Console.ReadLine()) == 1)
                {
                    bestFriends[ConID] = true;
                    Console.WriteLine($"{names[ConID]} ha sido promovido a \'mejor amigo\'.");
                }
            }
            break;
    }

}

static void DeleteContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    SearchContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);

    var cancelID = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine($"¿Esta seguro que quiere eliminar el contacto {names[cancelID]} {lastnames[cancelID]}? \n Presione 1 para confirmar, 2 para denegar.");
    if (Int32.Parse(Console.ReadLine()) == 1)
    {
        ids.Remove(cancelID);
        names.Remove(cancelID);
        lastnames.Remove(cancelID);
        addresses.Remove(cancelID);
        telephones.Remove(cancelID);
        emails.Remove(cancelID);
        ages.Remove(cancelID);
        bestFriends.Remove(cancelID);

        Console.WriteLine("El contacto ha sido exitosamente eliminado.");

    }
    else
    {
        Console.WriteLine("El contacto no fue eliminado.");
    }
}

static void PrintContactHeader()
{
    Console.WriteLine($"Nombre          Apellido            Dirección           Telefono            Email           Edad            Es Mejor Amigo?");
    Console.WriteLine($"____________________________________________________________________________________________________________________________");

}
