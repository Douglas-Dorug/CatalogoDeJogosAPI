using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDeJogosAPI_2.Exceptions
{
    public class GameJaCadastradoException : Exception
    {
        public GameJaCadastradoException()
            : base("Este já jogo está cadastrado")
        { }
    }
}
