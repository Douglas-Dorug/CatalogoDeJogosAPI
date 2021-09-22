using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogosAPI_2.Exceptions
{
    public class GameNaoCadastradoException : Exception
    {
        public GameNaoCadastradoException()
            : base("Este jogo não está cadastrado")
        { }
    }
}
