using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DeepDiveIntoOOPPart1
{
    public partial class ClientForm : Window
    {
        private string changedFields;

        private string operation;

        private Regex regex;

        private readonly Repository repository = new Repository();        
        internal string ClientFilePath { get { return repository.ClientFilePath; } }

        internal Worker Worker { get; private set; }               

        internal ClientForm(Worker worker)
        {
            InitializeComponent();
            this.Worker = worker;             
        }
        
        private string GenerateHistoryFilePath(string id)
        {
            string historyFilePath = $"HistoryOfChanges{id}.txt";

            return historyFilePath;
        }

        private string AssignOrReadId()
        {
            string id;

            if (ClientIdTextBox.Text.Length != 0)
            {
                id = ClientIdTextBox.Text;
            }
            else
            {
                id = (repository.AssignId()).ToString();
                ClientIdTextBox.Text = id;
            }

            return id;
        }

        private void NameValidationTextBox
            (object sender, TextCompositionEventArgs e)
        {
            regex = new Regex("[^a-zA-Zа-яА-Я]");

            if (regex.IsMatch(e.Text) && (e.Text != "-") && (e.Text != "'"))
            {
                e.Handled = true;
            }            
        }

        private void NumberValidationTextBox
            (object sender, TextCompositionEventArgs e)
        {
            regex = new Regex("[^0-9]+");

            if (regex.IsMatch(e.Text) && (e.Text != "-"))
            {
                e.Handled = true;
            }
        }       

        private Client GetClientFromForm()
        {
            Client client = new Client(AssignOrReadId(),
                SurnameTextBox.Text.Trim(),
                NameTextBox.Text.Trim(),
                PatronymicTextBox.Text.Trim(),
                String.Empty,
                PhoneNumberTextBox.Text.Trim(),
                PassportSeriesTextBox.Text.Trim(),
                PassportNumberTextBox.Text.Trim());

            return client;
        }               

        private void WriteHistoryOfChanges(string id)
        {
            List<string> historyOfChanges = new List<string>()
            {
                $"Операция: {operation}",
                $"Дата и время: {DateTime.Now}",
                $"Сотрудник: {Worker.Position}",                
            };

            if (operation == "изменение записи")
            {
                historyOfChanges.Add($"Изменены поля: {changedFields}");                
            }

            historyOfChanges.Add(String.Empty);           

            using (StreamWriter streamWriter = new StreamWriter
                ($"{GenerateHistoryFilePath(id)}", true, Encoding.Unicode))
            {
                foreach (string streamString in historyOfChanges)
                {
                    streamWriter.WriteLine(streamString);
                    HistoryOfChangesListBox.Items.Add(streamString);
                }                
            }
        }        

        private void OwerwriteOrWriteClient()
        {
            Client client = GetClientFromForm();            

            bool status = repository.CheckBeforeReading(ClientFilePath);           

            if (status == true)
            {
                if (Worker.GetType() == typeof(Consultant))
                {
                    changedFields = (Worker as Consultant).OwerwriteClient(client);                    
                }

                if (Worker.GetType() == typeof(Manager))
                {
                    changedFields = (Worker as Manager).OwerwriteClient(client);                    
                }

                if (changedFields == "Идентификатор не найден")
                {
                    (Worker as Manager).WriteClient(client);
                    operation = "создание записи";
                    WriteHistoryOfChanges(client.ClientId);
                }
                else
                {
                    operation = "изменение записи";
                    WriteHistoryOfChanges(client.ClientId);
                }
            }
            else
            {
                (Worker as Manager).WriteClient(client);
                operation = "создание записи";
                WriteHistoryOfChanges(client.ClientId);
            }            
        }        

        private void ClientForm_Loaded(object sender, RoutedEventArgs e)
        {
            ClientIdTextBox.IsReadOnly = true;
            ClientIdTextBox.Background = Brushes.LightGray;            
        }

        private bool CheckRequiredFields()
        {
            bool status = true;

            if (SurnameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Поле \"Фамилия\" не может быть пустым");
                status = false;
            }
            if (NameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Поле \"Имя\" не может быть пустым");
                status = false;
            }
            if (PhoneNumberTextBox.Text.Length == 0)
            {
                MessageBox.Show("Поле \"Номер телефона\" не может быть пустым");
                status = false;
            }

            return status;
        }

        private void Write_Click(object sender, RoutedEventArgs e)
        {
            bool status = CheckRequiredFields();

            if (status == true)
            {
                OwerwriteOrWriteClient();
            }                           
        }      

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool status = CheckRequiredFields();
            
            if (status == true)
            {
                OwerwriteOrWriteClient();
                Close();
            }                       
        }

        private void LoadHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryOfChangesListBox.Items.Clear();

            string historyFilePath = GenerateHistoryFilePath(ClientIdTextBox.Text);

            bool status = repository.CheckBeforeReading(historyFilePath);

            if (status == true)
            {
                using (StreamReader streamReader = new StreamReader
                (historyFilePath, Encoding.Unicode))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string streamString = $"{streamReader.ReadLine()}";
                        HistoryOfChangesListBox.Items.Add(streamString);
                    }
                }
            }                       
        }        
    }
}
