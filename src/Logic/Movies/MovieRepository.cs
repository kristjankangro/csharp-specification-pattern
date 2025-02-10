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
        public IReadOnlyList<Movie> GetList(Specification<Movie> spec)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                return session.Query<Movie>()
                    .Where(spec.ToExpression()) 
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
