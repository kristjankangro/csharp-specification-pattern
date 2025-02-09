using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Logic.Utils;
using NHibernate;
using NHibernate.Linq;
using UI.Movies;

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

        public IReadOnlyList<Movie> GetList(GenericSpecs<Movie> spec)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Query<Movie>()
                    .Where(spec.Expression) 
                    .ToList();
            }
        }
    }
}
