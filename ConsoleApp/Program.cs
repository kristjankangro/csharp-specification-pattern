using System;
using System.Collections.Generic;
using Logic.Common;
using Logic.Movies;
using Logic.Utils;
using UI.Common;
using UI.Movies;

namespace Console2
{
	internal class Program
	{
		public static Command<long> BuyAdultTicketCommand { get; set; }
		public static Command<long> BuyChildTicketCommand { get; set; }
		public static Command<long> BuyCDCommand { get; set; }

		public static void Main(string[] args)
		{
			Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=SpecPattern;Trusted_Connection=true;");


			BuyAdultTicketCommand = new Command<long>(BuyAdultTicket);
			BuyChildTicketCommand = new Command<long>(BuyAdultTicket);
			BuyCDCommand = new Command<long>(BuyCD);
			var movieList = new MovieListViewModel();

			// PrintMovieList("kids ", new MovieRepository().GetList(true, 7, false));
			PrintMovieList("all ", GetMovieList());

			BuyCD(1);
			BuyChildTicket(1);
			BuyChildTicket(4);
		}

		private static IReadOnlyList<Movie> GetMovieList()
		{
			// var forKids = new MovieForChildrenSpec();
			// var hasCd = new HasCdSpec();
			// return new MovieRepository().GetList(hasCd.And(forKids.Not()));

			Specification<Movie> spec = Specification<Movie>.All;

			spec.And(new MovieForChildrenSpec())
				.And(new HasCdSpec());
			return new MovieRepository().GetList(spec, 7);
		}

		private static void BuyAdultTicket(long obj)
		{
			Console.WriteLine("Adult ticket bought");
		}

		private static void BuyCD(long id)
		{
			var m = new MovieRepository().GetOne(id);
			if (m.HasNoValue) return;

			var movie = m.Value;

			// var spec = new GenericSpecs<Movie>(Movie.HasCd);
			var hasCdSpec = new HasCdSpec();
			// if (!spec.IsSatisfiedBy(movie))
			if (!hasCdSpec.IsSatisfiedBy(movie))
			{
				Console.WriteLine("No cd available");
				return;
			}

			Console.WriteLine("CD bought");
		}

		private static void BuyChildTicket(long id)
		{
			var m = new MovieRepository().GetOne(id);
			if (m.HasNoValue) return;

			var movie = m.Value;
			// var spec = new GenericSpecs<Movie>(Movie.IsForChildren);
			var movieForChildrenSpec = new MovieForChildrenSpec();
			if (!movieForChildrenSpec.IsSatisfiedBy(movie))
			{
				Console.WriteLine("Child ticket not available");
				return;
			}

			Console.WriteLine("Child ticket bought");
		}

		private static void PrintMovieList(string name, IReadOnlyList<Movie> movies)
		{
			Console.WriteLine(">>> " + name + " movies:");
			foreach (var movie in movies)
			{
				Console.WriteLine(movie.ToString());
			}
		}
	}
}