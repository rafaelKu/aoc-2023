namespace AoC;

public static class Day7
{
    private static readonly string FileName = nameof(Day7).ToLower();

    public static string ExecutePart1()
    {
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines();

        var sortedCards = GetCardsWithRank(lines)
            .OrderBy(group => group, new MySuperCardComparer())
            .ToArray();

        var sum = 0;
        for (int i = 0; i < sortedCards.Length; i++)
        {
            Console.WriteLine($"Cards: {sortedCards[i].cards} Bet: {sortedCards[i].bet}");
            sum += sortedCards[i].bet * (i + 1);
        }

        return sum.ToString();
    }

    public static string ExecutePart2()
    {
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines();

        var sortedCards = GetCardsWithRankAndJoker(lines)
            .OrderBy(group => group, new MySuperCardComparer())
            .ToArray();

        var sum = 0;
        for (int i = 0; i < sortedCards.Length; i++)
        {
            Console.WriteLine($"Cards: {sortedCards[i].cards} Bet: {sortedCards[i].bet}");
            sum += sortedCards[i].bet * (i + 1);
        }

        return sum.ToString();
    }

    private static List<(string cards, int bet, CardsRank rank)> GetCardsWithRankAndJoker(IEnumerable<string> lines)
    {
        var cardsWithRank = new List<(string cards, int bet, CardsRank rank)>();
        foreach (var line in lines)
        {
            try
            {
                var cards = line[..5];
                var bet = line[5..].GetNumbers().ToArray()[0];

                var result = cards
                    .GroupBy(card => card)
                    .Select(grouping => (card: grouping.Key, count: grouping.Count()))
                    .OrderByDescending(pair => pair.count)
                    .ToArray();

                var jokers = result.FirstOrDefault(x => x.card == 'J');
                if (!jokers.Equals(default))
                {
                    if (jokers.count != 5)
                    {
                        if (result[0].card != 'J')
                            result[0].count += jokers.count;
                        else
                            result[1].count += jokers.count;
                    }
                }

                var resultWithoutJokers = result
                    .Where(x => x.card != 'J' || x.count == 5)
                    .ToArray();

                var rank = GetCardRank(resultWithoutJokers);

                cardsWithRank.Add((cards, bet, rank));

            }
            catch (Exception)
            {

                throw;
            }
        }

        return cardsWithRank;
    }

    private static List<(string cards, int bet, CardsRank rank)> GetCardsWithRank(IEnumerable<string> lines)
    {
        var cardsWithRank = new List<(string cards, int bet, CardsRank rank)>();
        foreach (var line in lines)
        {
            var cards = line[..5];
            var bet = line[5..].GetNumbers().ToArray()[0];

            var result = cards
                .GroupBy(card => card)
                .Select(grouping => (card: grouping.Key, count: grouping.Count()))
                .OrderByDescending(pair => pair.count)
                .ToArray();
            var rank = GetCardRank(result);

            cardsWithRank.Add((cards, bet, rank));
        }

        return cardsWithRank;
    }

    private static CardsRank GetCardRank((char card, int count)[] result)
    {
        if (result.First().count == 5)
            return CardsRank.FiveOfAKind;
        else if (result.First().count == 4)
            return CardsRank.FourOfAKind;
        else if (result.First().count == 3 && result[1].count == 2)
            return CardsRank.FullHouse;
        else if (result.First().count == 3)
            return CardsRank.ThreeOfaAKind;
        else if (result.First().count == 2 && result[1].count == 2)
            return CardsRank.TwoPairs;
        else if (result.First().count == 2)
            return CardsRank.Pair;
        else
            return CardsRank.SingleCard;
    }

    enum CardsRank
    {
        SingleCard,
        Pair,
        TwoPairs,
        ThreeOfaAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    private static readonly Dictionary<char, int> CardRank = new()
    {
        {'J', 1},
        {'2', 2},
        {'3', 3},
        {'4', 4},
        {'5', 5},
        {'6', 6},
        {'7', 7},
        {'8', 8},
        {'9', 9},
        {'T', 10},
        {'Q', 12},
        {'K', 13},
        {'A', 14},
    };

    private class MySuperCardComparer : IComparer<(string cards, int bet, CardsRank rank)>
    {
        public int Compare((string cards, int bet, CardsRank rank) x, (string cards, int bet, CardsRank rank) y)
        {
            if (x.rank > y.rank)
                return 1;
            if (y.rank > x.rank)
                return -1;

            for (int i = 0; i < 5; i++)
            {
                if (CardRank[x.cards[i]] > CardRank[y.cards[i]])
                    return 1;
                if (CardRank[y.cards[i]] > CardRank[x.cards[i]])
                    return -1;
            }
            return 0;
        }
    }
}