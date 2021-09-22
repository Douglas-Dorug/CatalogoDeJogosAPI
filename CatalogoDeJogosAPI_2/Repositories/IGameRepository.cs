using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDeJogosAPI_2.Entities;

namespace CatalogoDeJogosAPI_2.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Obter(int pagina, int quantidade);
        Task<List<Game>> Obter(string name, string producer);
        Task<Game> Obter(Guid id);
        Task Inserir(Game game);
        Task Atualizar(Game game);
        Task Remover(Guid id);

    }
}
