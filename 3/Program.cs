using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int letter = int.Parse(Console.ReadLine());
        int num = int.Parse(Console.ReadLine());
        string code = GenerateCode(letter, num);

        Dictionary<string, string> products = new Dictionary<string, string>();

        while (true)
        {
            string name = Console.ReadLine();

            if (name.ToLower() == "stop")
            {
                break;
            }

            products.Add(code, name);
            code = GenerateNextCode(code);
        }

        string searchCode = Console.ReadLine();

        if (products.ContainsKey(searchCode))
        {
            Console.WriteLine(products[searchCode]);
        }
        else
        {
            Console.WriteLine("Not found!");
        }
    }

    static string GenerateCode(int letter, int num)
    {
        string code = "";

        for (int i = 0; i < letter; i++)
        {
            code += 'A';
        }

        for (int i = 0; i < num; i++)
        {
            code += '0';
        }

        return code;
    }

    static string GenerateNextCode(string code)
    {
        char[] chars = code.ToCharArray();

        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (chars[i] == 'Z')
            {
                chars[i] = 'A';
            }
                
            else if (chars[i] == '9')
            {
                chars[i] = '0';
            }

            else
            {
                chars[i]++;
                break;
            }
        }

        return new string(chars);
    }
}