using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    [Ignore("not yet")]
    public class JoeySkipWhileTests
    {
        [Test]
        public void skip_cards_until_separate_card()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
                new Card {Kind = CardKind.Separate},
            };

            //符合條件之後的都拿，包含自己
            var actual = JoeySkipWhile(cards,
                card => card.Kind == CardKind.Separate);

            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
                new Card {Kind = CardKind.Separate},
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        private IEnumerable<Card> JoeySkipWhile(IEnumerable<Card> cards, Func<Card, bool> filter)
        {
            var employeesEnumerator = cards.GetEnumerator();
            while (employeesEnumerator.MoveNext())
            {
                var card = employeesEnumerator.Current;
                if (filter(card))
                    yield return card;
                else
                    yield break;
            }
        }
    }
}