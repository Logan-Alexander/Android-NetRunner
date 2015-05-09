using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetRunner.Core.Runner;
using Rhino.Mocks;

namespace NetRunner.Core.Tests.Runner
{
    /// <summary>
    /// This set of tests ensures the correct behaviour of event cards.
    /// </summary>
    [TestClass]
    public class EventCardTests
    {
        /// <summary>
        /// The number of credits int the Runner's supply should be lowered by the cost
        /// of an event card when the card is played.
        /// </summary>
        [TestMethod]
        public void PlayEventCard_WithCostOfTwoWhenRunnerHasFiveCredits_RunnerHasThreeCreditsRemaining()
        {
            // ARRANGE
            
            // Create a game where the Runner has 5 credits and an event card that costs 2 to play.
            GameContext context = new GameContext();
            context.RunnerCredits = 5;
            EventCard eventCard = new EventCard(0, "Test Card", 0, RunnerFaction.None, 2);

            // ACT
            
            // Play the card.
            eventCard.Play(context);

            // ASSERT
            
            // Check that the runner has 3 credits remaining.
            Assert.AreEqual(3, context.RunnerCredits);
        }

        /// <summary>
        /// An event card cannot be played if the play cost exceeds the Runner's current
        /// number of credits. If this happens, we want an exception to be thrown.
        /// </summary>
        [ExpectedException(typeof(IllegalActionException))]
        [TestMethod]
        public void PlayEventCard_CostOfFiveWhenRunnerHasTwoCredits_ThrowsException()
        {
            // ARRANGE

            // Create a game where the Runner has 2 credits and an event card that costs 5 to play.
            GameContext context = new GameContext();
            context.RunnerCredits = 2;
            EventCard eventCard = new EventCard(0, "Test Card", 0, RunnerFaction.None, 5);

            // ACT

            // Play the card.
            eventCard.Play(context);

            // ASSERT

            // See the ExpectedException attribute.
            // We want to assert that an IllegalActionException was thrown when the card was played.
        }

        /// <summary>
        /// If an event card has several effects, these should each be executed in order
        /// when the card is played.
        /// </summary>
        [TestMethod]
        public void PlayEventCard_WithTwoEffects_BothEffectsAreExecuted()
        {
            // ARRANGE
            
            // Create a game.
            GameContext context = new GameContext();

            // Create an event card with two effects.
            Effect effect1 = MockRepository.GenerateMock<Effect>();
            Effect effect2 = MockRepository.GenerateMock<Effect>();
            EventCard eventCard = new EventCard(0, "Test Card", 0, RunnerFaction.None, 0);
            eventCard.AddEffect(effect1);
            eventCard.AddEffect(effect2);

            // ACT

            // Play the card.
            eventCard.Play(context);

            // ASSERT

            // Check that the "Execute" method was called on both effects
            // with the context being provided in each case.
            effect1.AssertWasCalled(e => e.Execute(context));
            effect2.AssertWasCalled(e => e.Execute(context));
        }

        /// <summary>
        /// When an event card is played it should be removed from the Runner's Grip
        /// and added to the Heap.
        /// </summary>
        [TestMethod]
        public void PlayEventCard_RunnerHasThreeCardsInGrip_CardIsRemovedFromRunnersGripAndAddedToHeap()
        {
            // ARRANGE

            // Create a game where the Runner has 3 cards in their Grip,
            // one of which is an event card.
            GameContext context = MockRepository.GenerateMock<GameContext>();
            EventCard eventCard = new EventCard(0, "Test Card", 0, RunnerFaction.None, 0);
            RunnerCard card2 = MockRepository.GenerateMock<RunnerCard>(0, "Test Card", 0, RunnerFaction.None, 0);
            RunnerCard card3 = MockRepository.GenerateMock<RunnerCard>(0, "Test Card", 0, RunnerFaction.None, 0);
            context.AddToGrip(eventCard);
            context.AddToGrip(card2);
            context.AddToGrip(card3);

            // ACT

            // Play the card.
            eventCard.Play(context);

            // ASSERT

            // Check that the card was removed from the Grip.
            context.AssertWasCalled(c => c.RemoveFromGrip(eventCard));

            // Check that the card was added to the Heap.
            context.AssertWasCalled(c => c.AddToHeap(eventCard));
        }
    }
}
