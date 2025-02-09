using System;
using System.Linq.Expressions;
using Logic.Utils;

namespace Logic.Movies
{
    public class Movie : Entity
    {
        public static Expression<Func<Movie, bool>> IsForChildren =>
            movie => movie.MpaaRating <= MpaaRating.PG;
        public static Expression<Func<Movie, bool>> HasCd =>
            movie => movie.ReleaseDate <= DateTime.Now.AddYears(-8);
        
        public virtual string Name { get; }
        public virtual DateTime ReleaseDate { get; }
        public virtual MpaaRating MpaaRating { get; }
        public virtual string Genre { get; }
        public virtual double Rating { get; }

        
        protected Movie()
        {
        }

        public virtual string ToString()
        {
            return $"{Id} {Name} {Genre} {ReleaseDate} {Rating}";
        }
    }


    public enum MpaaRating
    {
        G = 1,
        PG = 2,
        PG13 = 3,
        R = 4
    }
}
