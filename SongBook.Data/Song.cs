using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Data
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Band")]
        [Display(Name = "Band Name")]
        public int BandID { get; set; }
        [ForeignKey("Show")]
        public int ShowId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Length { get; set; }

        public virtual Band Band { get; set; }
        public virtual Show Show { get; set; }
    }
}
