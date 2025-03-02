namespace Application.Utils
{
    public static class VerificationCodeGenerator
    {
        private static readonly Random _random = new();

        public static string GenerateCode(int length = 6)
        {
            if (length < 4 || length > 10)
                throw new ArgumentOutOfRangeException(nameof(length), "Code length must be between 4 and 10 digits.");

            var min = (int)Math.Pow(10, length - 1); 
            var max = (int)Math.Pow(10, length) - 1; 

            return _random.Next(min, max).ToString();
        }
    }
}
