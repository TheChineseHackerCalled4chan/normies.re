using System;

namespace NormiesRe.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Post Post { get; set; }
        public string Country { get; set; }
    }
}
