﻿namespace FiorellaBackend.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool SoftDeleted { get; set; }


    }
}
