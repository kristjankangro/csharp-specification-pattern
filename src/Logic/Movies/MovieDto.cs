using System;

namespace Logic.Movies
{
	public class MovieDto
	{
		public long Id { get; set; }
		public virtual string Name { get; set;}
		public virtual DateTime ReleaseDate { get; set;}
		public virtual MpaaRating MpaaRating { get; set;}
		public virtual string Genre { get; set;}
		public virtual double Rating { get; set;}
		public virtual string Director { get; set;}
		public virtual string ToString()
		{
			return $"{Id} {Name} {Genre} {ReleaseDate} {Rating} {Director}";
		}
		
	}
}