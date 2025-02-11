using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Logic.Common;
using Logic.Utils;
using NHibernate;
using NHibernate.Linq;

namespace Logic.Movies
{
    public class MovieRepository
    {
        public Maybe<Movie> GetOne(long id)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Get<Movie>(id);
            }
        }

        // public IReadOnlyList<Movie> GetList(GenericSpecs<Movie> spec)
        public IReadOnlyList<MovieDto> GetList(
            Specification<Movie> spec,
            double minRating,
            int page = 0,
            int pageSize = 10
            )
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Query<Movie>()
                    .Where(spec.ToExpression())
                    .Where(x => x.Rating >= minRating)
                    .Skip(page*pageSize)
                    .Take(pageSize)
                    .Fetch(x => x.Director)
                    .ToList()
                    .Select(x=> new MovieDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ReleaseDate = x.ReleaseDate,
                        MpaaRating = x.MpaaRating,
                        Genre = x.Genre,
                        Rating = x.Rating,
                        Director = x.Director.Name,
                    })
                    .ToList();
            }
        } 
        
        //anti pattern, never return IQueryable, IEnumerable
        public IQueryable<Movie> Find()
        {
            ISession session = SessionFactory.OpenSession();
                return session.Query<Movie>();
        }
    }
}
