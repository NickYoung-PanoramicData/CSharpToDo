using CSharpToDo.Shared.Utilities;
using FluentAssertions;

namespace CSharpToDo.Tests;

public class DateFormattingTests
{
	[Fact]
	//Take an expired date that is marked as expirable and return a string that starts with "(Expired)"
	public void GetFormattedDate_ExpirableAndExpired_ReturnsExpiredLabel()
	{
		DateTime inputDate = new DateTime(2023, 1, 1);
		bool expirable = true;
		DateTime comparisonDate = new DateTime(2023, 1, 2);

		string result = DateDisplayLogic.GetFormattedDate(inputDate, expirable, comparisonDate);

		result.Should().StartWith("(Expired)");
	}

	[Fact]
	//Take a non-expired date that is marked as expirable and return a string that does not start with "(Expired)"
	public void GetFormattedDate_ExpirableAndNotExpired_ReturnsFormattedDate()
	{
		DateTime inputDate = new DateTime(2023, 1, 2);
		bool expirable = true;
		DateTime comparisonDate = new DateTime(2023, 1, 1);

		string result = DateDisplayLogic.GetFormattedDate(inputDate, expirable, comparisonDate);

		result.Should().NotStartWith("(Expired)");
		result.Should().Contain(inputDate.ToString("HH:mm"));
	}

	[Fact]
	//Take a non-expirable date and return a string that does not start with "(Expired)"
	public void GetFormattedDate_NonExpirable_ReturnsFormattedDate()
	{
		DateTime inputDate = new DateTime(2023, 1, 2);
		bool expirable = false;
		DateTime comparisonDate = new DateTime(2023, 1, 1);

		string result = DateDisplayLogic.GetFormattedDate(inputDate, expirable, comparisonDate);

		result.Should().NotStartWith("(Expired)");
		result.Should().Contain(inputDate.ToString("HH:mm"));
	}

	[Fact]
	//Take today's date and return a string that contains "Today at" and the time
	public void GetFormattedDate_Today_ReturnsTodayFormattedDate()
	{
		DateTime inputDate = new DateTime(2023, 1, 1);
		bool expirable = true;
		DateTime comparisonDate = new DateTime(2023, 1, 1);
		string result = DateDisplayLogic.GetFormattedDate(inputDate, expirable, comparisonDate);

		result.Should().Contain("Today at");
		result.Should().Contain(inputDate.ToString("HH:mm"));
	}

	[Fact]
	//Take a date from the current year and return a string that contains the date and time
	public void GetFormattedDate_CurrentYear_ReturnsFormattedDate()
	{
		DateTime inputDate = new DateTime(2023, 6, 1);
		bool expirable = true;
		DateTime comparisonDate = new DateTime(2023, 1, 1);

		string result = DateDisplayLogic.GetFormattedDate(inputDate, expirable, comparisonDate);

		result.Should().Contain("1st Jun");
		result.Should().Contain(inputDate.ToString("HH:mm"));
	}

	[Fact]
	//Take a date from a previous year and return a string that specifies the year and contains the date and time
	public void GetFormattedDate_OldYear_ReturnsFormattedDate()
	{
		DateTime inputDate = new DateTime(2020, 1, 1);
		bool expirable = true;

		string result = DateDisplayLogic.GetFormattedDate(inputDate, expirable);

		result.Should().Contain("Expired");
		result.Should().Contain("2020");
	}
}
