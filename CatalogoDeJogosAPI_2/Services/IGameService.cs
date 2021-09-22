using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDeJogosAPI_2.InputModel;
using CatalogoDeJogosAPI_2.ViewModel;

namespace CatalogoDeJogosAPI_2.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Obter(int pagina, int quantidade);
        Task<GameViewModel> Obter(Guid id);
        Task<GameViewModel> Inserir(GameInputModel game);
        Task Atualizar(Guid id, GameInputModel game);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);

    }
}
