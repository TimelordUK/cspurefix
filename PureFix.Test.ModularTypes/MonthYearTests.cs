using System.Text.Json;
using PureFix.Types;

namespace PureFix.Test.ModularTypes
{
    public class MonthYearTests
    {
        [Test]
        [TestCase(2024, 9, "202409")]
        [TestCase(2024, 10, "202410")]
        public void Construction_MonthAndYear(int year, int month, string expected)
        {
            var my = new MonthYear(year, month);
            Assert.That(my.AsFixString(), Is.EqualTo(expected));
            Assert.That(my.Length, Is.EqualTo(6));
            Assert.That(my.IsValid, Is.True);
            Assert.That(my.HasWeekCode, Is.False);
            Assert.That(my.HasDayOfMonth, Is.False);
        }

        [Test]
        [TestCase(2024, 9, 1, "20240901")]
        [TestCase(2024, 10, 31, "20241031")]
        public void Construction_MonthAndYearAndDayOfMonth(int year, int month, int dayOfMonth, string expected)
        {
            var my = new MonthYear(year, month, dayOfMonth);
            Assert.That(my.AsFixString(), Is.EqualTo(expected));
            Assert.That(my.Length, Is.EqualTo(8));
            Assert.That(my.IsValid, Is.True);
            Assert.That(my.HasWeekCode, Is.False);
            Assert.That(my.HasDayOfMonth, Is.True);
        }

        [Test]
        [TestCase(2024, 9, WeekCode.W1, "202409w1")]
        [TestCase(2024, 9, WeekCode.W2, "202409w2")]
        [TestCase(2024, 9, WeekCode.W3, "202409w3")]
        [TestCase(2024, 9, WeekCode.W4, "202409w4")]
        [TestCase(2024, 9, WeekCode.W5, "202409w5")]
        public void Construction_MonthAndYearAndWeekfMonth(int year, int month, WeekCode weekCode, string expected)
        {
            var my = new MonthYear(year, month, weekCode);
            Assert.That(my.AsFixString(), Is.EqualTo(expected));
            Assert.That(my.Length, Is.EqualTo(8));
            Assert.That(my.IsValid, Is.True);
            Assert.That(my.Year, Is.EqualTo(year));
            Assert.That(my.Month, Is.EqualTo(month));
            Assert.That(my.HasWeekCode, Is.True);
            Assert.That(my.HasDayOfMonth, Is.False);

            Assert.That(my.TryGetWeekCode(out var w), Is.True);
            Assert.That(w, Is.EqualTo(weekCode));
        }

        [Test]
        [TestCase("20240131", true)]
        [TestCase("20240915", true)]
        [TestCase("20240931", false)] // Invalid day of month
        [TestCase("20240930", true)]
        [TestCase("202409w0", false)]
        [TestCase("202409w1", true)]
        [TestCase("202409w2", true)]
        [TestCase("202409w3", true)]
        [TestCase("202409w4", true)]
        [TestCase("202409w5", true)]
        [TestCase("202409w6", false)]
        [TestCase("20240229", true)] // leap day
        [TestCase("20250229", false)] // invalid leap day
        public void TryParseString(string value, bool expected)
        {
            var parsed = MonthYear.TryParse(value, out var monthYear);
            Assert.That(parsed, Is.EqualTo(expected));
        }

        [Test]
        public void Construction_Default()
        {
            var my = new MonthYear();
            Assert.That(my.AsFixString(), Is.EqualTo(""));
            Assert.That(my.Length, Is.EqualTo(0));
            Assert.That(my.IsValid, Is.False);
            Assert.That(my.Year, Is.EqualTo(0));
            Assert.That(my.Month, Is.EqualTo(0));

            Assert.That(my.TryGetWeekCode(out var _), Is.False);
            Assert.That(my.TryGetDayOfMonth(out var _), Is.False);
        }

        [Test]
        public void Construction_LeapYear()
        {
            var my = new MonthYear(2024, 2, 29);
            Assert.That(my.AsFixString(), Is.EqualTo("20240229"));
        }

        [Test]
        public void Construction_NotLeapYear()
        {
            Assert.Catch(() => new MonthYear(2023, 2, 29));
        }

        [Test]
        public void Construction_30DayMonths()
        {
            Assert.Catch(() => new MonthYear(2023, 4, 31));
            Assert.Catch(() => new MonthYear(2023, 6, 31));
            Assert.Catch(() => new MonthYear(2023, 9, 31));
            Assert.Catch(() => new MonthYear(2023, 11, 31));
        }

        [Test]
        public void Parse_BadValues()
        {
            Assert.Catch(() => MonthYear.Parse("Jack"));
            Assert.Catch(() => MonthYear.Parse("20240199"));
        }

        [Test]
        public void JsonSerialization()
        {
            var data = new JsonData
            {
                Value = new(2024, 9, 15),
                NullableValue = new(2024, 8, WeekCode.W3)
            };

            var text = JsonSerializer.Serialize(data);
            var loaded = JsonSerializer.Deserialize<JsonData>(text);

            Assert.That(data.Value, Is.EqualTo(loaded.Value));
            Assert.That(data.DefaultValue, Is.EqualTo(loaded.DefaultValue));
            Assert.That(data.NullableValue, Is.EqualTo(loaded.NullableValue));
            Assert.That(data.NullableValueNotSet, Is.EqualTo(loaded.NullableValueNotSet));
        }

        [Test]
        public void Equality()
        {
            var left = new MonthYear(2024, 9, 15);
            var right = new MonthYear(2024, 9, 15);

            Assert.That(left == right, Is.True);
            Assert.That(left != right, Is.False);
        }

        [Test]
        public void Hashing()
        {
            // As the use BitConverter we know a bit about the hash codes we'll get
            var def = new MonthYear();
            Assert.That(def.GetHashCode(), Is.EqualTo(0));

            var m = new MonthYear(2024, 9, 15);
            Assert.That(m.GetHashCode(), Is.Not.EqualTo(0));
        }

        class JsonData
        {
            public MonthYear Value{get; set;}
            public MonthYear DefaultValue{get; set;}
            public MonthYear? NullableValue{get; set;}
            public MonthYear? NullableValueNotSet{get; set;}
        }
    }
}
