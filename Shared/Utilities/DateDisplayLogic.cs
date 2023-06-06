namespace CSharpToDo.Shared.Utilities
{
	public static class DateDisplayLogic
	{
		public static string GetFormattedDate(DateTime inputDate, bool expirable)
		{
			return GetFormattedDate(inputDate, expirable, DateTime.Now);
		}

		internal static string GetFormattedDate(DateTime inputDate, bool expirable, DateTime comparisonDate)
		{
			string formattedDate = string.Empty;

			if (expirable && inputDate < comparisonDate)
			{
				formattedDate = "(Expired) ";
			}

			if (inputDate.Date == comparisonDate.Date)
			{
				formattedDate += $"Today at {inputDate:HH:mm}";
			}
			else if (inputDate.Year == comparisonDate.Year)
			{
				formattedDate += $"{GetDayWithSuffix(inputDate.Day)} {inputDate:MMM HH:mm}";
			}
			else
			{
				formattedDate += $"{GetDayWithSuffix(inputDate.Day)} {inputDate:MMM yyyy HH:mm}";
			}

			return formattedDate;
		}

		private static string GetDayWithSuffix(int day)
		{
			string suffix;

			if (day >= 11 && day <= 13)
			{
				suffix = "th";
			}
			else
			{
				int lastDigit = day % 10;

				suffix = lastDigit switch
				{
					1 => "st",
					2 => "nd",
					3 => "rd",
					_ => "th",
				};
			}

			return $"{day}{suffix}";
		}
	}
}
