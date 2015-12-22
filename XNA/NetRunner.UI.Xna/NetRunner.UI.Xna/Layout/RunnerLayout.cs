using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Layout
{
    public class RunnerLayout
    {
        /* |-----------------------------||----------|
         * | Programs             | ID   || Stuff    |
         * |                      |      ||          |
         * |                      |      ||          |
         * |-----------------------------||          |
         * | Hardware                    ||          |
         * |                             ||          |
         * |                             ||----------|
         * |-----------------------------||----------|
         * | Resources                   || Credits  |
         * |                             ||          |
         * |                             ||          |
         * |-----------------------------||----------|
         * 
         * Programs Area:   Icebreakers and co.
         * ID Area:         Runner's identity. This would possibly be better on the bottom row and act as the player's hand of cards.
         * Hardware Area:   Upgrades (Maybe this shouldn't exist until some hardware is played?)
         * Resources Area:  Connections and objects. (This too?)
         * Stuff Area:      Score area, click tracker and action list.
         * Credits Area:    The runner's pile of credits.
         * 
         * There is a margin around the outside edge and between each region.
         */

        public int Margin { get; private set; }

        public Vector2 CardSize { get; private set; }

        public Rectangle ProgramArea { get; private set; }
        public Rectangle HardwareArea { get; private set; }
        public Rectangle ResourcesArea { get; private set; }
        public Rectangle StuffArea { get; private set; }
        public Rectangle CreditsArea { get; private set; }

        public Rectangle HeapArea { get; private set; }
        public Rectangle StackArea { get; private set; }

        public Rectangle ConsoleArea { get; private set; }

        public Rectangle IdentityArea { get; private set; }
        public Rectangle ScoreArea { get; private set; }

        public RunnerLayout(Rectangle titleSafeArea)
        {
            Margin = (int)(titleSafeArea.Width / 100);

            int cardWidth = (int)(titleSafeArea.Width / 10);
            int cardHeight = (int)(cardWidth / 0.7);

            CardSize = new Vector2(cardWidth, cardHeight);

            int creditsAreaWidth = cardWidth;
            int creditsAreaHeight = cardHeight;

            CreditsArea = new Rectangle(
                titleSafeArea.Right - creditsAreaWidth - Margin,
                titleSafeArea.Bottom - creditsAreaHeight - Margin,
                creditsAreaWidth,
                creditsAreaHeight);

            int resourcesAreaWidth = titleSafeArea.Width - (3 * cardWidth) - (5 * Margin);
            int resourcesAreaHeight = creditsAreaHeight;

            ResourcesArea = new Rectangle(
                Margin,
                titleSafeArea.Bottom - resourcesAreaHeight - Margin,
                resourcesAreaWidth,
                resourcesAreaHeight);

            int stuffAreaWidth = creditsAreaWidth;
            int stuffAreaHeight = titleSafeArea.Height - creditsAreaHeight - (3 * Margin);

            StuffArea = new Rectangle(
                titleSafeArea.Right - stuffAreaWidth - Margin,
                Margin,
                stuffAreaWidth,
                stuffAreaHeight);

            int programAreaWidth = titleSafeArea.Width - creditsAreaWidth - cardWidth - (4 * Margin);
            int programAreaHeight = titleSafeArea.Height - resourcesAreaHeight - creditsAreaHeight - (4 * Margin);

            ProgramArea = new Rectangle(
                Margin,
                Margin,
                programAreaWidth,
                programAreaHeight);

            HardwareArea = new Rectangle(
                Margin,
                ResourcesArea.Top - resourcesAreaHeight - Margin,
                programAreaWidth,
                cardHeight);

            ConsoleArea = new Rectangle(
                HardwareArea.Right + Margin,
                HardwareArea.Top,
                cardWidth,
                cardHeight);

            HeapArea = new Rectangle(
                CreditsArea.Left - creditsAreaWidth - Margin,
                ResourcesArea.Top,
                cardWidth,
                cardHeight);

            StackArea = new Rectangle(
                HeapArea.Left - (cardWidth + Margin),
                ResourcesArea.Top,
                cardWidth,
                cardHeight);

            IdentityArea = new Rectangle(
                StuffArea.Left - (cardWidth + Margin),
                ProgramArea.Top,
                cardWidth,
                cardHeight);

            ScoreArea = new Rectangle(
                StuffArea.Left,
                StuffArea.Top,
                cardWidth,
                cardHeight);
        }
    }
}
