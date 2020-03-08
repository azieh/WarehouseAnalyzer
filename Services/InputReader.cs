using System;
using System.Collections.Generic;
using Common.Interfaces;

namespace Services
{
    public class InputReader : IInputReader
    {
        private readonly string _skipLineSymbol = "#";

        public List<string> ReadInputData()
        {
            var inputData = new List<string>();
            while (true)
            {
                var input = Console.ReadLine();

                if (input != null && input.Contains(_skipLineSymbol))
                    continue;
                if (string.IsNullOrEmpty(input))
                    break;

                inputData.Add(input);
            }
            return inputData;
        }
    }
}