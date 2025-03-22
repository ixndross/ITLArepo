using System.ComponentModel;
using System.Linq;
using System.Timers;
using ContactosOOP;

Console.WriteLine("Bienvenido a la lista de contactos.");

bool runing = true;

List<Contact> Contactos = new List<Contact>();

while (runing)
{
    try
    {
        HeaderHelpers.Selection();

        int typeOption = Convert.ToInt32(Console.ReadLine());

        switch (typeOption)
        {
            case 1:
                {
                    HeaderHelpers.AddContact(Contactos);

                }
                break;
            case 2:
                {
                    HeaderHelpers.ShowContacts(Contactos);
                }
                break;
            case 3:
                {
                    HeaderHelpers.SearchContacts(Contactos);
                }
                break;
            case 4:
                {
                    HeaderHelpers.ModifyContacts(Contactos);
                }
                break;
            case 5:
                {
                    HeaderHelpers.DeleteContacts(Contactos);
                }
                break;
            case 6:
                {
                    Console.WriteLine("Cerrando el programa...");
                    runing = false;
                    break;
                }
            default:

                Console.WriteLine("Favor introducir una entrada valida");
                break;
        }
    }
    catch (NullReferenceException)
    {
        Console.WriteLine("El contacto seleccionado no existe.\n ");
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("El programa ha encontrado un error buscando este dato. Por favor intente de nuevo.\n");
    }
}




