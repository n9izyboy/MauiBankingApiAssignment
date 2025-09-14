using MauiBankingExercise.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace MauiBankingExercise.Services
{
    public class BankingDataBaseServices
    {



        private static BankingDataBaseServices _instance;

        public static BankingDataBaseServices GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BankingDataBaseServices();
            }
            return _instance;
        }

        public BankingDataBaseServices()
        {

        }
        public List<Bank> GetAllBanks()
        {

            return new List<Bank>();

        }

        private SQLite.SQLiteConnection _dbconnection;
        public string GetDatabasePath()
        {

            string dbName = "BankingDatabase.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(pathToDb, dbName);
            return Path.Combine(pathToDb, filename);
        }

        private void ExtractDatabaseConnection()
        {
            var assembly = typeof(BankingDataBaseServices).Assembly;
            Stream stream = assembly.GetManifestResourceStream("MauiBankingExcercise.Data.BankingDatabase.db");
            var dbPath = GetPath();




            if (stream != null)
            {
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    using (FileStream fileStream = new FileStream(dbPath, (FileAccess)FileMode.Create))
                    {
                        BinaryWriter bw = new BinaryWriter(fileStream);
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        bw.Write(bytes);
                    }
                }
            }
        }

        private SafeFileHandle GetPath()
        {
           throw new NotImplementedException();
        }

        public BankingDataBaseServices(SQLite.SQLiteConnection dbconnection)
        {
            if (!File.Exists(GetDatabasePath()))
            {
                ExtractDatabaseConnection();
            }
            _dbconnection = dbconnection;
            ExtractDatabaseConnection();
        }


        public List<Bank> GetAllBanksFromDatabase()
        {
            // Ensure the database connection is established
            if (_dbconnection == null)
            {
                _dbconnection = new SQLite.SQLiteConnection(GetDatabasePath());
                ExtractDatabaseConnection();
            }
            // Query the database for all banks
            return _dbconnection.Table<Bank>().ToList();
        }
    }
}
    

