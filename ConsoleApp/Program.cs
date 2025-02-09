using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Logic.Movies;
using Logic.Utils;
using UI.Movies;

namespace Console2
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			
			Initer.Init(@"Server=(localdb)\MSSQLLocalDB;Database=SpecPattern;Trusted_Connection=true;");
			
			var movieList = new MovieListViewModel();

			PrintMovieList("kids: ", new MovieRepository().GetList(true, 10, false));
			PrintMovieList("all: ", new MovieRepository().GetList(false, 10, false));
			

			Console.WriteLine("Hello, World!");
		}

		private static void PrintMovieList(string name, IReadOnlyList<Movie> movies)
		{
			Console.WriteLine(name+ " movies:");
			foreach (var movie in movies)
			{
				Console.WriteLine($"{movie.Name} {movie.Genre} {movie.ReleaseDate} {movie.Rating}");
			}
		}
	}
}