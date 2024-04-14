using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;

namespace DeepDiveIntoOOPPart1
{
    internal class Manager : Worker, IOwerwriteClient
    {
        private readonly Repository repository = new Repository();
        internal string ClientFilePath { get { return repository.ClientFilePath; } }

        private readonly string position = "Менеджер";

        internal Manager()
        {
            Position = this.position;
        }

        internal override ClientList GetClientList(Worker worker)
        {
            ClientList clientList = base.GetClientList(worker);

            clientList.Create_Button.IsEnabled = true;

            return clientList;
        }

        internal override ClientForm GetClientForm(Worker worker, Client client)
        {
            ClientForm clientForm = base.GetClientForm(worker, client);

            clientForm.PassportSeriesTextBox.Text = client.PassportSeries.ToString();
            clientForm.PassportNumberTextBox.Text = client.PassportNumber.ToString();

            clientForm.SurnameTextBox.IsReadOnly = false;
            clientForm.SurnameTextBox.Background = Brushes.White;

            clientForm.NameTextBox.IsReadOnly = false;
            clientForm.NameTextBox.Background = Brushes.White;

            clientForm.PatronymicTextBox.IsReadOnly = false;
            clientForm.PatronymicTextBox.Background = Brushes.White;

            clientForm.PhoneNumberTextBox.IsReadOnly = false;
            clientForm.PhoneNumberTextBox.Background = Brushes.White;

            clientForm.PassportSeriesTextBox.IsReadOnly = false;
            clientForm.PassportSeriesTextBox.Background = Brushes.White;

            clientForm.PassportNumberTextBox.IsReadOnly = false;
            clientForm.PassportNumberTextBox.Background = Brushes.White;

            clientForm.OK_Button.IsEnabled = true;
            clientForm.Write_Button.IsEnabled = true;
            clientForm.LoadHistory_Button.IsEnabled = true;

            return clientForm;
        }

        internal void WriteClient(Client client)
        {
            string clientInString = repository.ConvertClientToString(client);

            using (StreamWriter streamWriter =
                new StreamWriter(ClientFilePath, true, Encoding.Unicode))
            {
                streamWriter.WriteLine(clientInString);
            }
        }

        private string ProcessChangedFields(string changedFields)
        {            
            changedFields = changedFields.Trim();
            changedFields = changedFields.TrimEnd(';');

            return changedFields;
        }           

        public string OwerwriteClient(Client client)
        {
            string changedFields = String.Empty;           

            List<Client> clients = repository.GetClientsFromFile();

            for (int i = 0; i < clients.Count; i++)
            {
                if (client.ClientId == clients[i].ClientId)
                {
                    if (client.Surname != clients[i].Surname)
                    {                     
                        changedFields = $"Фамилия " +
                            $"(было \"{clients[i].Surname}\", " +
                            $"стало \"{client.Surname}\"); ";
                        clients[i].Surname = client.Surname;
                    }
                    if (client.Name != clients[i].Name)
                    {                        
                        changedFields += $"Имя " +
                            $"(было \"{clients[i].Name}\", " +
                            $"стало \"{client.Name}\"); ";
                        clients[i].Name = client.Name;
                    }
                    if (client.Patronymic != clients[i].Patronymic)
                    {                        
                        changedFields += $"Отчество " +
                            $"(было \"{clients[i].Patronymic}\", " +
                            $"стало \"{client.Patronymic}\"); ";
                        clients[i].Patronymic = client.Patronymic;
                    }
                    if (client.PhoneNumber != clients[i].PhoneNumber)
                    {                       
                        changedFields += $"Номер телефона " +
                            $"(было \"{clients[i].PhoneNumber}\", " +
                            $"стало \"{client.PhoneNumber}\"); ";
                        clients[i].PhoneNumber = client.PhoneNumber;
                    }
                    if (client.PassportSeries != clients[i].PassportSeries)
                    {                        
                        changedFields += $"Серия паспорта " +
                            $"(было \"{clients[i].PassportSeries}\", " +
                            $"стало \"{client.PassportSeries}\"); ";
                        clients[i].PassportSeries = client.PassportSeries;
                    }
                    if (client.PassportNumber != clients[i].PassportNumber)
                    {                        
                        changedFields += $"Номер паспорта " +
                            $"(было \"{clients[i].PassportNumber}\", " +
                            $"стало \"{client.PassportNumber}\")";
                        clients[i].PassportNumber = client.PassportNumber;
                    }

                    if (changedFields.Length != 0)
                    {
                        repository.WriteClientsToFile(clients);
                        ProcessChangedFields(changedFields);
                    }
                    else
                    {
                        changedFields = "Перезапись без изменения полей";
                    }                   

                    break;
                }
            }

            if (changedFields.Length == 0)
            {
                changedFields = "Идентификатор не найден";
            }

            return changedFields;
        }
    }
}
