using System.Text.RegularExpressions;

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
                Console.Write("Вы ничего не ввели! Напишите ответ!");
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
                Console.Write("Вы ничего не ввели! Напишите ответ!");
                Console.WriteLine();
            }
            else if (data.Any(char.IsLetter))
            {
                Console.Write("Телефонный номер не должен содержать букв! Введите только цифры!");
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
                Console.Write("Вы ничего не ввели! Напишите ответ!");
                Console.WriteLine();
            }
            else
            {
                if(Regex.IsMatch(data, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                       RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    return data;
                }
                Console.Write("Введите корректный адрес электронной почты!");
                Console.WriteLine();
            }
        }
    }

    public static string ConfirmationValidation()
    {
        while (true)
        {
            string data = Console.ReadLine();
            if (data.Equals(String.Empty))
            {
                Console.Write("Вы ничего не ввели! Введите ответ!");
                Console.WriteLine();
            }
            else if ((data.ToLower().Equals("нет")) || (data.ToLower().Equals("да")))
            {
                return data;
            }
            else
            {
                Console.WriteLine("Некорректный ответ! Введите - [Да] либо [Нет]!");
            }
        }
    }
    public static void WrongDataMessage()
    {
        Console.WriteLine("Некорректный ответ!");
    }
}