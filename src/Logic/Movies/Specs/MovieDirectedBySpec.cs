using System;
using System.Linq.Expressions;
using Logic.Common;

namespace Logic.Movies.Specs
{
	public sealed class MovieDirectedBySpec : Specification<Movie>{
		private readonly string _name;

		public MovieDirectedBySpec(string name)
		{
			_name = name;
		}

		public override Expression<Func<Movie, bool>> ToExpression()
		{
			return movie => movie.Director.Name == _name;
		}
	}
}