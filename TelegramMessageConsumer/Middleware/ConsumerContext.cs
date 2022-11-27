using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramMessageConsumer.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TelegramMessageConsumer.Middleware
{
	public class ConsumerContext : DbContext
	{
		public DbSet<Comment> Comments { get; set; }

		public ConsumerContext()
		{
			//Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=Data\\appData.dat");
		}
	}
}
