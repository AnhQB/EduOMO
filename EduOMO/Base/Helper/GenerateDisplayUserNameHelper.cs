namespace EduOMO.Base;

public static class GenerateDisplayUserNameHelper
{
    private static readonly string[] FirstNames = new[]
    {
        "An", "Bình", "Châu", "Dũng", "Dương", "Giang", "Hải", "Hiếu", "Hương", "Khoa",
        "Lan", "Linh", "Mai", "Minh", "Nam", "Ngọc", "Phong", "Quân", "Trang", "Tuấn"
    };

    private static readonly string[] LastNames = new[]
    {
        "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Đặng", "Bùi",
        "Đỗ", "Hồ", "Ngô", "Dương", "Lý", "Võ", "Đinh", "Trịnh", "Hà", "Tô"
    };

    public static string GenerateOneUsername()
    {
        var random = new Random();
        string first = FirstNames[random.Next(FirstNames.Length)];
        string last = LastNames[random.Next(LastNames.Length)];
        return $"{first}+{last}";
    }
}
