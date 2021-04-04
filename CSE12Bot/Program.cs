using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Timers;

using Discord;
using Discord.WebSocket;

namespace IsLabZeroOutYet
{
	class Program
	{
		private const string botToken = "";
		private const ulong serverId = 0;
		private const ulong channelId = 0;
		private DiscordSocketClient discordClient;

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
			mainTimer.Elapsed += MainTimer_Elapsed;
			mainTimer.AutoReset = true;
			mainTimer.Enabled = true;

			await Task.Delay( -1 );
		}

		private void MainTimer_Elapsed( object sender, ElapsedEventArgs e )
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
