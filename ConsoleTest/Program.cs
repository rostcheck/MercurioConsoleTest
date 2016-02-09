using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleTest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			bool exit = false;
			bool newLine = false;
			var keys = new List<ConsoleKeyInfo>();
			int WriterRow = Console.WindowHeight - 1;
			const int MonitorRow = 0;
			const int BufferSize = 100;
			var buffer = new List<string>(BufferSize);
			//Console.SetBufferSize(80, 25);
			Console.BufferWidth = 80;
			Console.BufferHeight = 25;
			Console.SetCursorPosition(0, WriterRow);
			do
			{
				do
				{
					var consoleKeyInfo = Console.ReadKey();
					switch(consoleKeyInfo.Key)
					{
						case ConsoleKey.Enter:
							newLine = true;
							break;
						case ConsoleKey.Backspace:
						case ConsoleKey.Delete:
							keys.RemoveAt(keys.Count - 1);
							break;
						default:
							keys.Add(consoleKeyInfo);
							break;
					}							
					var left = Console.CursorLeft;
					Console.SetCursorPosition(left, WriterRow);
				} while (newLine == false);
				var line = string.Join("", keys.Select(s => s.KeyChar).ToList());
				var lineLower = line.ToLower();
				if (lineLower == "exit" || lineLower == "quit")
					exit = true;
				buffer.Add(line);
				newLine = false; 
				keys.Clear(); // Clear buffers
				ClearRow(WriterRow);
				WriteMonitor(buffer);
				Console.SetCursorPosition(0, WriterRow); 
			} while (exit == false);
		}

		private static void WriteMonitor(List<string> buffer)
		{
			int numWindowRows = Console.WindowHeight - 1;
			int rowsToWrite = (buffer.Count > numWindowRows) ? numWindowRows : buffer.Count;
			int bufferRowStart = (buffer.Count > numWindowRows) ? buffer.Count - numWindowRows : 0;
			for (int row = 0; row < numWindowRows; row++)
			{
				ClearRow(row);
				if (row < rowsToWrite)
				{
					Console.SetCursorPosition(0, row);
					Console.Write(buffer[bufferRowStart + row]);
				}
			}
		}

		private static void ClearRow(int rowNumber)
		{
			for (int col = 0; col < Console.BufferWidth; col++)
			{
				Console.SetCursorPosition(col, rowNumber);
				Console.Write(" ");
			}
		}
	}
}
