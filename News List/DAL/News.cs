namespace News_List.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            News_Files = new HashSet<News_Files>();
        }

        public long Id { get; set; }

        public long SourceId { get; set; }

        public DateTime CreateTime { get; set; }

        [Required]
        public string Contents { get; set; }

        public long ThemeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News_Files> News_Files { get; set; }

        public virtual Source Source { get; set; }

        public virtual Theme Theme { get; set; }
    }
}
