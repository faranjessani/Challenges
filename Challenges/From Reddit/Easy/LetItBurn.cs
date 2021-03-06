﻿using System;
using System.Text;
using NUnit.Framework;

namespace Challenges
{
    ///	<summary>
    ///	    [2017-03-27] Challenge #308 [Easy] Let it burn
    ///	    https://www.reddit.com/r/dailyprogrammer/comments/61ub0j/20170327_challenge_308_easy_let_it_burn/
    ///	    Description
    ///	    This week all challenges will be inspired by the game Flash Point
    ///	    The game is a fun cooperative game, where a bunch of fire(wo)men try to rescue victims in a burning building.
    ///	    Each round the fire is spreading, and it is this mechanic that we are going to implement.
    ///	    Formal Inputs &amp; Outputs
    ///	    Input description
    ///	    You recieve a floorplan of the building with the current situation on it. The floorplan is a grid and all tiles are
    ///	    connected vertically and horizontally. There is never ever a diagonally interaction.
    ///	    Here is the legend to what is what:
    ///	    S &lt;- smoke
    ///	    F &lt;- fire
    ///	    # &lt;- wall
    ///	    | &lt;- closed door
    ///	    / &lt;- open door
    ///	    = &lt;- damaged wall
    ///	    _ &lt;- broken wall or broken door
    ///	    &lt;- Open space (blank space)
    ///	    After the floorplan you will recieve a bunch off coordinates ((0,0) is top left coord).
    ///	    Each of these coordinates indicate where smoke developes. Depending on the tile it lands can have various outcomes:
    ///	    S -&gt; S becomes F, smoke turns into fire
    ///	    F -&gt; Nothing happens
    ///	    # -&gt; invalid move
    ///	    | -&gt; invalid move
    ///	    / -&gt; invalid move
    ///	    = -&gt; invalid move
    ///	    _ -&gt; invalid move
    ///	    -&gt;   becomes S, Smoke develops on a blank spot
    ///	    Additional rules:
    ///	    Fire and smoke: When smoke is next to a fire itself turns into a fire
    ///	    Doors and broken walls: doors and broken walls (or broken doors) connect to spaces. This means that if smoke is at
    ///	    one side and fire at the other the smoke turns into fire
    ///	    Small house:
    ///	    #############/#
    ///	    #     |       #
    ///	    #     #       #
    ///	    #     #       #
    ///	    #######       #
    ///	    #     _       #
    ///	    ###############
    ///	    Small house Input
    ///	    1 1
    ///	    1 2
    ///	    1 3
    ///	    5 6
    ///	    4 2
    ///	    1 1
    ///	    1 2
    ///	    5 5
    ///	    5 5
    ///	    9 1
    ///	    5 7
    ///	    2 2
    ///	    Output description
    ///	    Show the final Output
    ///	    #############/#
    ///	    #F    |  S    #
    ///	    #FF   #       #
    ///	    #F    #       #
    ///	    #######       #
    ///	    #    F_F      #
    ///	    ###############
    ///	    Bonus
    ///	    Explosions
    ///	    When smoke is set applied to fire, an explosion happens.
    ///	    To solve an explosion you need to look at the adjective tiles of where the explosion happend.
    ///	    S -&gt; Impossible, should already be fire
    ///	    F -&gt; Traverse in same direction until you do not have fire any more
    ///	    # -&gt; Wall take damage and becomes =
    ///	    | -&gt; Door is totaly broken and becomes _
    ///	    / -&gt; Explosion passes trough and traverse in the same direction. The door lives
    ///	    = -&gt; Wall is completely broken now and becomes _
    ///	    _ -&gt; Explosion passes trough and traverse in the same direction
    ///	    -&gt; The spot is set on fire and becomes F
    ///	    Additional input for explosion, using the outcome of the small house
    ///	    1 7
    ///	    1 8
    ///	    1 9
    ///	    1 10
    ///	    1 8
    ///	    Output:
    ///	    ########=####/#
    ///	    #F    _FFFFF  #
    ///	    #FF   # F     #
    ///	    #F    #       #
    ///	    #######       #
    ///	    #    F_F      #
    ///	    ###############
    ///	    Board game coordinates
    ///	    The board game does not use the 'structural' tiles but only the open tiles. The stuctural tiles are used to
    ///	    descripe how two tiles are connected.
    ///	    1 2 3 4 5 6 7
    ///	    #############/#
    ///	    1 # . . | . . . #
    ///	    #.....#.......#
    ///	    2 # . . # . . . #
    ///	    #######.......#
    ///	    3 # . . _ . . . #
    ///	    ###############
    ///	    EG:
    ///	    (1,1) and (1,2) are directly connected
    ///	    (1,2) and (1,3) are connected by a wall
    ///	    (3,3) and (4,3) are connected by a broken wall/door
    ///	    Work out these Inputs
    ///	    1 1
    ///	    2 1
    ///	    3 1
    ///	    4 1
    ///	    2 2
    ///	    2 3
    ///	    2 3
    ///	    2 1
    ///	    Output:
    ///	    1 2 3 4 5 6 7
    ///	    ###=#=#######/#
    ///	    1 =F.F.F_F. . . #
    ///	    #.....#.......#
    ///	    2 #F.F.F= . . . #
    ///	    ###=#=#.......#
    ///	    3 # . . _ . . . #
    ///	    ###############
    ///	    Do something fun with it
    ///	    You can animate this, or do something else fun. Amuse me :)
    ///	    Finally
    ///	    Have a good challenge idea?
    ///	    Consider submitting it to /r/dailyprogrammer_ideas
    ///	    Some feedback notes
    ///	    Good morning everyone,
    ///	    A bit confused, you seem to write your input coordinates in (Y, X) rather than (X, Y). fx (1, 9), from
    ///	    non-bonus-input, which is outside the borders of the house in X-Y but inside in Y-X. Not a big thing to work around
    ///	    but quite ambiguous :P
    ///	    This is a typon on my behalve, it is X Y. 5 7 was just to test you would ignore incorrect moves tough
    ///	    Does fire spread through closed doors? The description seems to imply yes but that doesn't make much sense...
    ///	    No it doesn't. I should have made that more clear
    ///	    Smoke adjacent to fire turns to fire, but how is this applied? Does it only update once per turn, much like
    ///	    Conway's Game of Life, or does it automatically update and continue to transform all adjacent smoke until there is
    ///	    no more left?
    ///	    All smoke adjective to fire is turned in the same turn, so it is possible to set a long corridor at fire at once if
    ///	    it is in smoke
    ///	</summary>
    [TestFixture]
    public class LetItBurn
    {
        private static void BurnNeighbours(char[,] house, int x, int y)
        {
            var inc = 1;
            while (FireCanPassThrough(house, x + inc, y))
            {
                SetOnFire(house, x + inc, y);
                BurnNeighbours(house, x + inc, y);
                inc++;
            }
            inc = 1;
            while (FireCanPassThrough(house, x - inc, y))
            {
                SetOnFire(house, x - inc, y);
                BurnNeighbours(house, x - inc, y);
                inc++;
            }
            inc = 1;
            while (FireCanPassThrough(house, x, y + inc))
            {
                SetOnFire(house, x, y + inc);
                BurnNeighbours(house, x, y + inc);
                inc++;
            }
            inc = 1;
            while (FireCanPassThrough(house, x, y - inc))
            {
                SetOnFire(house, x, y - inc);
                BurnNeighbours(house, x, y - inc);
                inc++;
            }
        }

