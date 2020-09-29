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
                $"4th TASK: {busStop.Key}({busStop.Value})\n" +
                $"5th TASK: {GetResultOfTaskFive()}"
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
                else if (!CheckValidity(pd))
                    count++;
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

        bool CheckValidity(PassengerData data)
        {
            if (data.Validity.HasValue)
            {
                if (data.BoardingDate.Ticks > data.Validity.Value.Ticks)
                    return false;
            }
            return true;
        }

        Dictionary<string, int> Count()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();
            count.Add("Discount", 0);
            count.Add("Free", 0);

            string[] discount = new string[] { "TAB", "NYB" };
            string[] free = new string[] { "NYP", "RVS", "GYK" };

            foreach(PassengerData pd in pData)
            {
                if (CheckValidity(pd))
                {
                    if (discount.Contains(pd.Type))
                        count["Discount"]++;
                    else if (free.Contains(pd.Type))
                        count["Free"]++;
                }
            }
            return count;
        }

        String GetResultOfTaskFive()
        {
            Dictionary<string, int> count = Count();
            string[] result = new string[count.Count];
            int i = 0;

            foreach (KeyValuePair<string, int> item in count)
            {
                result[i] = $"{item.Key}({item.Value})";
                i++;
            }

            return string.Join(", ", result);
        }
    }
}
