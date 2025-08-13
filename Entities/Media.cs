using System.ComponentModel.DataAnnotations.Schema;

namespace StansAssociates_Backend.Entities
{
    [Table("media")]
    public class Media : IdWithCreatedDate
    {
        [Column("file_name")]
        public string FileName { get; set; }

        [Column("content_type")]
        public string ContentType { get; set; }

        [Column("size_bytes")]
        public long SizeBytes { get; set; }

        [Column("data")]
        public byte[] Data { get; set; }

        //[InverseProperty(nameof(User.Media))]
        //public User? User { get; set; }

        //[InverseProperty(nameof(Student.Media))]
        //public Student? Student { get; set; }
    }
}
