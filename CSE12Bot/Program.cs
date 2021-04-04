using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

using Discord;
using Discord.WebSocket;

namespace CSE12Bot
{
	class Program
	{
		private readonly string botToken = File.ReadAllText( "bot_token.txt" );
		private DiscordSocketClient discordClient;
		private const ulong serverId = 0;
		private const ulong channelId = 0;

		private static void Main( )
		{
			Console.WriteLine( "Lab 0 Pester-er started!" );
			new Program( ).MainAsync( ).GetAwaiter( ).GetResult( );
		}

		private async Task MainAsync( )
		{
			discordClient = new DiscordSocketClient( );
			discordClient.Log += Log;
			await discordClient.LoginAsync( TokenType.Bot, botToken );
			await discordClient.StartAsync( );

			Timer mainTimer = new( 1.8e+6 ); // 30 minutes.
			mainTimer.Elapsed += MainTimerElapsed;
			mainTimer.AutoReset = true;
			mainTimer.Enabled = true;

			await Task.Delay( -1 );
		}

		private void MainTimerElapsed( object sender, ElapsedEventArgs e )
		{
			discordClient.GetGuild( serverId ).GetTextChannel( channelId ).SendMessageAsync( "Lab 0 is out now as of " + DateTime.Now.ToString( new CultureInfo( "en-US" ) ) + "." );
		}

		private Task Log( LogMessage msg )
		{
			Console.WriteLine( msg.ToString( ) );

			return Task.CompletedTask;
		}
	}
}
