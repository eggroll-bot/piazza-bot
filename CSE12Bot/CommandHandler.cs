using System.Reflection;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

namespace CSE12Bot
{
	public class CommandHandler
	{
		private readonly DiscordSocketClient client;
		private readonly CommandService commands;

		public CommandHandler( DiscordSocketClient client, CommandService commands )
		{
			this.client = client;
			this.commands = commands;
		}

		public async Task InstallCommandsAsync( )
		{
			client.MessageReceived += HandleCommandAsync;
			await commands.AddModulesAsync( assembly: Assembly.GetEntryAssembly( ), services: null );
		}

		private async Task HandleCommandAsync( SocketMessage messageParam )
		{
			// Ignore system messages.
			if ( messageParam is not SocketUserMessage message )
			{
				return;
			}

			// Ignore bot messages.
			if ( message.Author.IsBot )
			{
				return;
			}

			int argPos = 0;

			// Check prefix of command.
			if ( !message.HasCharPrefix( 'p', ref argPos ) && !message.HasCharPrefix( '!', ref argPos ) )
			{
				return;
			}

			SocketCommandContext context = new SocketCommandContext( client, message );
			await commands.ExecuteAsync( context: context, argPos: argPos, services: null );
		}
	}
}
