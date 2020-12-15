using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Avanade.EsercitazioneCloud
{
    public class Metodi
    {
        public static string GetConnectionString(string server, string db, string username, string psw)
        {
            string connString = "Server=" + server + ";Initial Catalog=" + db +
                                            ";Persist Security Info=False;User ID=" + username +
                                            ";Password=" + psw + ";MultipleActiveResultSets=False;" +
                                            "Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            return connString;
        }
        public static void CreaTabella(string cs)
        {
            //Creare una tabella “Products_[nome]” con i seguenti campi:
            //“Code” (string), “Name” (string) e “Description” (string), Price (int)
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"CREATE TABLE dbo.Products_Cusinato
                (
                    ID int IDENTITY(1,1) NOT NULL,
                    Code nvarchar(50),
                    Price int,
                    Description nvarchar(255),
                    CONSTRAINT pk_id PRIMARY KEY (ID)
                );";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Hai creato la tabella Products!");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Errore: " + e.ToString());
                }
                finally
                {
                    con.Close();
                    Console.ReadKey();
                }
            }

        }
        public static void InserisciProdotto(string cs)
        {
            //Inserire un oggetto Product nella tabella su database SQL Azure, eseguendo una “INSERT…” 
            //da ADO.NET.Le informazioni del prodotto devono essere chieste all’utente

            using (SqlConnection connction = new SqlConnection(cs))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connction;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Products_Cusinato";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connction;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Products_Cusinato VALUES(@codice,@prezzo,@descr)";

                insertCommand.Parameters.Add("@codice", System.Data.SqlDbType.NVarChar, 50, "Code");
                insertCommand.Parameters.Add("@prezzo", System.Data.SqlDbType.Int, 500, "Price");
                insertCommand.Parameters.Add("@descr", System.Data.SqlDbType.NVarChar, 255, "Description");

                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;

                //Creiamo il dataset

                System.Data.DataSet dataset = new System.Data.DataSet();
                Console.WriteLine("Codice del prodotto da inserire?");
                string codice = Console.ReadLine();
                Console.WriteLine("Descrizione del prodotto?");
                string descrizione = Console.ReadLine();
                Console.WriteLine("Prezzo del prodotto?");
                string prezzo = Console.ReadLine();
                try
                {
                    connction.Open();
                    adapter.Fill(dataset, "Products_Cusinato");
                    //Creiamo il record
                    DataRow prod = dataset.Tables["Products_Cusinato"].NewRow();
                    prod["Code"] = codice;
                    prod["Price"] = prezzo;
                    prod["Description"] = descrizione;

                    dataset.Tables["Products_Cusinato"].Rows.Add(prod);

                    adapter.Update(dataset, "Products_Cusinato");

                    Console.WriteLine("Hai inserito il prodotto corretamente!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connction.Close();
                }
            }
        }
        public static List<Products> SalvaProdotti(string cs) 
        {
            //	Estrarre le lista dei prodotti contenuti nel database
            List<Products> listap= new List<Products>();
            using (SqlConnection connection = new SqlConnection(cs))
            {

                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Products_Cusinato";

                //Eseguire Command -> DataReader
                SqlDataReader reader = command.ExecuteReader();
                //Leggere i dati
                while (reader.Read())
                {
                    Products p = new Products();
                    p.id = (int)reader["ID"];
                    p.Code = (string)reader["Code"];
                    p.Description = (string)reader["Description"];
                    p.Price = (int)reader["Price"];

                    listap.Add(p);
                }
                connection.Close();
                reader.Close();
            }

            return listap;
        }
        public static void StampaProdotti(string cs)
        {
            //Visualizzare a video (nella console) la lista dei prodotti
            using (SqlConnection connection = new SqlConnection(cs))
            {

                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Products_Cusinato";

                //Eseguire Command -> DataReader
                SqlDataReader reader = command.ExecuteReader();
                //Leggere i dati
                while (reader.Read())
                {
                    Console.WriteLine("{0} - Codice prodotto: {1}, Descrizione: {2}, Prezzo: {3}",
                    reader["ID"], reader["Code"], reader["Description"], reader["Price"]);

                }
                connection.Close();
                reader.Close();
            }
        }
    }
}
