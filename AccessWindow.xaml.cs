using System.Collections.Generic;
using System.Windows;

namespace DeepDiveIntoOOPPart1
{    
    public partial class AccessWindow : Window
    {
        public AccessWindow()
        {
            InitializeComponent();

            List<Worker> workers = new List<Worker>();

            Consultant consultant = new Consultant();
            workers.Add(consultant);

            Manager manager = new Manager();            
            workers.Add(manager);

            AccessComboBox.ItemsSource = workers;                      
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (AccessComboBox.SelectedItem is Worker worker)
            {
                if (worker.GetType() == typeof(Consultant))
                {
                    ClientList clientList = (worker as Consultant).GetClientList(worker);
                    clientList.Show();
                }

                if (worker.GetType() == typeof(Manager))
                {
                    ClientList clientList = (worker as Manager).GetClientList(worker);
                    clientList.Show();
                }               

                Close();
            }            
        }       
    }
}
