using System.Drawing;

namespace CPC
{
	public interface IUserInterface
	{
		Bitmap Background { get; set; }

		void MoveWindow(int x, int y);

		bool IsVisible { get; set; }
	}
}