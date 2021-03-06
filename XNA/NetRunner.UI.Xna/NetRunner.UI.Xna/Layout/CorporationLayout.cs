﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Layout
{
    public class CorporationLayout
    {
        /* |-----------------------------||----------|
         * | Ice                         || Stuff    |
         * |                             ||          |
         * |                             ||          |
         * |                             ||          |
         * |                             ||          |
         * |                             ||          |
         * |-----------------------------||----------|
         * |-----------------------------||----------|
         * | Servers                     || Credits  |
         * |                             ||          |
         * |                             ||          |
         * |-----------------------------||----------|
         * 
         * Ice Area:        The Ice that guards the servers.
         * Servers Area:    From left-to-right: Archives, R&D, HQ, Remote Server 1, Remote server 2, etc...
         * Stuff Area:      Score area, click tracker and action list.
         * Credits Area:    The corp's pile of credits.
         * 
         * There is a margin around the outside edge and between each region.
         */

        public int Margin { get; private set; }

        public Vector2 CardSize { get; private set; }

        public Rectangle IceArea { get; private set; }
        public Rectangle ServersArea { get; private set; }
        public Rectangle StuffArea { get; private set; }
        public Rectangle CreditsArea { get; private set; }

        public Rectangle ArchivesArea { get; private set; }
        public Rectangle ResearchAndDevelopmentArea { get; private set; }
        public Rectangle HeadQuartersArea { get; private set; }
        public Rectangle RemoteServersArea { get; private set; }
        public Rectangle ScoreArea { get; private set; }

        public CorporationLayout(Rectangle titleSafeArea)
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

            int serversAreaWidth = titleSafeArea.Width - creditsAreaWidth - (3 * Margin);
            int serversAreaHeight = creditsAreaHeight;

            ServersArea = new Rectangle(
                Margin,
                titleSafeArea.Bottom - serversAreaHeight - Margin,
                serversAreaWidth,
                serversAreaHeight);

            int stuffAreaWidth = creditsAreaWidth;
            int stuffAreaHeight = titleSafeArea.Height - creditsAreaHeight - (3 * Margin);

            StuffArea = new Rectangle(
                titleSafeArea.Right - stuffAreaWidth - Margin,
                Margin,
                stuffAreaWidth,
                stuffAreaHeight);

            int iceAreaWidth = serversAreaWidth;
            int iceAreaHeight = stuffAreaHeight;

            IceArea = new Rectangle(
                Margin,
                Margin,
                iceAreaWidth,
                iceAreaHeight);

            ArchivesArea = new Rectangle(
                ServersArea.Right - cardWidth,
                ServersArea.Top,
                cardWidth,
                cardHeight);

            ResearchAndDevelopmentArea = new Rectangle(
                ArchivesArea.Left - (cardWidth + Margin),
                ServersArea.Top,
                cardWidth,
                cardHeight);

            HeadQuartersArea = new Rectangle(
                ResearchAndDevelopmentArea.Left - (cardWidth + Margin),
                ServersArea.Top,
                cardWidth,
                cardHeight);

            RemoteServersArea = new Rectangle(
                ServersArea.Left,
                ServersArea.Top,
                ServersArea.Width - (ArchivesArea.Width + Margin + ResearchAndDevelopmentArea.Width + Margin + HeadQuartersArea.Width + Margin),
                cardHeight);

            ScoreArea = new Rectangle(
                StuffArea.Left,
                StuffArea.Top,
                cardWidth,
                cardHeight);
        }
    }
}
