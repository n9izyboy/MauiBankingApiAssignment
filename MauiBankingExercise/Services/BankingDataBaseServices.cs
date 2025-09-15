using Android.Provider;
using MauiBankingExercise.Models;
using Microsoft.Win32.SafeHandles;
using Org.Apache.Http.Impl.Client;
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

        private readonly HttpClient _httpClient;

        public DatabaseService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true

            };
            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri("https://localhost:7280/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
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

      


       
    }
}
    

