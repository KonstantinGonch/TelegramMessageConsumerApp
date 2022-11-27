using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramMessageConsumer.DTO
{
	public class CommentDTO
	{
		public string UserName { get; set; }
		public string Content { get; set; }
		public string PostedDate { get; set; }
		public bool IsResponse { get; set; }
	}
}
