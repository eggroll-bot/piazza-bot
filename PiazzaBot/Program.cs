using System;
using System.IO;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace PiazzaBot
{
	class Program
	{
		private readonly string botToken = File.ReadAllText( "bot_token.txt" );
		private DiscordSocketClient discordClient;

		private static void Main( )
		{
			new Program( ).MainAsync( ).GetAwaiter( ).GetResult( );
		}

		private async Task MainAsync( )
		{
			discordClient = new DiscordSocketClient( );
			discordClient.Log += Log;
			await discordClient.LoginAsync( TokenType.Bot, botToken );
			await discordClient.StartAsync( );
			Console.WriteLine( "Bot Dlin initialized." );
			new PiazzaModule( discordClient );
			await Task.Delay( -1 ); // Hang infinitely.
		}

		private Task Log( LogMessage msg )
		{
			Console.WriteLine( msg.ToString( ) );

			return Task.CompletedTask;
		}
	}
}
