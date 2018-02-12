using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Challenges
{
    ///	<summary>
    ///	This problem is from Jackson Gabbard's set of interview problems
    ///	Available at: https://youtu.be/olK6SWl8UrM
    ///	In his video, he postulated that if you solve the problem by finding
    ///	events that don't conflict is easier and that didn't ring true to me
    ///	so I wrote it in that format
    ///	I ended up using a set and a map but the code came out fairly simple
    ///	</summary>
    [TestFixture]
    public class CalendarConflicts
    {
        [Test]
        public void FindOverlappingCalendarEntries()
        {
            var result = new Dictionary<int, HashSet<CalendarEntry>>();
            var end = _calendarEntries[0].End;
            for (int conflictIndex = 0, calendarIndex = 1; calendarIndex < _calendarEntries.Length; calendarIndex++)
            {
                if (_calendarEntries[calendarIndex].Start <= end)
                {
                    if (!result.ContainsKey(conflictIndex)) result[conflictIndex] = new HashSet<CalendarEntry>();
                    result[conflictIndex].Add(_calendarEntries[conflictIndex]);
                    result[conflictIndex].Add(_calendarEntries[calendarIndex]);
                }
                else
                {
                    conflictIndex = calendarIndex;
                }

                end = Math.Max(_calendarEntries[calendarIndex].End, _calendarEntries[conflictIndex].End);
            }

            Assert.That(result.Values.ToArray(), Is.EqualTo(_overlappingEntries));
        }

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
    }
}