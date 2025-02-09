using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
			var exp = false ? Movie.IsForChildren : x => true;
			exp = true ? Movie.HasCd : x => true;
			return new MovieRepository().GetList(exp);
		}

		private static void BuyCD(long id)
		{
			var m = new MovieRepository().GetOne(id);
			if (m.HasNoValue) return;
			
			var movie = m.Value;
			var hasCd = Movie.HasCd.Compile();
			if (!hasCd(movie))
			{
				Console.WriteLine("No cd available");
				return;
			}
			Console.WriteLine("CD bought");
		}

		private static void BuyAdultTicket(long obj)
		{
			Console.WriteLine("Adult ticket bought");
		}

		private static void BuyChildTicket(long id)
		{
			var m = new MovieRepository().GetOne(id);
			if (m.HasNoValue) return;
			
			var movie = m.Value;
			var isForChildren = Movie.IsForChildren.Compile();
			if (!isForChildren(movie))
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