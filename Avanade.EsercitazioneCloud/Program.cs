using System;

namespace Avanade.EsercitazioneCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            //Chiedere il nome di un database server, il nome del database, uno username e la password da linea di comando
            Console.WriteLine("Nome server?");
            string server = Console.ReadLine();
            Console.WriteLine("Nome del databse a cui accedere?");
            string db = Console.ReadLine();
            Console.WriteLine("Username per accedere al DB?");
            string usernamer = Console.ReadLine();
            Console.WriteLine("Password per accedere?");
            string psw = Console.ReadLine();


        }
    }
}
