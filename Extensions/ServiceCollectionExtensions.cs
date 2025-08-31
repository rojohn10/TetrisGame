using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Models;
using TetrisGame.Models.Abstractions;
using TetrisGame.Services;
using TetrisGame.Services.Abstractions;
using TetrisGame.ViewModels;
using TetrisGame.Views;

namespace TetrisGame.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {
            // Register models
            services.AddTransient<IGameBoard>(sp => new GameBoard());
            services.AddTransient<IGameState>(sp => new GameState(sp.GetRequiredService<IGameBoard>()));

            // Register services
            services.AddSingleton<ICollisionDetector, CollisionDetector>();
            services.AddSingleton<ILineDetector, LineDetector>();
            services.AddSingleton<IScoreCalculator, ScoreCalculator>();
            services.AddSingleton<IInputHandler, InputHandler>();
            services.AddSingleton<IGameEngine, GameEngine>();

            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<GameViewModel>();

            return services;
        }

        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            services.AddTransient<GamePage>();

            return services;
        }
    }

}
