using Microsoft.AspNet.Identity;
using SongBook.Data;
using SongBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Services
{
    public class ShowService
    {
        private readonly Guid _userId;

        public ShowService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateShow(ShowCreate model)
        {
            var entity =
                new Show()
                {
                    UserId = _userId,
                    BandId = model.BandId,
                    Venue = model.Venue,
                    Location = model.Location,
                    Date = model.Date
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Shows.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ShowListItem> GetShows()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Shows
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new ShowListItem
                                {
                                    ShowId = e.ShowId,
                                    BandId = e.BandId,
                                    Venue = e.Venue,
                                    Location = e.Location,
                                    Date = e.Date

                                });

                return query.ToArray();
            }
        }

        public ShowDetail GetShowById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Shows
                        .Single(e => e.ShowId == id && e.UserId == _userId);
                return
                    new ShowDetail
                    {
                        ShowId = entity.ShowId,
                        BandId = entity.Band.BandId,
                        Venue = entity.Venue,
                        Location = entity.Location,
                        Date = entity.Date

                    };
            }
        }

        public bool UpdateShow(ShowEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Shows
                        .Single(e => e.ShowId == model.ShowId && e.UserId == _userId);

                entity.BandId = model.BandId;
                entity.Venue = model.Venue;
                entity.Location = model.Location;
                entity.Date = model.Date;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteShow(int showId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Shows
                        .Single(e => e.ShowId == showId && e.UserId == _userId);
                ctx.Shows.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
