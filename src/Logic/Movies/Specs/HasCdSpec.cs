using System;
using System.Linq.Expressions;
using Logic.Common;

namespace Logic.Movies.Specs
{
	public sealed class HasCdSpec : Specification<Movie>{
		private readonly int MonthBeforeCD;
		public override Expression<Func<Movie, bool>> ToExpression()
		{
			return movie => movie.ReleaseDate <= DateTime.Now.AddYears(-MonthBeforeCD);
		}
	}
}