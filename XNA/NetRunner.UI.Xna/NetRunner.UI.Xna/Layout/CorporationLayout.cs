using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Layout
{
    public class CorporationLayout
    {
        /* |-----------------------------|----------|
         * | To the runner               | Menu     |
         * |-----------------------------|----------|
         * | Content      n              | Card     |
         * |              |              | Close-up |
         * |         <- Drag ->          |          |
         * |              |              |          |
         * |              v              |----------|
         * |-----------------------------| Status   |
         * | Card list                   |          |
         * |         <- Drag ->          |          |
         * |                             |          |
         * |-----------------------------|----------|
         * | Console                     | Summary  |
         * |-----------------------------|----------|
         * 
         * To the runner:   An way to scroll up to the runner's view
         * Content:         The corporation's play area. Drag to scroll
         * Card list:       Used to show a list of cards, e.g. the corp's hand or the contents of archives. Drag to scroll
         * Console:         Informational text, such as what action the runner is taking
         * Menu:            A way to access the game menus and maybe toggle quick settings
         * Card close-up:   A view of the currently selected card
         * Status:          A list of actions that can be taken
         * Summary:         The corporation's credits, clicks and agendas.
         */

        public int TopBarHeight { get; private set; }
        public int BottomBarHeight { get; private set; }

        public int RightColumnWidth { get; private set; }
        public int LeftColumnWidth { get; private set; }

        public int CardCloseUpHeight { get; private set; }
        public int StatusHeight { get; private set; }

        public int CardListHeight { get; private set; }
        public int ContentHeight { get; private set; }

        public Rectangle Menu { get; private set; }
        public Rectangle CardCloseUp { get; private set; }
        public Rectangle Status { get; private set; }
        public Rectangle Summary { get; private set; }

        public Rectangle ToTheRunner { get; private set; }
        public Rectangle Content { get; private set; }
        public Rectangle CardList { get; private set; }
        public Rectangle Console { get; private set; }

        public CorporationLayout(Rectangle titleSafeArea)
        {
            TopBarHeight = 32;
            BottomBarHeight = 48;

            RightColumnWidth = 300;
            LeftColumnWidth = titleSafeArea.Width - RightColumnWidth;

            CardCloseUpHeight = (int)(RightColumnWidth / 0.7); // Card aspect ratio is: W = 0.7 * H;
            StatusHeight = titleSafeArea.Height - (TopBarHeight + CardCloseUpHeight + BottomBarHeight);

            CardListHeight = 256;
            ContentHeight = titleSafeArea.Height - (TopBarHeight + CardListHeight + BottomBarHeight);

            Menu = new Rectangle(LeftColumnWidth, 0, RightColumnWidth, TopBarHeight);
            CardCloseUp = new Rectangle(LeftColumnWidth, TopBarHeight, RightColumnWidth, CardCloseUpHeight);
            Status = new Rectangle(LeftColumnWidth, TopBarHeight + CardCloseUpHeight, RightColumnWidth, StatusHeight);
            Summary = new Rectangle(LeftColumnWidth, TopBarHeight + CardCloseUpHeight + StatusHeight, RightColumnWidth, BottomBarHeight);
            
            ToTheRunner = new Rectangle(0, 0, LeftColumnWidth, TopBarHeight);
            Content = new Rectangle(0, TopBarHeight, LeftColumnWidth, ContentHeight);
            CardList = new Rectangle(0, TopBarHeight + ContentHeight, LeftColumnWidth, CardListHeight);
            Console = new Rectangle(0, TopBarHeight + ContentHeight + CardListHeight, LeftColumnWidth, BottomBarHeight);
        }
    }
}
