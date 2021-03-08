using System;

namespace SearchEngine.Crawler
{
	class ProgressBar
	{
		private static float _max;
		private static float _min;
		private static int _top;

		public static float CurrentValue { get; private set; }
		
		
		public static void Initialize(float min, float max)
		{
			_top = Console.CursorTop;
			_max = max;
			_min = min;
			CurrentValue = _min;
			Console.WriteLine($"{CurrentValue}%");
		}

		public static void Update(float min, float max)
		{
			_max = max;
			_min = min;
		}

		public static void Progress()
		{
			CurrentValue++;
			float progressPercentage = (CurrentValue - _min) * 100 / (_max - _min);
			if (_top < 9000)
			{
				Console.SetCursorPosition(0, _top);
			}
			else
			{
				Console.SetCursorPosition(0, 8999);
			}
			Console.WriteLine($"{progressPercentage:0.0}%");
		}
	}
}
