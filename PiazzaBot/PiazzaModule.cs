using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace PiazzaBot
{
	public class PiazzaModule
	{
		private const string piazzaUrl = "https://piazza.com/class/kmwk4jtmkrj56j";
		private readonly Regex piazzaPostRegex = new( @"(^|\s)p([0-9]+)", RegexOptions.IgnoreCase );
		public PiazzaModule( DiscordSocketClient discordClient )
		{
			discordClient.MessageReceived += MessageReceivedAsync;
		}

		public async Task MessageReceivedAsync( SocketMessage message )
		{
			MatchCollection piazzaPostMatches = piazzaPostRegex.Matches( message.Content );

			foreach ( Match match in piazzaPostMatches )
			{
				// TO-DO: Create a Piazza API that will pull the title and description.
				string postNumber = match.Groups[ 2 ].Value;
				EmbedBuilder embedBuilder = new EmbedBuilder( );
				embedBuilder.WithTitle( "Piazza Post @" + postNumber );
				embedBuilder.WithUrl( piazzaUrl + "?cid=" + postNumber );
				embedBuilder.WithDescription( "Here is the post you requested." );
				embedBuilder.WithColor( Color.Orange );
				await message.Channel.SendMessageAsync( "", false, embedBuilder.Build(), null, null, new MessageReference( message.Id ) );
			}
		}
	}
}
