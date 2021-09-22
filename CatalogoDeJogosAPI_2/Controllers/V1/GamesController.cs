using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDeJogosAPI_2.Exceptions;
using CatalogoDeJogosAPI_2.InputModel;
using CatalogoDeJogosAPI_2.Services;
using CatalogoDeJogosAPI_2.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeJogosAPI_2.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5) 
        {
            var games = await _gameService.Obter(pagina, quantidade);
            if (games.Count() == 0) 
                return NoContent();
            return Ok(games);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Obter(Guid idJogo)
        {
            var game = await _gameService.Obter(idJogo);
            if (game == null) 
                return NoContent();
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InserirJogo([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var jogo = await _gameService.Inserir(gameInputModel);

                return Ok(jogo);
            }
            catch (GameJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Atualizar(idGame, gameInputModel);

                return Ok();
            }
            catch (GameNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idGame:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idGame, [FromRoute] double preco)
        {
            try
            {
                await _gameService.Atualizar(idGame, preco);

                return Ok();
            }
            catch (GameNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Remover(idGame);

                return Ok();
            }
            catch (GameNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}
