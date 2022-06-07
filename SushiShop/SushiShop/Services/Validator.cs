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
                Console.Write("�� ������ �� �����! �������� �����!");
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
                Console.Write("�� ������ �� �����! �������� �����!");
                Console.WriteLine();
            }
            else if (data.Any(char.IsLetter))
            {
                Console.Write("���������� ����� �� ������ ��������� ����! ������� ������ �����!");
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
                Console.Write("�� ������ �� �����! �������� �����!");
                Console.WriteLine();
            }
            else
            {
                if(Regex.IsMatch(data, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                       RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    return data;
                }
                Console.Write("������� ���������� ����� ����������� �����!");
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
                Console.Write("�� ������ �� �����! ������� �����!");
                Console.WriteLine();
            }
            else if ((data.ToLower().Equals("���")) || (data.ToLower().Equals("��")))
            {
                return data;
            }
            else
            {
                Console.WriteLine("������������ �����! ������� - [��] ���� [���]!");
            }
        }
    }
    public static void WrongDataMessage()
    {
        Console.WriteLine("������������ �����!");
    }
}