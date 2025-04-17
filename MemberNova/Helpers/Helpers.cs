using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace MemberNova.Helpers
{
    class Helpers
    {
        public static void Header()
        {
            Console.Write("Eliga una opción: \n");
            Console.Write("1. Administrar usuarios.\t\t2. Administrar membresias.\t\t3. Ver pagos.\t\t4. Administrar beneficios\t\t5. Salir del programa.\n");
        }

    }
}
