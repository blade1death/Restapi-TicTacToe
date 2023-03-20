using Microsoft.AspNetCore.Mvc;



namespace tictactoe_restapi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GamesController : ControllerBase
	{
		private static Dictionary<string, TicTacToeGame> games = new Dictionary<string, TicTacToeGame>();

		[HttpPost]
		public IActionResult CreateGame([FromBody] GameRequest request)
		{
			var game = new TicTacToeGame(request.Player1, request.Player2);
			games[game.Id] = game;

			return Ok(new { id = game.Id });
		}

		[HttpGet("{id}")]
		public IActionResult GetGame(string id)
		{
			if (!games.ContainsKey(id))
			{
				return NotFound();
			}

			var game = games[id];

			return Ok(new
			{
				id = game.Id,
				board = game.Board,
				currentPlayer = game.CurrentPlayer,
				winner = game.Winner,
				status = game.Status
			});
		}


		[HttpPatch("{id}")]
		public IActionResult MakeMove(string id, [FromBody] MoveRequest request)
		{
			if (!games.ContainsKey(id))
			{
				return NotFound();
			}

			var game = games[id];

			if (game.Status != "In progress")
			{
				return BadRequest(new { error = "Game is over" });
			}

			if (game.CurrentPlayer != request.Player)
			{
				return BadRequest(new { error = "It's not your turn" });
			}

			var result = game.MakeMove(request.Row, request.Col);

			if (result == "Invalid move")
			{
				return BadRequest(new { error = "Invalid move" });
			}

			if (result == "Game over")
			{
				return Ok(new
				{
					id = game.Id,
					board = game.Board,
					currentPlayer = game.CurrentPlayer,
					winner = game.Winner,
					status = game.Status
				});
			}

			return Ok(new
			{
				id = game.Id,
				board = game.Board,
				currentPlayer = game.CurrentPlayer
			});
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteGame(string id)
		{
			if (!games.ContainsKey(id))
			{
				return NotFound();
			}

			games.Remove(id);

			return NoContent();
		}
	}
}

