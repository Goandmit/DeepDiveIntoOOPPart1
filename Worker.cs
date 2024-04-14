using System;
using System.Windows.Media;

namespace DeepDiveIntoOOPPart1
{
    internal abstract class Worker
    {
        internal string Position { get; set; }        

        public override string ToString() => $"{Position}";        

        internal virtual ClientList GetClientList(Worker worker)
        {
            ClientList clientList = new ClientList(worker);

            clientList.Create_Button.IsEnabled = false;

            return clientList;
        }

        private string HideString(string normalString)
        {            
            string hiddenString = String.Empty;

            for (int i = 0; i < normalString.Length; i++)
            {
                if (normalString[i] != ' ')
                {
                    hiddenString += "*";
                }
                else
                {
                    hiddenString += " ";
                }
            }

            return hiddenString;
        }

        internal virtual ClientForm GetClientForm(Worker worker, Client client)
        {
            ClientForm clientForm = new ClientForm(worker);

            clientForm.ClientIdTextBox.Text = client.ClientId;
            clientForm.SurnameTextBox.Text = client.Surname;
            clientForm.NameTextBox.Text = client.Name;
            clientForm.PatronymicTextBox.Text = client.Patronymic;
            clientForm.PhoneNumberTextBox.Text = client.PhoneNumber;
            clientForm.PassportSeriesTextBox.Text = HideString(client.PassportSeries);
            clientForm.PassportNumberTextBox.Text = HideString(client.PassportNumber);            

            clientForm.SurnameTextBox.IsReadOnly = true;
            clientForm.SurnameTextBox.Background = Brushes.LightGray;

            clientForm.NameTextBox.IsReadOnly = true;
            clientForm.NameTextBox.Background = Brushes.LightGray;

            clientForm.PatronymicTextBox.IsReadOnly = true;
            clientForm.PatronymicTextBox.Background = Brushes.LightGray;

            clientForm.PhoneNumberTextBox.IsReadOnly = true;
            clientForm.PhoneNumberTextBox.Background = Brushes.LightGray;

            clientForm.PassportSeriesTextBox.IsReadOnly = true;
            clientForm.PassportSeriesTextBox.Background = Brushes.LightGray;            

            clientForm.PassportNumberTextBox.IsReadOnly = true;
            clientForm.PassportNumberTextBox.Background = Brushes.LightGray;

            clientForm.OK_Button.IsEnabled = false;
            clientForm.Write_Button.IsEnabled = false;
            clientForm.LoadHistory_Button.IsEnabled = false;

            return clientForm;
        }        
    }
}
