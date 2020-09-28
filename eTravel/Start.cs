using System;
using System.Collections.Generic;
using System.Text;

namespace eTravel
{
    class Start
    {
        List<PassengerData> pData { get; set; }

        public Start()
        {
            DataReader reader = new DataReader();
            pData = reader.GetData();
        }
    }
}
