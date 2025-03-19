using System.Diagnostics;

List<decimal> typedNumbers = new List<decimal>();

decimal result = 0;
int typedOption = 1;
bool running = true;
int will = 0;


Console.WriteLine("This is the best calculator.");

while (running)
{
    DisplayHeader();

    try
    {
        typedOption = Convert.ToInt32(Console.ReadLine());

        if (typedOption == 5)
        { running = false; }
        else
        {
            Console.Write("Please type the first number: ");
            typedNumbers.Add(Convert.ToDecimal(Console.ReadLine()));

            Console.Write("Please type the second number: ");
            typedNumbers.Add(Convert.ToDecimal(Console.ReadLine()));

            TypeMore(will, typedNumbers);

            switch (typedOption)
            {
                case 1:

                        result = AddList(ref typedNumbers);

                    break;
                case 2:

                        result = RestList(ref typedNumbers);

                    break;
                case 3:

                        result = ProdList(ref typedNumbers);

                    break;
                case 4:
                    result = QuotList(ref typedNumbers);
                    break;
                default:
                    result = 0;
                    break;
            }

            Console.WriteLine($"The Result of the operation is:{result}");


        }
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"You can not divide by zero: {ex.Message}");
    }
    catch (ArithmeticException ex)
    {
        Console.WriteLine($"You can not divide by zero: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.Write($"You need to choose a correct option: {ex.Message}");
    }
    finally
    {
        Console.WriteLine("\nFormatting the operation history for new inputs.\n");
    }

}
//procceess

static void DisplayHeader()
{
    Console.WriteLine("Please Type the option number than you want");
    Console.WriteLine("---------------------------------------");
    Console.WriteLine("1. Sum, \n2. Substract,  \n3. Multiplication,  \n4. Division,  \n5. Exit");
}

static void TypeMore(int will, List<decimal> typedNumbers)
{
    while (will != 2)
    {
        Console.WriteLine("Do you want to continue inserting numbers? 1. Yes, 2. No");
        will = Convert.ToInt32(Console.ReadLine());
        if (will == 1)
        {
            Console.WriteLine("Please Type another number");
            typedNumbers.Add(Convert.ToDecimal(Console.ReadLine()));
        }
    }
}


//function
static decimal Add(decimal valueToModify, decimal value)
{
    valueToModify += value;
    return valueToModify;
}

static decimal Rest(ref decimal m, decimal substrahend)
{
    m -= substrahend;
    return m;
}

static decimal Prod(ref decimal prod, decimal multiple)
{
    prod *= multiple;
    return prod;
}

static decimal Quot(ref decimal d, decimal divisor)
{
    d /= divisor;
    return d;
}

static decimal AddList(ref List<decimal> typedNumbers)
{
    decimal result = 0;
    foreach (int typedNumber in typedNumbers)
    {
        result = Add(result, typedNumber);
    }

    typedNumbers.Clear();

    return result;
}

static decimal RestList(ref List<decimal> typedNumbers)
{
    decimal result = 0;
    decimal minuend1 = typedNumbers[0];
    typedNumbers.RemoveAt(0);

    foreach (int typedNumber in typedNumbers)
    {
        result = Rest(ref minuend1, typedNumber);
    }

    typedNumbers.Clear();
    
    return result;
}

static decimal ProdList(ref List <decimal> typedNumbers)
{
    decimal result = 0;
    decimal fMultiple = 1;

    foreach (int typedNumber in typedNumbers)
    {
        result = Prod(ref fMultiple, typedNumber);
    }

    typedNumbers.Clear();

    return result;
}

static decimal QuotList(ref List<decimal> typedNumbers)
{
    decimal result = 0;
    decimal dividend = typedNumbers[0];
    typedNumbers.RemoveAt(0);

    foreach (int typedNumber in typedNumbers)
    {
        result = Quot(ref dividend, typedNumber);
    }

    typedNumbers.Clear();
    
    return result;
}