using System;
using System.Linq;
using System.Linq.Expressions;

namespace Logic.Common
{

	internal sealed class InitSpecification<T> : Specification<T>
	{
		public override Expression<Func<T, bool>> ToExpression()
		{
			return x => true;
		}
	}
	public abstract class Specification<T>
	{
		
		public static readonly Specification<T> All = new InitSpecification<T>();
		public bool IsSatisfiedBy(T entity)
		{
			Func<T, bool> predicate = ToExpression().Compile();
			return predicate(entity);
		}

		public Specification<T> And(Specification<T> other)
		{
			if(this == All) return other;
			
			if(other == All) return this;
			
			return new AndSpec<T>(this, other);
		}
		public Specification<T> Or(Specification<T> other)
		{
			if (this == All || other == All)
			{
				return All;
			}
			return new OrSpec<T>(this, other);
		}
		
		public Specification<T> Not()
		{
			return new NotSpec<T>(this);
		}
		
		public abstract Expression<Func<T, bool>> ToExpression();
	}

	internal sealed class AndSpec<T> : Specification<T>
	{
		private readonly Specification<T> _left;
		private readonly Specification<T> _right;

		public AndSpec(Specification<T> left, Specification<T> right)
		{
			_left = left;
			_right = right;
		}

		public override Expression<Func<T, bool>> ToExpression()
		{
			var left = _left.ToExpression();
			var right = _right.ToExpression();
			
			BinaryExpression andExpression = Expression.AndAlso(left.Body, right.Body);
			
			return Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters.Single());
		}
	}
	
	internal sealed class OrSpec<T> : Specification<T>
	{
		private readonly Specification<T> _left;
		private readonly Specification<T> _right;

		public OrSpec(Specification<T> left, Specification<T> right)
		{
			_left = left;
			_right = right;
		}

		public override Expression<Func<T, bool>> ToExpression()
		{
			var left = _left.ToExpression();
			var right = _right.ToExpression();
			
			BinaryExpression andExpression = Expression.OrElse(left.Body, right.Body);
			
			return Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters.Single());
		}
	}
	
	internal sealed class NotSpec<T> : Specification<T>
	{
		private readonly Specification<T> _left;

		public NotSpec(Specification<T> left)
		{
			_left = left;
		}

		public override Expression<Func<T, bool>> ToExpression()
		{
			var left = _left.ToExpression();
			
			var expression = Expression.Not(left.Body);
			
			return Expression.Lambda<Func<T, bool>>(expression, left.Parameters.Single());
		}
	}
}