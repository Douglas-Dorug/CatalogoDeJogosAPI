using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDeJogosAPI_2.InputModel;
using CatalogoDeJogosAPI_2.ViewModel;

namespace CatalogoDeJogosAPI_2.Services
{
    public interface IGameService
    {
        Task<List<GameViewModel>> Get(int pagina, int quantidade);
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Post(GameInputModel jogo);
        Task<GameViewModel> Update(Guid id, GameInputModel jogo);
        Task<GameViewModel> Update(Guid id, double preco);
        Task<GameViewModel> Delete(Guid id);

    }
}
