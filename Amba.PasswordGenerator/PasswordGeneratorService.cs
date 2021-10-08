using System;
using System.Text;

namespace Amba.PasswordGenerator
{
    public class PasswordGeneratorService
    {
        private static readonly char[] Numerics =  "01234567890".ToCharArray();
        private static readonly char[] Letters =  "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] Symbols =  "-_{}@!()[]|'".ToCharArray();

        private Random _random = new Random();
        
        public string Generate(int length, bool addSymbols = true)
        {
            var maxNumbers = length / 2;
            var numberOfNumbers =  maxNumbers > 0 ? _random.Next(1, maxNumbers) : 0;
            
            var maxSymbols = length / 3;
            var numberOfSymbols = addSymbols && maxSymbols > 0 ? _random.Next(1, maxSymbols) : 0;
            
            var numberOfLetters = length - numberOfNumbers - numberOfSymbols;

            var result = GenerateLettersString(numberOfLetters);
            result = AddChars(result, Numerics, numberOfNumbers);
            result = AddChars(result, Symbols, numberOfSymbols);
            
            return result;
        }

        private string GenerateLettersString(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var symbol = Letters[_random.Next(0, Letters.Length)];
                if (_random.Next(0, 2) == 1)
                    symbol = char.ToLowerInvariant(symbol);
                result.Append(symbol);
            }
            return result.ToString();
        }
        
        private string AddChars(string result, char[] symbols, int numberOfSymbols)
        {
            if (symbols.Length == 0 || numberOfSymbols == 0)
                return result;
            
            for (var i = 0; i < numberOfSymbols; i++)
            {
                var position = _random.Next(0, result.Length + 1);
                var symbol = symbols[_random.Next(0, symbols.Length)];
                result = result.Insert(position, symbol.ToString());
            }
            return result;
        }
    }
}