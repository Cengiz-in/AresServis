using API.Models.Exceptions;

namespace API.Extensions
{
    public static class InputExtensions
    {
        public static string FirstCharToUpper(this string input) =>
    input switch
    {
        null => throw new ArgumentNullException(nameof(input)),
        "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
        _ => input.First().ToString().ToUpper() + input.Substring(1)
    };
        public static string CheckGender(this string input) => input switch {
            "" => throw new ArgumentNullException(nameof(input)),
            null => throw new ArgumentNullException(nameof(input)),
            _ => input.ToLower() == "erkek" ? "Erkek" : input.ToLower() == "kadın" ? "Kadın" : throw new CustomException("cinsiyet seçimi geçersiz. [Erkek,Kadın] olmalı.")
        };

        public static string GenerateUsername(this string text)
        {
            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };
            text = text.ToLower();
            // Match chars
            for (int i = 0; i < turkishChars.Length; i++)
                text = text.Replace(turkishChars[i], englishChars[i]);

            return text.ToLower();
        }
    }
}