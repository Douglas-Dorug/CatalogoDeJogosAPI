using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5) 
        {
            var games = await _gameService.Get(pagina, quantidade);
            if (games.Count() == 0) 
                return NoContent();
            return Ok();
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<GameViewModel>> Get(Guid idJogo)
        {
            var game = await _gameService.Get(idJogo);
            if (game == null) 
                return NoContent();
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> PostGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var jogo = await _gameService.Post(gameInputModel);

                return Ok(jogo);
            }
            //catch (JogoJaCadastradoException ex)
            catch(Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idJogo, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idJogo, gameInputModel);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _gameService.Update(idJogo, preco);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idJogo)
        {
            try
            {
                await _gameService.Delete(idJogo);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}
