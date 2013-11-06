using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Andover.Domain.Components.Logs.Results;
using Andover.Domain.Utils;

namespace Andover.Data.Logs.Provider.Chainsaw
{
	public class LogParser
	{
		public static string GetLogFileContent(string logFilePath)
		{
			string contents = string.Empty;

			try
			{
				contents = File.ReadAllText(logFilePath);
			}
			catch (Exception)
			{
				// TODO: Clean this up to look for specific excption
				// check for error when file is already in use. Capture here and allow for processing of other files.S
			}
			
			return contents;
		}

		public static List<LogEntry> GetLogFileContents(FileInfo log, string path)
		{
			var logEntries = new List<LogEntry>();

			var filename = log.Name.Split('.');
			bool isFileDateValid = filename.Length >= 2 && filename[1].Length == 8;
			var filenameDate = isFileDateValid ? filename[1].Substring(0, 4) + "-" + filename[1].Substring(4, 2) + "-" + filename[1].Substring(6, 2) : string.Empty;

			var contents = GetLogFileContent(log.FullName);
			if (string.IsNullOrEmpty(contents))
			{
				return logEntries;
			}

			var allLines = contents.Split(new string[] { "\n" }, StringSplitOptions.None); // break on new line

			bool isValidLogFile = allLines.Any(l => l.Contains("Sitecore started"));

			if (isValidLogFile)
			{
				logEntries = (from line in allLines
							  let timestampMatch = Regex.Match(line, @"\d{2}:\d{2}:\d{2}")
							  let timestamp = DateFormatting.TryParseDate(string.Format("{0} {1}", filenameDate, timestampMatch.Value))
							  let logEntryWithoutTimestamp = line.Substring((timestampMatch.Index + timestampMatch.Length), line.Length - (timestampMatch.Index + timestampMatch.Length))
							  where timestampMatch != null && !string.IsNullOrEmpty(logEntryWithoutTimestamp)
							  select new LogEntry()
							  {
								  Filename = log.FullName,
								  Timestamp = (DateTime)timestamp,
								  Message = logEntryWithoutTimestamp
							  }).ToList();
			}

			return logEntries;
		}
	}
}
