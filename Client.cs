namespace DeepDiveIntoOOPPart1
{
    internal class Client
    {
        private string clientId;
        private string clientName;

        public string ClientId { get { return this.clientId; } }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string ClientName { get { return this.clientName; } }
        public string PhoneNumber { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }

        /// <summary>
        /// Карточка клиента
        /// </summary>
        /// <param name="ClientId">Идентификатор клиента</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Name">Имя</param>
        /// <param name="Patronymic">Отчество</param>
        /// <param name="ClientName">Наименование клиента (Ф.И.O)</param>
        /// <param name="PhoneNumber">Номер телефона</param>
        /// <param name="PassportSeries">Серия паспорта</param>
        /// <param name="PassportNumber">Номер паспорта</param>          
        public Client(string ClientId, string Surname, string Name, string Patronymic,
            string ClientName, string PhoneNumber, string PassportSeries, string PassportNumber)
        {
            clientId = ClientId;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            clientName = ClientName;
            this.PhoneNumber = PhoneNumber;
            this.PassportSeries = PassportSeries;
            this.PassportNumber = PassportNumber;

            if (Patronymic.Length == 0)
            {
                this.clientName = $"{this.Surname} {this.Name}";
            }
            else
            {
                this.clientName = $"{this.Surname} {this.Name} {this.Patronymic}";
            }
        }                
    }
}
