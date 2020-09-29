using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace eTravel
{
    class Start
    {
        List<PassengerData> pData { get; set; }
        DataManager dManager = new DataManager();

        public Start()
        {
            pData = dManager.GetData();
        }

        public void Do()
        {
            KeyValuePair<int, int> busStop = GetBusStopOfMostTravellers();
            string date1 = "2020/09/29",
                   date2 = "2020/10/01";

            Console.WriteLine
            (
                $"2nd TASK: {CountPassengers()} passengers wanted to get on the bus.\n" +
                $"3rd TASK: Number of rejected passengers: {CountCanceledPassengers()}\n" +
                $"4th TASK: Most passengers boarded at stop {busStop.Key} ({busStop.Value} passengers).\n" +
                $"5th TASK: {GetResultOfTaskFive()}\n" +
                $"6th TASK: There are {CountDaysBetweenTwoDates(DateTime.ParseExact(date1, "yyyy/MM/dd", CultureInfo.InvariantCulture),DateTime.ParseExact(date2, "yyyy/MM/dd", CultureInfo.InvariantCulture))} day(s) in between {date1} and {date2}\n" +
                $"7th TASK: {GetPassengersWhosePassWillExpireInThreeDays()} passengers pass expire within 3 days."
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

        Dictionary<string, int> CountDiscountAndFreeTravels()
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
            Dictionary<string, int> count = CountDiscountAndFreeTravels();
            string[] result = new string[count.Count];
            int i = 0;

            foreach (KeyValuePair<string, int> item in count)
            {
                result[i] = $"{item.Key}({item.Value})";
                i++;
            }

            return string.Join(", ", result);
        }

        int CountDaysBetweenTwoDates(DateTime date1, DateTime date2)
        {
            int y1, m1, r1,
                y2, m2, r2;

            m1 = (date1.Month + 9) % 12;
            y1 = date1.Year - m1 / 10;
            r1 = 365 * y1 + y1 / 4 - y1 / 100 + y1 / 400 + (m1 * 306 + 5) / 10 + date1.Day - 1;

            m2 = (date2.Month + 9) % 12;
            y2 = date2.Year - m2 / 10;
            r2 = 365 * y2 + y2 / 4 - y2 / 100 + y2 / 400 + (m2 * 306 + 5) / 10 + date2.Day - 1;
            return r2 - r1;
        }

        int GetPassengersWhosePassWillExpireInThreeDays()
        {
            List<string> data = new List<string>();

            foreach (PassengerData pd in pData)
            {
                if (pd.Validity.HasValue)
                {
                    if (CountDaysBetweenTwoDates(pd.BoardingDate, (DateTime)pd.Validity) == 3)
                        data.Add($"{pd.CardId} {pd.Validity}");
                }
            }
            dManager.Write(data.ToArray());
            return data.Count;
        }
    }
}
