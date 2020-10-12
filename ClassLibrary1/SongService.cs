using SongBook.Data;
using SongBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SongService
    {
        private readonly Guid _userId;

        public SongService (Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSong(SongCreate model)
        {
            var entity =
                new Song()
                {
                    UserId = _userId,
                    BandID = model.BandID,
                    ShowId = model.ShowId,
                    Name = model.Name,
                    Length = model.Length


                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Songs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SongListItem> GetSongs()
        {
         
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Songs
                        .Where(e => e.UserId == _userId)
                        .Select(
                        e =>
                            new SongListItem
                            {
                                SongId = e.SongId,
                                BandID = e.BandID,
                                ShowId = e.ShowId,
                                Name = e.Name,
                                Length = e.Length
                            });
               
                        return query.ToArray();
            }
        }

        public SongDetail GetSongById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Songs
                    .Single(e => e.SongId == id && e.UserId == _userId);
                return
                    new SongDetail
                    {
                        SongId = entity.SongId,
                        BandID = entity.BandID,
                        ShowId = entity.ShowId,
                        Name = entity.Name,
                        Length = entity.Length
                    };
            }
        }

        public bool UpdateSong(SongEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Songs
                    .Single(e => e.SongId == model.SongId && e.UserId == _userId);

                //entity.SongId = model.SongId;
                entity.BandID = model.BandID;
                entity.ShowId = model.ShowId;
                entity.Name = model.Name;
                entity.Length = model.Length;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSong(int songId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == songId && e.UserId == _userId);

                ctx.Songs.Remove(entity);

                return ctx.SaveChanges() == 1;
                        
            }
        }
    }
}
