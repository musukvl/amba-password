using System;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace Amba.PasswordGenerator
{
    [Command(Name = "generate-password", Description = "Generates random passwords with guarantied presence of letters, numbers and symbols.")]
    [HelpOption("--help|-h")]
    class Program
    {
        private readonly PasswordGeneratorService _passwordGeneratorService;
        private readonly IConsole _console;

        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<PasswordGeneratorService>()
                .AddSingleton<IConsole>(PhysicalConsole.Singleton)
                .BuildServiceProvider();

            var app = new CommandLineApplication<Program>();
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

        public Program(PasswordGeneratorService passwordGeneratorService, IConsole console)
        {
            _passwordGeneratorService = passwordGeneratorService;
            _console = console;
        }

        [Argument(1,"length", "Password length")]
        public int Length { get; } = 8;
        
        [Option("--no-symbols|-ns", "Not use symbols in password", CommandOptionType.NoValue)]
        public bool NotUseSymbols { get; } = false;

        public int OnExecute()
        {
            var result = _passwordGeneratorService.Generate(Length, !NotUseSymbols);
            _console.WriteLine(result);
            return 0;
        }
    }
}