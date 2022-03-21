namespace Calculator;

public static class Start
{
    public static void Main()
    {
        Console.WriteLine("Give first number: ");
        var a = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Give operator: ");
        var op = Convert.ToChar(Console.ReadLine()!);
        Console.WriteLine("Give second number: ");
        var b = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Result: ");
        switch (op)
        {
            case '+':
                Console.WriteLine(Operations.Add(a, b));
                break;
            case '-':
                Console.WriteLine(Operations.Sub(a, b));
                break;
            case '*':
                Console.WriteLine(Operations.Mul(a, b));
                break;
            case '/':
                Console.WriteLine(Operations.Div(a, b));
                break;
            default:
                Console.WriteLine("Error");
                break;
        }
    }
}