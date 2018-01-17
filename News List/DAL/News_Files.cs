namespace News_List.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News_Files
    {
        public long Id { get; set; }

        public long FileId { get; set; }

        public long NewsId { get; set; }

        public virtual File File { get; set; }

        public virtual News News { get; set; }
    }
}
