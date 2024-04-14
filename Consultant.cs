using System.Collections.Generic;
using System.Windows.Media;

namespace DeepDiveIntoOOPPart1
{
    internal class Consultant : Worker, IOwerwriteClient
    {
        private readonly Repository repository = new Repository();

        private readonly string position = "Консультант";

        internal Consultant()
        {
            Position = this.position;
        }

        internal override ClientForm GetClientForm(Worker worker, Client client)
        {
            ClientForm clientForm = base.GetClientForm(worker, client);

            clientForm.PhoneNumberTextBox.IsReadOnly = false;
            clientForm.PhoneNumberTextBox.Background = Brushes.White;

            clientForm.OK_Button.IsEnabled = true;
            clientForm.Write_Button.IsEnabled = true;

            return clientForm;
        }
        
        public string OwerwriteClient(Client client)
        {
            string changedFields = "Перезапись без изменения полей";            

            List<Client> clients = repository.GetClientsFromFile();            

            for (int i = 0; i < clients.Count; i++)
            {
                if (client.ClientId == clients[i].ClientId)
                {
                    if (client.PhoneNumber != clients[i].PhoneNumber)
                    {
                        changedFields = $"Номер телефона " +
                            $"(было \"{clients[i].PhoneNumber}\", " +
                            $"стало \"{client.PhoneNumber}\"); ";
                        clients[i].PhoneNumber = client.PhoneNumber;

                        repository.WriteClientsToFile(clients);
                    }                  

                    break;
                }                
            }            

            return changedFields;
        }
    }
}
