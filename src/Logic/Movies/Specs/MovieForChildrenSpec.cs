using System;
using System.Linq.Expressions;
using Logic.Movies;

namespace Logic.Common
{
	public sealed class MovieForChildrenSpec : Specification<Movie>{
		public override Expression<Func<Movie, bool>> ToExpression()
		{
			return movie => movie.MpaaRating <= MpaaRating.PG;
		}
	}
}