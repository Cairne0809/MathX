namespace MathematicsX
{
	public interface ISquareMatrix
	{
		int Row { get; }
		int Column { get; }
		int Length { get; }
		double this[int index] { get; set; }
		double this[int row, int column] { get; set; }
	}
}
