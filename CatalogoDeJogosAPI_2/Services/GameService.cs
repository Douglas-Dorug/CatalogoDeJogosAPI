using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDeJogosAPI_2.Entities;
using CatalogoDeJogosAPI_2.Exceptions;
using CatalogoDeJogosAPI_2.InputModel;
using CatalogoDeJogosAPI_2.Repositories;
using CatalogoDeJogosAPI_2.ViewModel;

namespace CatalogoDeJogosAPI_2.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Obter(int pagina, int quantidade)
        {
            var games = await _gameRepository.Obter(pagina, quantidade);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Preco = game.Preco
            })
                               .ToList();
        }

        public async Task<GameViewModel> Obter(Guid id)
        {
            var game = await _gameRepository.Obter(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Preco = game.Preco
            };
        }

        public async Task<GameViewModel> Inserir(GameInputModel game)
        {
            var entidadeGame = await _gameRepository.Obter(game.Name, game.Producer);

            if (entidadeGame.Count > 0)
                throw new GameJaCadastradoException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Preco = game.Preco
            };

            await _gameRepository.Inserir(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Preco = game.Preco
            };
        }

        public async Task Atualizar(Guid id, GameInputModel game)
        {
            var entidadeGame = await _gameRepository.Obter(id);

            if (entidadeGame == null)
                throw new GameNaoCadastradoException();

            entidadeGame.Name = game.Name;
            entidadeGame.Producer = game.Producer;
            entidadeGame.Preco = game.Preco;

            await _gameRepository.Atualizar(entidadeGame);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeGame = await _gameRepository.Obter(id);

            if (entidadeGame == null)
                throw new GameNaoCadastradoException();

            entidadeGame.Preco = preco;

            await _gameRepository.Atualizar(entidadeGame);
        }

        public async Task Remover(Guid id)
        {
            var game = await _gameRepository.Obter(id);

            if (game == null)
                throw new GameNaoCadastradoException();

            await _gameRepository.Remover(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }

       
    }
}
