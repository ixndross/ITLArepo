namespace ContactosOOP
{
    public class Contact
    { 
        public Contact()
        {
            //Constructor vacio para obtener sobrecarga de metodos y permitir la creacion de contactos sin argumentos.
        }
        
        public Contact(int id, string name, string lastname, string phone, string email, string address, int age, bool isFavorite)
        {
            ID = id;
            Name = name;
            LastName = lastname;
            Phone = phone;
            Email = email;
            Address = address;
            Age = age;
            IsFavorite = isFavorite;

            //Constructor base
        }

        public int ID { get;  set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; } 
        public bool IsFavorite { get; set; }

    }
}
