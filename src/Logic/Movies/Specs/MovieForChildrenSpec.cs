using System;
using System.Linq.Expressions;
using Logic.Common;

namespace Logic.Movies.Specs
{
	public sealed class MovieForChildrenSpec : Specification<Movie>{
		public override Expression<Func<Movie, bool>> ToExpression()
		{
			return movie => movie.MpaaRating <= MpaaRating.PG;
		}
	}
}