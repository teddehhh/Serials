using System;

namespace Serials.models
{
    internal class Comment
    {
        public int CommentID { get; set; }
        public int SerialID { get; set; }
        public int VisitorID { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
    }
}
