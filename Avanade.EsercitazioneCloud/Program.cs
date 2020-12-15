using System;
using System.Collections.Generic;

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
            string username = Console.ReadLine();
            Console.WriteLine("Password per accedere?");
            string psw = Console.ReadLine();

            Console.WriteLine("La Connection String è:");
            string connectionString = Metodi.GetConnectionString(server, db, username, psw);
            Console.WriteLine(connectionString);

            //string connectionString= @"Server = tcp:serenaserver.database.windows.net,1433; Initial Catalog = EsercitazioneCloud; Persist Security Info = False; User ID = serenac; Password =Password1; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";
            //Console.WriteLine("##################");
            //Console.WriteLine("CREO LA NUOVA TABELLA NEL DB SPECIFICATO");
            //Metodi.CreaTabella(connectionString);

            //Console.WriteLine("##################");
            //Console.WriteLine("INSERISCO UN NUOVO PRODOTTO NELLA TABELLA");
            //Metodi.InserisciProdotto(connectionString);

            //Console.WriteLine("###################");
            //Console.WriteLine("INSERIAMO UN SECONDO PRODOTTO");
            //Metodi.InserisciProdotto(connectionString);

            Console.WriteLine("###################");
            Console.WriteLine("Salvo i prodotti in una lista e la stampo ora: ");
            List<Products> listap = Metodi.SalvaProdotti(connectionString);
            foreach (var p in listap)
            {
                Console.WriteLine("Prodotto {0}, {1}, Prezzo {2}, Codice {3}",p.id,p.Description,p.Price,p.Code);
                
            }

            Console.WriteLine("###################");
            Console.WriteLine("Questi sono tutti i prodotti nel DB: ");
            Metodi.StampaProdotti(connectionString);
        }
    }
}
