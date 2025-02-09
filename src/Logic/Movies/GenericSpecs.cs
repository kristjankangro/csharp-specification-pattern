using System;
using System.Linq.Expressions;

namespace UI.Movies
{
	public class GenericSpecs<T>
	{
		public Expression<Func<T,bool>> Expression { get; }
		public GenericSpecs(Expression<Func<T, bool>> exp)
		{
			Expression = exp;
		}

		public bool IsSatisfiedBy(T entity)
		{
			return Expression.Compile().Invoke(entity);
		}
	}
}