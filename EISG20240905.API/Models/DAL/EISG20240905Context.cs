using Microsoft.EntityFrameworkCore;
using EISG20240905.API.Models.EN;

namespace EISG20240905.API.Models.DAL
{
	public class EISG20240905Context : DbContext
	{
		public EISG20240905Context(DbContextOptions<EISG20240905Context> options) : base(options)
		{
		}

		public DbSet<ProductEISG> ProductsEISG { get; set; }
	}
}
