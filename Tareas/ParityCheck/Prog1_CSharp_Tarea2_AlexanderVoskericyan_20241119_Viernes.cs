Console.Write("Por favor, introduzca un numeero para verificar su paridad: ");

double doBle = Convert.ToDouble(Console.ReadLine());

if (doBle % 2 == 0)
{
    Console.WriteLine($"El número {doBle} es par.");
}
else
{
    Console.WriteLine($"El número {doBle} es impar.");
}

Console.WriteLine("Presione cualquier tecla para cerrar el programa.");

Console.ReadKey();