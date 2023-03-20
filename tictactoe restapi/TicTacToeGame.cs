namespace tictactoe_restapi
{
	public class TicTacToeGame
	{
		public string Id { get; }
		public string Player1 { get; }
		public string Player2 { get; }
		public string CurrentPlayer { get; private set; }
		public List<List<string>> Board { get; private set; }

		public string Status { get; private set; }
		public string Winner { get; private set; }

		public TicTacToeGame(string player1, string player2)
		{
			Id = Guid.NewGuid().ToString();
			Player1 = player1;
			Player2 = player2;
			CurrentPlayer = Player1;
			Board = new List<List<string>>
		{
			new List<string> {null, null, null},
			new List<string> {null, null, null},
			new List<string> {null, null, null}
		};
			Status = "In progress";
			Winner = null;
		}

		public string MakeMove(int row, int col)
		{
			if (Board[row][col] != null)
			{
				return "Invalid move";
			}

			Board[row][col] = CurrentPlayer;

			if (CheckForWin())
			{
				Status = "Game over";
				Winner = CurrentPlayer;
				return "Game over";
			}

			if (CheckForDraw())
			{
				Status = "Game over";
				return "Game over";
			}

			CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
			return "Success";
		}

		private bool CheckForWin()
		{
			// Проверяем строки
			for (int i = 0; i < 3; i++)
			{
				if (Board[i][0] != null && Board[i][0] == Board[i][1] && Board[i][1] == Board[i][2])
				{
					return true;
				}
			}

			// Проверяем столбцы
			for (int j = 0; j < 3; j++)
			{
				if (Board[0][j] != null && Board[0][j] == Board[1][j] && Board[1][j] == Board[2][j])
				{
					return true;
				}
			}

			// Проверяем диагонали
			if (Board[0][0] != null && Board[0][0] == Board[1][1] && Board[1][1] == Board[2][2])
			{
				return true;
			}

			if (Board[0][2] != null && Board[0][2] == Board[1][1] && Board[1][1] == Board[2][0])
			{
				return true;
			}

			return false;
		}

		private bool CheckForDraw()
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (Board[i][j] == null)
					{
						return false;
					}
				}
			}

			return true;
		}
	}



}
