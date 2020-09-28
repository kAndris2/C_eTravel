using System;
using System.Collections.Generic;
using System.Text;

namespace eTravel
{
    class PassengerData
    {
        public int StopNumber { get; set; }
        public DateTime BoardingDate { get; set; }
        public int CardId { get; set; }
        public String Type { get; set; }
        public int? Ticket { get; set; }
        public DateTime? Validity { get; set; }

        public PassengerData(int stop, DateTime date, int card_id, string type, int? ticket, DateTime? validity)
        {
            StopNumber = stop;
            BoardingDate = date;
            CardId = card_id;
            Type = type;
            Ticket = ticket;
            Validity = validity;
        }
    }
}
