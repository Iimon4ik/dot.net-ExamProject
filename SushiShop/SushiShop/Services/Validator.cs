namespace SushiShop;

public static class Validator
{
    public static string Validation()
    {
        while(true)
        {
            string data = Console.ReadLine();
            if (data.Equals(String.Empty))
            {
                Console.Write("You didn't enter anything! Enter a valid value!");
                Console.WriteLine();
            }
            else
            {
                return data;
            }
        }
    }
    
    public static string PhoneValidation()
    {
        while(true)
        {
            string data = Console.ReadLine();
            if (data.Equals(String.Empty))
            {
                Console.Write("You didn't enter anything! Enter a valid value!");
                Console.WriteLine();
            }
            else if (data.Any(char.IsLetter))
            {
                Console.Write("Phone number must not contain letters! Enter a valid value!");
                Console.WriteLine();
            }
            else
            {
                return data;
            }
        }
    }
    
    public static string EmailAddressValidation()
    {
        while(true)
        {
            string data = Console.ReadLine();
            if (data.Equals(String.Empty))
            {
                Console.Write("You didn't enter anything! Enter a valid value!");
                Console.WriteLine();
            }
            else
            {
                if (data.Contains('@') && (data.Contains(".ru")||data.Contains(".com")||data.Contains(".by")||data.Contains(".org")||data.Contains(".ua")||data.Contains(".net")))
                {
                    return data;
                }
                Console.Write("Your email should contain '@' and domain (.com/.ru/.by/.ua/.org/.net)! Enter a valid value!");
                Console.WriteLine();
            }
        }
    }

    public static bool ConfirmationValidation()
    {
        while (true)
        {
            string data = Console.ReadLine();
            if (data.Equals(String.Empty))
            {
                Console.Write("You didn't enter anything! Enter a valid value!");
                Console.WriteLine();
            }
            else if ((data.ToLower().Equals("n")) || (data.ToLower().Equals("y")))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Wrong data! Enter a valid value - [Y] or [N]!");
            }
        }
    }
    public static void WrongDataMessage()
    {
        Console.WriteLine("Wrong data! Enter a valid value!");
    }
}