using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Challenges
{
    [TestFixture]
    public class CalendarConflicts
    {
        [SetUp]
        public void SetUp()
        {
            _calendarEntries = new[]
            {
                new CalendarEntry(1, 2, 'a'),
                new CalendarEntry(3, 5, 'b'),
                new CalendarEntry(4, 6, 'c'),
                new CalendarEntry(7, 10, 'd'),
                new CalendarEntry(8, 11, 'e'),
                new CalendarEntry(10, 12, 'f'),
                new CalendarEntry(13, 14, 'g'),
                new CalendarEntry(13, 14, 'h')
            };

            _overlappingEntries = new[]
            {
                new[]
                {
                    new CalendarEntry(3, 5, 'b'),
                    new CalendarEntry(4, 6, 'c')
                },
                new[]
                {
                    new CalendarEntry(7, 10, 'd'),
                    new CalendarEntry(8, 11, 'e'),
                    new CalendarEntry(10, 12, 'f')
                },
                new[]
                {
                    new CalendarEntry(13, 14, 'g'),
                    new CalendarEntry(13, 14, 'h')
                }
            };
        }

        private CalendarEntry[] _calendarEntries;
        private CalendarEntry[][] _overlappingEntries;

        internal struct CalendarEntry
        {
            public override string ToString()
            {
                return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
            }

            public CalendarEntry(int start, int end, char name)
            {
                Start = start;
                End = end;
                Name = name;
            }

            public int Start { get; }
            public int End { get; }
            public char Name { get; }
        }

        [Test]
        public void FindOverlappingCalendarEntries()
        {
            var result = new Dictionary<int, HashSet<CalendarEntry>>();
            var end = _calendarEntries[0].End;
            for (int j = 0, i = 1; i < _calendarEntries.Length; i++)
            {
                if (_calendarEntries[i].Start <= end)
                {
                    if (!result.ContainsKey(j)) result[j] = new HashSet<CalendarEntry>();
                    result[j].Add(_calendarEntries[j]);
                    result[j].Add(_calendarEntries[i]);
                }
                else
                {
                    j = i;
                }

                end = Math.Max(_calendarEntries[i].End, _calendarEntries[j].End);
            }

            Assert.That(result.Values.ToArray(), Is.EqualTo(_overlappingEntries));
        }
    }
}