        private static bool FireCanPassThrough(char[,] house, int x, int y)
        {
            if (x > house.GetUpperBound(0) || y > house.GetUpperBound(1))
                return false;

            var character = house[x, y];
            return character == 'S'
                   || character == '|'
                   || character == '/'
                   || character == '_';
        }

        private static void SetOnFire(char[,] house, int x, int y)
        {
            if (house[x, y] == 'S') house[x, y] = 'F';
        }

        private static StringBuilder PrintHouse(char[,] house)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < 7; i++)
            {
                for (var j = 0; j < 15; j++)
                    builder.Append(house[j, i]);
                builder.Append(new[] {'\r', '\n'});
            }
            Console.WriteLine(builder.ToString());
            return builder;
        }

        [Test]
        public void SmallHouse()
        {
            var start = @"#############/#
#     |       #
#     #       #
#     #       #
#######       #
#     _       #
###############";
            var expected = @"#############/#
#F    |  S    #
#FF   #       #
#F    #       #
#######       #
#    F_F      #
###############
";
            var input = new[]
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(5, 6),
                new Tuple<int, int>(2, 4),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(5, 5),
                new Tuple<int, int>(5, 5),
                new Tuple<int, int>(9, 1),
                new Tuple<int, int>(7, 5),
                new Tuple<int, int>(2, 2)
            };

            var width = 17;
            var height = 7;
            var house = new char[15, 7];
            for (var i = 0; i < 15; i++)
            for (var j = 0; j < 7; j++)
                house[i, j] = start[i + j * 17];

            foreach (var coordinate in input)
            {
                Console.WriteLine($"Applying: {coordinate.Item1}, {coordinate.Item2}");
                if (coordinate.Item1 > house.GetUpperBound(0) || coordinate.Item2 > house.GetUpperBound(1)) continue;
                switch (house[coordinate.Item1, coordinate.Item2])
                {
                    case 'S':
                        house[coordinate.Item1, coordinate.Item2] = 'F';
                        BurnNeighbours(house, coordinate.Item1, coordinate.Item2);
                        break;
                    case ' ':
                        int x = coordinate.Item1, y = coordinate.Item2;
                        var fire = false;
                        var inc = 1;
                        while (FireCanPassThrough(house, x + inc, y))
                            inc++;
                        if (house[x + inc, y] == 'F') fire = true;
                        inc = 1;
                        while (FireCanPassThrough(house, x - inc, y))
                            inc++;
                        if (house[x - inc, y] == 'F') fire = true;

                        inc = 1;
                        while (FireCanPassThrough(house, x, y + inc))
                            inc++;
                        if (house[x, y + inc] == 'F') fire = true;
                        inc = 1;
                        while (FireCanPassThrough(house, x, y - inc))
                            inc++;
                        if (house[x, y - inc] == 'F') fire = true;
                        house[x, y] = fire ? 'F' : 'S';
                        break;
                }
            }

            var wip = PrintHouse(house);
            Assert.That(wip.ToString(), Is.EqualTo(expected));
        }
    }
}