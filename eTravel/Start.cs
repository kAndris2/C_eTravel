using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Do()
        {
            KeyValuePair<int, int> busStop = GetBusStopOfMostTravellers();
            Console.WriteLine
            (
                $"2nd TASK: {CountPassengers()}pc\n" +
                $"3rd TASK: {CountCanceledPassengers()}\n" +
                $"4th TASK: {busStop.Key}({busStop.Value})\n"
            );
        }

        int CountPassengers()
        {
            return pData.Count;
        }

        int CountCanceledPassengers()
        {
            int count = 0;
            
            foreach (PassengerData pd in pData)
            {
                if (pd.Ticket == 0)
                    count++;
                else if (pd.Validity.HasValue)
                {
                    if (pd.BoardingDate.Ticks > pd.Validity.Value.Ticks)
                        count++;
                }
            }

            return count;
        }

        KeyValuePair<int, int> GetBusStopOfMostTravellers()
        {
            Dictionary<int, int> busStops = new Dictionary<int, int>();

            foreach (PassengerData pd in pData)
            {
                if (!busStops.ContainsKey(pd.StopNumber))
                    busStops.Add(pd.StopNumber, 1);
                else
                    busStops[pd.StopNumber]++;
            }

            return busStops.Aggregate((x, y) => x.Value >= y.Value ? x : y);
        }
    }
}
