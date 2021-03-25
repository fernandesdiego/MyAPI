using System.Collections.Generic;

namespace MyAPI.Business.Models.Validations.Documents
{
    public class NumberVerifier
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _swaps = new Dictionary<int, string>();
        private bool _complementModule = true;

        public NumberVerifier(string numero)
        {
            _number = numero;
        }

        public NumberVerifier WithMultipliers(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();
            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);

            return this;
        }

        public NumberVerifier Swap(string substitute, params int[] numbers)
        {
            foreach (var i in numbers)
            {
                _swaps[i] = substitute;
            }
            return this;
        }

        public void AddNumber(string number)
        {
            _number = string.Concat(_number, number);
        }

        public string CalculateNumber()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var soma = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                soma += produto;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (soma % Module);
            var result = _complementModule ? Module - mod : mod;

            return _swaps.ContainsKey(result) ? _swaps[result] : result.ToString();
        }
    }

}
