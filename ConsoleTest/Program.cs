using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleTest
{
	class MainClass
	{
		private static string BlankLine { get; set; }

		public static void Main(string[] args)
		{
			bool exit = false;
			bool newLine = false;
			var keys = new List<ConsoleKeyInfo>();
			int WriterRow = Console.WindowHeight - 1;
			const int MonitorRow = 0;
			const int BufferSize = 100;
			BlankLine = new string(' ', Console.WindowWidth);
			var buffer = new List<string>(BufferSize);
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
				ClearRow(WriterRow, null);
				WriteMonitor(buffer);
				Console.SetCursorPosition(0, WriterRow); 
			} while (exit == false);
		}

		private static void WriteMonitor(List<string> buffer)
		{
			int numWindowRows = Console.WindowHeight - 1;
			int rowsToWrite = (buffer.Count > numWindowRows) ? numWindowRows : buffer.Count;
			int bufferRowStart = (buffer.Count > numWindowRows) ? buffer.Count - numWindowRows : 0;
			string line = null;
			for (int row = 0; row < numWindowRows; row++)				
			{
				line = null;
				if (row < rowsToWrite)
				{
					int bufferRow = bufferRowStart + row;
					int thisRowLength = buffer[bufferRow].Length;
					line = buffer[bufferRow] + BlankLine.Substring(thisRowLength, BlankLine.Length - thisRowLength); 
				}
				ClearRow(row, line);
			}
		}

		private static void ClearRow(int rowNumber, string line)
		{
			Console.SetCursorPosition(0, rowNumber);
			if (line == null)
				Console.Write(BlankLine);
			else
				Console.Write(line);
		}
	}
}
