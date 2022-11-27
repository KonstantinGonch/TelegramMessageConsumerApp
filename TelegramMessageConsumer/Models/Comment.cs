using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramMessageConsumer.Models
{
	public  class Comment
	{
		public long Id { get; set; }
		public string? UserName { get; set; }
		public string? Content { get; set; }
		public DateTime? PostedDate { get; set; }
		public bool IsResponse { get; set; }
	}
}
