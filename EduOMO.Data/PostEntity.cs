using EduOMO.Data.Base;
using System.Reflection.Metadata;

namespace MMOEdu.Data
{
    public class PostEntity : FullTrackEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Keyword { get; set; }
        public string? Document { get; set; }
    }
}
