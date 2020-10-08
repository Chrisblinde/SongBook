using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Models
{
    public class BandEdit
    {
        public int BandId { get; set; }
        public string Name { get; set; }
        public string Members { get; set; }
        public string YearsActive { get; set; }
        public int NumberOfAlbums { get; set; }
    }
}
