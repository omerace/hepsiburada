using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static modum0.hepsiburada;

namespace modum0
{
    internal class Program
    {
        static readonly string inputRoverFile = @"C:\Users\omera\source\repos\modum0\modum0\inputRoverFile.txt";
        static void Main(string[] args)
        {
            var (xBoundary, yBoundary, roverList) = PrepareInput();
            var result = MarsRover(xBoundary, yBoundary, roverList);
            writeOutput(result);
        }

        public static (int, int, List<Rover>) PrepareInput()
        {
            int x = 0;
            int y = 0;
            var roverList = new List<Rover>();

            if (File.Exists(inputRoverFile))
            {
                using (StreamReader file = new StreamReader(inputRoverFile))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        ln = Regex.Replace(ln, @"[\r\n\t]+", "");
                        Console.WriteLine(ln);
                        if (counter==0)
                        {
                            try
                            {
                                x = int.Parse(ln.Split(" ")[0]);
                                y = int.Parse(ln.Split(" ")[1]);
                            }
                            catch (Exception e)
                            {
                                throw new Exception("Invalid Boundaries. Exception Message: " + e.Message);
                            }
                        }
                        else
                        {
                            var fLine = ln.Split(" ");
                            ln = file.ReadLine();
                            if (ln == null)
                            {
                                throw new Exception("Invalid Movements For Rover: " + counter);
                            }
                            var sLineTrimmed = ln.Trim();
                            sLineTrimmed = Regex.Replace(sLineTrimmed, @"[\r\n\t ]+", "");
                            Console.WriteLine(sLineTrimmed);
                            var sLine = sLineTrimmed.Split(" ");
                            var rover = new Rover();

                            try
                            {
                                var position = new Position()
                                {
                                    X = int.Parse(fLine[0]),
                                    Y = int.Parse(fLine[1])
                                };
                                if (fLine[2].Length > 1 || string.IsNullOrWhiteSpace(fLine[2]))
                                {
                                    throw new Exception("Invalid Face of Position For Rover: " + counter);
                                }
                                position.Face = Char.ToLower(fLine[2][0]);
                                rover.Position = position;
                            }
                            catch (Exception)
                            {
                                throw new Exception("Invalid Positions For Rover: " + counter);
                            }

                            try
                            {
                                var movementString = sLine[0].ToLower();
                                List<char> movements = new List<char>();
                                movements.AddRange(movementString);
                                rover.Movements = movements;
                            }
                            catch (Exception)
                            {
                                throw new Exception("Invalid Movements For Rover: " + counter);
                            }

                            roverList.Add(rover);
                        }
                        counter++;

                    }
                    file.Close();
                    Console.WriteLine($"File has {counter-1} rovers.");
                }
            }
            
            return (x, y, roverList);
        }

        public static void writeOutput(List<Rover> rovers)
        {
            Console.WriteLine($"Final positions of the rovers: ");
            foreach (var item in rovers)
            {
                Console.WriteLine(item.Position.X + " " + item.Position.Y + " " + char.ToUpperInvariant(item.Position.Face));
            }
        }
    }
}
