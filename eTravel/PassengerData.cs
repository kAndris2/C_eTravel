using System;
using System.Collections.Generic;
using System.Text;

namespace eTravel
{
    class PassengerData
    {
        int StopNumber { get; set; }
        DateTime Date { get; set; }
        int CardId { get; set; }
        String Type { get; set; }
        int Ticket { get; set; }
        DateTime Validity { get; set; }

        public PassengerData(int stop, DateTime date, int card_id, string type, int ticket)
        {
            StopNumber = stop;
            Date = date;
            CardId = card_id;
            Type = type;
            Ticket = ticket;
        }

        public PassengerData(int stop, DateTime date, int card_id, string type, DateTime validity)
        {
            StopNumber = stop;
            Date = date;
            CardId = card_id;
            Type = type;
            Validity = validity;
        }
    }
}
