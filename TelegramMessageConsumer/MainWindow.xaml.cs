using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TelegramMessageConsumer.DTO;
using TelegramMessageConsumer.Middleware;
using TelegramMessageConsumer.Models;

namespace TelegramMessageConsumer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DateTime _defaultDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		public MainWindow()
		{
			InitializeComponent();
			SetupWebView();
		}

		public async void SetupWebView()
		{
			await webView.EnsureCoreWebView2Async(null);
			string text = File.ReadAllText("Js/customScript.js");
			await webView.CoreWebView2.ExecuteScriptAsync(text);
			webView.CoreWebView2.WebMessageReceived += WebMessageReceived;
		}

		private async void WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			var commentDTO = JsonConvert.DeserializeObject<CommentDTO>(e.WebMessageAsJson);
			if (commentDTO != null)
			{
				var comment = new Comment
				{
					UserName = commentDTO.UserName,
					IsResponse = commentDTO.IsResponse,
					Content = commentDTO.Content,
					PostedDate = double.TryParse(commentDTO.PostedDate, out double timestamp) ? _defaultDateTime.AddSeconds(timestamp) : _defaultDateTime
				};
				using (var dbContext = new ConsumerContext())
				{
					dbContext.Comments.Add(comment);
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
