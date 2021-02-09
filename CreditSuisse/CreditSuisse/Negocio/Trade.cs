using CreditSuisse.Interfaces;
using CreditSuisse.Negocio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditSuisse.Negocio
{
    public class Trade : ITrade
    {
        private const string PRIVATE = "Private";
        private const string PUBLIC = "Public";
        private const int VALOR_REFERENCIA = 1000000;
        private const int LIMITE_DIAS_ATRASO = 30;

        public Trade(double value, string clientSector, DateTime nextPaymentDate, bool isPoliticallyExposed = false)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
            IsPoliticallyExposed = isPoliticallyExposed;
        }

        public double Value { get; private set; }
        public string ClientSector { get; private set; }
        public DateTime NextPaymentDate { get; private set; }

        public bool IsPoliticallyExposed { get; private set; }

        private string GetCategory(DateTime dateRef)
        {
            if (IsPoliticallyExposed) return "PEP";

            if (Value > VALOR_REFERENCIA && PRIVATE == ClientSector)
            {
                return "HIGHRISK";
            }

            else if (dateRef.Subtract(NextPaymentDate).TotalDays > LIMITE_DIAS_ATRASO)
            {
                return "DEFAUTED";
            }

            else if (Value > VALOR_REFERENCIA && PUBLIC == ClientSector)
            {
                return "MEDIUMRISK";
            }

            return null;
        }

        public void PrintTrade()
        {
            Console.Write($"{this.Value} {this.ClientSector} {this.NextPaymentDate.ToString("MM/dd/yyyy")}");
        }

        public void PrintCategory(DateTime dateRef)
        {
            Console.WriteLine(GetCategory(dateRef));
        }
    }
}
