using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyTakeWhileTests
    {
        [Test]
        public void take_cards_until_separate_card()
        {
            var cards = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
                new Card {Kind = CardKind.Separate},
                new Card {Kind = CardKind.Normal, Point = 5},
                new Card {Kind = CardKind.Normal, Point = 6},
            };
            
            //碰到不符合該條件之前的都拿
            var actual = JoeyTakeWhile(cards,
                card => card.Kind != CardKind.Separate);


            var expected = new List<Card>
            {
                new Card {Kind = CardKind.Normal, Point = 2},
                new Card {Kind = CardKind.Normal, Point = 3},
                new Card {Kind = CardKind.Normal, Point = 4},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private IEnumerable<Card> JoeyTakeWhile(IEnumerable<Card> cards, Func<Card, bool> filter)
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