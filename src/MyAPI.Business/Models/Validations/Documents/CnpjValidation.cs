using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAPI.Business.Models.Validations.Documents
{
    class CnpjValidation
    {
        public const int CnpjSize = 14;

        public static bool Validate(string cnpj)
        {
            var cnpjNumbers = new string(cnpj.Where(Char.IsDigit).ToArray());

            if (!ValidSize(cnpjNumbers)) return false;
            return !HasRepeatedNumbers(cnpjNumbers) && HasValidNumbers(cnpjNumbers);
        }

        private static bool ValidSize(string value)
        {
            return value.Length == CnpjSize;
        }

        private static bool HasRepeatedNumbers(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidNumbers(string value)
        {
            var number = value.Substring(0, CnpjSize - 2);

            var numberVerifier = new NumberVerifier(number)
                .WithMultipliers(2, 9)
                .Swap("0", 10, 11);
            var firstNumber = numberVerifier.CalculateNumber();
            numberVerifier.AddNumber(firstNumber);
            var secondNumber = numberVerifier.CalculateNumber();

            return string.Concat(firstNumber, secondNumber) == value.Substring(CnpjSize - 2, 2);
        }
    }
}
