namespace MemberNova.Helpers
{
    class Helpers
    {
        public static void Header()
        {
            Console.Write("Eliga una opción: \n");
            Console.Write("1. Administrar usuarios.\t\t2. Administrar membresias.\t\t3. Ver pagos.\t\t4. Administrar beneficios\t\t5. Salir del programa.\n");
        }

        public static void UserMenu()
        {
            int TipoUsuario = 0;
            Console.WriteLine("Seleccione el tipo de usuario a gestionar \n1. Usuario Regular.\n2. Usuario VIP.\n3. Salir.\n");
            TipoUsuario = Int32.Parse(Console.ReadLine());

            switch (TipoUsuario)
            {
                case 1:
                    RegUserHelpers.RegularUserSelection();
                    break;

                case 2:
                    VIPUsersHelpers.VIPUserSelection();
                    break;

                default:
                    Console.WriteLine("Por favor, introducir una entrada válida.");
                    break;
            }

        }

        public static void PaymentMenu()
        {
            int TipoPago = 0;
            Console.WriteLine("Seleccione el tipo de pago a gestionar \n1. Pagos regulares.\n2. Pagos VIPs\n3. Salir.\n");
            TipoPago = Int32.Parse(Console.ReadLine());

            switch (TipoPago)
            {
                case 1:
                    Payments.PaymentPortal();
                    break;
                case 2:
                    VIPPayments.VIPPaymentPortal();
                    break;
            }
            
        }

    }
}
