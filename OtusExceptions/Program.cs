using System.Text;

namespace OtusExceptions
{
	internal class Program
	{
		private static readonly string _parseEx = "Параметры которые не прошли парсинг";
		private static readonly string _noRootsEx = "Вещественных значений не найдено";
		private static readonly string _line = "--------------------------------------------------";
		private static int a;
		private static int b;
		private static int c;
		static void Main(string[] args)
		{
			Console.WriteLine("a * x^2 + b * x + c = 0");
			while (true)
			{
				try {
					var ex = new Exception(_parseEx);

					ReadValue("a", out a, ex);
					ReadValue("b", out b, ex);
					ReadValue("c", out c, ex);

					if (ex.Data.Count > 0) throw ex;
					else break;
				}
				catch (Exception ex)
				{
					FormatData(ex.Message, Severity.Error, ex.Data);
				}
			}
			try
			{
				CalculateQuadraticEquation(a, b, c);
			}
			catch (Exception ex)
			{
				FormatData(ex.Message, Severity.Warning, ex.Data);
			}
		}
		private static void ReadValue(string paramName, out int param, Exception exception)
		{
			Console.WriteLine($"Введите значение {paramName}");
			var reply = Console.ReadLine();
			if (!int.TryParse(reply, out param)) exception.Data.Add(paramName, reply);
		}
		private static void CalculateQuadraticEquation(int a, int b, int c)
		{
			var discriminant = (b*b) - (4 * a * c);
			if (discriminant < 0)
			{
				throw new Exception(_noRootsEx);
			}
			else if (discriminant == 0)
			{
				var root = (-b + Math.Sqrt(discriminant)) / (2 * a);
				Console.WriteLine($"x = {root}");
			}
            else
            {
                var firstRoot = (-b + (int)Math.Sqrt(discriminant)) / (2 * a);
				var secondRoot = (-b - (int)Math.Sqrt(discriminant)) / (2 * a);
				Console.WriteLine($"x1 = {firstRoot},x2 = {secondRoot}");
            }
        }
		private static void FormatData(string message, Severity severity, System.Collections.IDictionary data)
		{
			if (severity == Severity.Error) SetErrorColoring();
			if (severity == Severity.Warning) SetWarningColoring();

			Console.WriteLine(_line);
			Console.WriteLine(message);
			Console.WriteLine(_line);

			foreach (var item in data)
			{
				Console.WriteLine(item.ToString());
			}

			SetNormalColoring();
		}
		private static void SetNormalColoring()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}
		private static void SetWarningColoring()
		{
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.ForegroundColor = ConsoleColor.Black;
		}
		private static void SetErrorColoring()
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
	enum Severity
	{
		Warning,
		Error
	}
}
