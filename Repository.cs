using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeepDiveIntoOOPPart1
{
    internal class Repository
    {
        private readonly string clientFilePath = "ClientDataStorage.txt";
        private readonly string idFilePath = "IDStorage.txt";

        internal string ClientFilePath { get { return this.clientFilePath; } }

        internal bool CheckBeforeReading(string filePath)
        {
            bool status = false;

            if (File.Exists(filePath) && new FileInfo(filePath).Length > 6)
            {
                status = true;
            }

            return status;
        }

        internal int AssignId()
        {
            int id;
            string streamString;            

            if (File.Exists(idFilePath) && new FileInfo(idFilePath).Length > 6)
            {
                using (StreamReader streamReader =
                    new StreamReader(idFilePath, Encoding.Unicode))
                {
                    streamString = $"{streamReader.ReadLine()}";
                }

                id = Convert.ToInt32(streamString) + 1;
                streamString = id.ToString();
            }
            else
            {
                streamString = "1";
                id = Convert.ToInt32(streamString);
            }

            using (StreamWriter streamWriter =
                new StreamWriter(idFilePath, false, Encoding.Unicode))
            {
                streamWriter.WriteLine(streamString);
            }

            return id;
        }        

        internal List<Client> GetClientsFromFile()
        {
            List<Client> clients = new List<Client>();

            using (StreamReader streamReader =
                new StreamReader(ClientFilePath, Encoding.Unicode))
            {
                while (!streamReader.EndOfStream)
                {
                    string streamString = $"{streamReader.ReadLine()}";
                    string[] streamStringSplited = streamString.Split('#');

                    Client client = new Client(streamStringSplited[0],
                        streamStringSplited[1], streamStringSplited[2],
                        streamStringSplited[3], streamStringSplited[4],
                        streamStringSplited[5], streamStringSplited[6],
                        streamStringSplited[7]);

                    clients.Add(client);
                }
            }

            return clients;
        }

        internal string ConvertClientToString(Client client)
        {
            string clientInString = $"{client.ClientId}#" +
                $"{client.Surname}#" +
                $"{client.Name}#" +
                $"{client.Patronymic}#" +
                $"{client.ClientName}#" +
                $"{client.PhoneNumber}#" +
                $"{client.PassportSeries}#" +
                $"{client.PassportNumber}";

            return clientInString;
        }

        internal void WriteClientsToFile(List<Client> clients)
        {
            using (StreamWriter streamWriter =
                new StreamWriter(ClientFilePath, false, Encoding.Unicode))
            {
                foreach (Client client in clients)
                {
                    string clientInString = ConvertClientToString(client);
                    streamWriter.WriteLine(clientInString);
                }
            }
        }

        internal Client FindClientById(List<Client> clients, string id)
        {
            Client client = clients.FirstOrDefault();

            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ClientId == id)
                {
                    client = clients[i];
                    break;
                }
            }

            return client;
        }
    }
}
