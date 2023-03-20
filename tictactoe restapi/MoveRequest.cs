namespace tictactoe_restapi
{
	public class MoveRequest
	{
		public string Player { get; set; }
		public int Row { get; set; }
		public int Col { get; set; }
	}
}
