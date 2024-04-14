using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DeepDiveIntoOOPPart1
{    
    public partial class ClientList : Window
    {
        private readonly Repository repository = new Repository();
        internal string ClientFilePath { get { return repository.ClientFilePath; } }

        internal Worker Worker { get; private set; }        

        internal ClientList(Worker worker)
        {
            InitializeComponent();
            this.Worker = worker;
        }       

        private class СlientListItem
        {
            public string ClientID { get; set; }
            public string ClientName { get; set; }
        }

        private void FillСlientList()
        {
            bool status = repository.CheckBeforeReading(ClientFilePath);

            if (status == true)
            {
                List<Client> clients = repository.GetClientsFromFile();

                foreach (Client client in clients)
                {
                    СlientListItem clientListItem = new СlientListItem()
                    {
                        ClientID = client.ClientId,
                        ClientName = client.ClientName
                    };

                    СlientList.Items.Add(clientListItem);
                }               
            }
        }

        private void CreateNewClientForm()
        {
            ClientForm clientForm = new ClientForm(Worker)
            {
                Owner = this
            };

            clientForm.Show();
        }        

        private void OpenClientForm()
        {
            if (!string.IsNullOrEmpty($"{СlientList.SelectedItem as СlientListItem}"))
            {
                string id = (СlientList.SelectedItem as СlientListItem).ClientID;

                List<Client> clients = repository.GetClientsFromFile();

                Client client = repository.FindClientById(clients, id);

                ClientForm clientForm = new ClientForm(Worker)
                {
                    Owner = this,
                };

                if (Worker.GetType() == typeof(Consultant))
                {
                    clientForm = (Worker as Consultant).GetClientForm(Worker, client);
                }

                if (Worker.GetType() == typeof(Manager))
                {
                    clientForm = (Worker as Manager).GetClientForm(Worker, client);
                }

                clientForm.Show();
            }                                                            
        }        

        private void ClientList_Loaded(object sender, RoutedEventArgs e)
        {
            FillСlientList();                       
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateNewClientForm();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            СlientList.Items.Clear();
            FillСlientList();
        }

        private void MouseLeft_Click(object sender, MouseButtonEventArgs e)
        {            
            OpenClientForm();
        }
    }
}

