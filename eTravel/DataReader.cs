using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace eTravel
{
    class DataReader
    {
        const String FILENAME = "utasadat.txt";

        public List<PassengerData> GetData()
        {
            List<PassengerData> data = new List<PassengerData>();
            foreach (string line in Read(FILENAME))
            {
                string[] lineInfo = line.Split(" ");
                data.Add
                (
                    new PassengerData
                    (
                        Int32.Parse(lineInfo[0]),
                        DateTime.ParseExact(lineInfo[1], "yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                        Int32.Parse(lineInfo[2]),
                        lineInfo[3],
                        Int32.Parse(lineInfo[4])
                    )
                );
            }
            return data;
        }

        string[] Read(string filename)
        {
            return System.IO.File.ReadAllLines(filename);
        }
    }
}
