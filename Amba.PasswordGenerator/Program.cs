using System;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace Amba.PasswordGenerator
{
    
    class Program
    {
        private readonly PasswordGeneratorCommand _passwordGeneratorCommand;
        private readonly IConsole _console;

        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IConsole>(PhysicalConsole.Singleton)
                .BuildServiceProvider();

            var app = new CommandLineApplication<PasswordGeneratorCommand>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);
            if (!args.Any())
            {
                app.ShowHelp();
                return 0;
            }
            return app.Execute(args);
        }

        public Program(PasswordGeneratorCommand passwordGeneratorCommand, IConsole console)
        {
            _passwordGeneratorCommand = passwordGeneratorCommand;
            _console = console;
        }
    }
}