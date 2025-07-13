using System.Text.RegularExpressions;

namespace EduOMO.Base;

public static class CommonHelper
{
    public static string StripHtmlTags(this string html)
    {
        return Regex.Replace(html, "<.*?>", string.Empty);
    }
}
