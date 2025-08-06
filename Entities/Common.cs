using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    public class PrimaryKey
    {
        [Key, Column("id")]
        public long Id { get; set; }
    }

    public class IdWithDateColumns : PrimaryKey
    {
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }

    public class IdWithCreatedDate : PrimaryKey
    {
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    public class IdAndDatesWithIsActive : IdWithDateColumns
    {
        [Column("is_active")]
        public bool IsActive { get; set; } = true;
    }

    public class IdAndDatesWithIsDeleted : IdWithDateColumns
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }

    public class IdAndDatesWithIsActiveAndIsDeleted : IdAndDatesWithIsActive
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
