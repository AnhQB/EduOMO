using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnidecodeSharpFork;

namespace EduOMO.Base;

public static class GenerateSlugHelper
{
    public static string GenerateSlug(string phrase)
    {
        string salt = GenerateSalt();

        string str = RemoveAccent(phrase).ToLower();
        str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // loại bỏ các ký tự đặc biệt, chỉ giữ lại các ký tự chữ cái, số, dấu cách và dấu gạch ngang
        str = Regex.Replace(str, @"\s+", " ").Trim(); // loại bỏ các khoảng trắng thừa
        str = Regex.Replace(str, @"\s", "-"); // thay thế khoảng trắng bằng dấu gạch ngang
        str += $"-{salt}"; // thêm salt vào cuối slug
        return str;
    }
    public static string GenerateSalt(int byteLength = 12)
    {
        var bytes = RandomNumberGenerator.GetBytes(byteLength);
        return Convert.ToBase64String(bytes);
    }

    public static string RemoveAccent(string text)
    {
        /*Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
        return Encoding.ASCII.GetString(bytes);*/
        return Unidecoder.Unidecode(text);
    }
}
