using SongBook.Data;
using SongBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Services
{
    public class BandService
    {
        private readonly Guid _userId;
        public BandService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBand(BandCreate model)
        {
            var entity =
                new Band()
                {
                    UserId = _userId,
                    Name = model.Name,
                    Members = model.Members,
                    YearsActive = model.YearsActive,
                    NumberOfAlbums = model.NumberOfAlbums
                };
            using(var ctx = new ApplicationDbContext())
            {
                ctx.Bands.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BandListItem> GetBands()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Bands
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                            new BandListItem
                            {
                                BandId = e.BandId,
                                Name = e.Name,
                                Members = e.Members,
                                YearsActive = e.YearsActive,
                                NumberOfAlbums = e.NumberOfAlbums
                            });
                return query.ToArray();
            }
        }

        public BandDetail GetBandById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bands
                        .Single(e => e.BandId == id && e.UserId == _userId);
                return
                    new BandDetail
                    {
                        BandId = entity.BandId,
                        Name = entity.Name,
                        Members = entity.Members,
                        YearsActive = entity.YearsActive,
                        NumberOfAlbums = entity.NumberOfAlbums,
                    };
            }
        }

        public bool UpdateBand(BandEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bands
                    .Single(e => e.BandId == model.BandId && e.UserId == _userId);

                entity.Name = model.Name;
                entity.Members = model.Members;
                entity.YearsActive = model.YearsActive;
                entity.NumberOfAlbums = model.NumberOfAlbums;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBand(int bandId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bands
                        .Single(e => e.BandId == bandId && e.UserId == _userId);
                ctx.Bands.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


            

    }
}
