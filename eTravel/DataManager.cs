﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;

namespace eTravel
{
    class DataManager
    {
        const String READ = "utasadat.txt";
        const String WRITE = "figyelmeztetes.txt";

        public void Write(string[] lines)
        {
            File.WriteAllLines(WRITE, lines);
        }

        public List<PassengerData> GetData()
        {
            List<PassengerData> data = new List<PassengerData>();
            foreach (string line in Read(READ))
            {
                string[] lineInfo = line.Split(" ");
                bool check = lineInfo[4].Length < 8;

                data.Add
                    (
                        new PassengerData
                        (
                            Int32.Parse(lineInfo[0]),
                            DateTime.ParseExact(lineInfo[1].Split("-")[0], "yyyyMMdd", CultureInfo.InvariantCulture),
                            Int32.Parse(lineInfo[2]),
                            lineInfo[3],
                            check == true ? int.Parse(lineInfo[4]) : (int?) null,
                            check == false ? DateTime.ParseExact(lineInfo[4], "yyyyMMdd", CultureInfo.InvariantCulture) : (DateTime?) null
                        )
                    );
            }
            return data;
        }

        string[] Read(string filename)
        {
            string[] data = new string[0];
            try
            {
                data = File.ReadAllLines(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return data;
        }
    }
}
