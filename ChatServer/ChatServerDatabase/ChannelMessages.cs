namespace ChatServerDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChannelMessages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserFrom { get; set; }

        public int Channel { get; set; }

        [StringLength(300)]
        public string Message { get; set; }

        [StringLength(300)]
        public string FilePath { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; }

        public virtual Channels Channels { get; set; }

        public virtual Users Users { get; set; }
    }
}
