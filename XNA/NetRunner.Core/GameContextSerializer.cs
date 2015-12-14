using NetRunner.Core.Corporation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NetRunner.Core
{
    /// <summary>
    /// Allows a GameContext object to be written to and from a string of XML.
    /// This will be used when the hosted game needs to tell a client about the game
    /// when they first join or if they need to resync.
    /// It can also be used to write a game to disk for saving and loading.
    /// </summary>
    public class GameContextSerializer
    {
        #region Serialization

        public string Serialize(GameContext context, PlayerType playerType)
        {
            XElement xGameContext = SerializeGameContext(context, playerType);

            return xGameContext.ToString();
        }

        private XElement SerializeGameContext(GameContext context, PlayerType playerType)
        {
            XElement xGameContext = new XElement("GameContext");

            xGameContext.SetAttributeValue("runnerCredits", context.RunnerCredits);
            xGameContext.SetAttributeValue("corporationCredits", context.CorporationCredits);
            xGameContext.SetAttributeValue("runnerClicks", context.RunnerClicks);
            xGameContext.SetAttributeValue("corporationClicks", context.CorporationClicks);

            XElement xResearchAndDevelopment = SerializeResearchAndDevelopment(context.ResearchAndDevelopment, playerType);
            xGameContext.Add(xResearchAndDevelopment);

            XElement xHeadQuarters = SerializeHeadQuarters(context.HeadQuarters, playerType);
            xGameContext.Add(xHeadQuarters);
            // TODO

            return xGameContext;
        }

        private XElement SerializeResearchAndDevelopment(ResearchAndDevelopment researchAndDevelopment, PlayerType playerType)
        {
            XElement xResearchAndDevelopment = new XElement("ResearchAndDevelopment");

            XElement xDrawPile = new XElement("DrawPile");
            foreach (Card card in researchAndDevelopment.DrawPile)
            {
                XElement xCard = SerializeCard(card, playerType);
                xDrawPile.Add(xCard);
            }
            xResearchAndDevelopment.Add(xDrawPile);

            XElement xIce = new XElement("Ice");
            foreach (Card card in researchAndDevelopment.Ice)
            {
                XElement xCard = SerializeCard(card, playerType);
                xIce.Add(card);
            }
            xResearchAndDevelopment.Add(xIce);

            XElement xUpgrades = new XElement("Upgrades");
            foreach (Card card in researchAndDevelopment.Upgrades)
            {
                XElement xCard = SerializeCard(card, playerType);
                xUpgrades.Add(card);
            }
            xResearchAndDevelopment.Add(xUpgrades);

            return xResearchAndDevelopment;
        }

        private XElement SerializeHeadQuarters(HeadQuarters headQuarters, PlayerType playerType)
        {
            XElement xHeadQuarters = new XElement("HeadQuarters");

            XElement xFactionCard = SerializeCard(headQuarters.FactionCard, playerType);
            xHeadQuarters.Add(xFactionCard);

            XElement xHand = new XElement("Hand");
            foreach (Card card in headQuarters.Hand)
            {
                XElement xCard = SerializeCard(card, playerType);
                xHand.Add(xCard);
            }
            xHeadQuarters.Add(xHand);

            XElement xIce = new XElement("Ice");
            foreach (Card card in headQuarters.Ice)
            {
                XElement xCard = SerializeCard(card, playerType);
                xIce.Add(card);
            }
            xHeadQuarters.Add(xIce);

            XElement xUpgrades = new XElement("Upgrades");
            foreach (Card card in headQuarters.Upgrades)
            {
                XElement xCard = SerializeCard(card, playerType);
                xUpgrades.Add(card);
            }
            xHeadQuarters.Add(xUpgrades);

            return xHeadQuarters;
        }

        private XElement SerializeCard(Card card, PlayerType playerType)
        {
            XElement xCard = new XElement("Card");

            string playerTypeString = card.PlayerType.ToString();
            xCard.SetAttributeValue("playerType", playerTypeString);

            xCard.SetAttributeValue("hostedAgendaTokens", card.HostedAgendaTokens);
            xCard.SetAttributeValue("hostedVirusTokens", card.HostedVirusTokens);

            xCard.SetAttributeValue("knownToRunner", card.KnownToRunner);
            xCard.SetAttributeValue("knownToCorporation", card.KnownToCorporation);

            if ((playerType == PlayerType.Corporation && card.KnownToCorporation)
                || (playerType == PlayerType.Runner && card.KnownToRunner))
            {
                string cardSetString = card.Behaviour.CardBehaviourID.CardSet.ToString();
                xCard.SetAttributeValue("cardSet", cardSetString);

                xCard.SetAttributeValue("id", card.Behaviour.CardBehaviourID.ID);
            }

            // TODO: Hosted cards

            return xCard;
        }

        #endregion

        #region Deserialization

        public GameContext Deserialize(string serializedGameContext)
        {
            XElement xContext = XElement.Parse(serializedGameContext);
            
            GameContext context = new GameContext();

            DeserializeContext(context, xContext);

            return context;
        }

        private void DeserializeContext(GameContext context, XElement xContext)
        {
            context.RunnerCredits = int.Parse(xContext.Attribute("runnerCredits").Value);
            context.CorporationCredits = int.Parse(xContext.Attribute("corporationCredits").Value);
            context.RunnerClicks = int.Parse(xContext.Attribute("runnerClicks").Value);
            context.CorporationClicks = int.Parse(xContext.Attribute("corporationClicks").Value);

            XElement xResearchAndDevelopment = xContext.Element("ResearchAndDevelopment");
            DeserializeResearchAndDevelopment(context.ResearchAndDevelopment, xResearchAndDevelopment);

            XElement xHeadQuarters = xContext.Element("HeadQuarters");
            DeserializeHeadQuarters(context.HeadQuarters, xHeadQuarters);

            //TODO:
        }

        private void DeserializeResearchAndDevelopment(ResearchAndDevelopment researchAndDevelopment, XElement xResearchAndDevelopment)
        {
            XElement xDrawPile = xResearchAndDevelopment.Element("DrawPile");
            foreach (XElement xCard in xDrawPile.Elements("Card"))
            {
                Card card = DeserializeCard(xCard);
                researchAndDevelopment.AddCard(card);
            }
        }

        private void DeserializeHeadQuarters(HeadQuarters headQuarters, XElement xHeadQuarters)
        {
            XElement xFactionCard = xHeadQuarters.Element("Card");
            headQuarters.FactionCard = DeserializeCard(xFactionCard);
            
            XElement xHand = xHeadQuarters.Element("Hand");
            foreach (XElement xCard in xHand.Elements("Card"))
            {
                Card card = DeserializeCard(xCard);
                headQuarters.Hand.Add(card);
            }

            XElement xIce = xHeadQuarters.Element("Ice");
            foreach (XElement xCard in xIce.Elements("Card"))
            {
                Card card = DeserializeCard(xCard);
                headQuarters.Ice.Add(card);
            }

            XElement xUpgrades = xHeadQuarters.Element("Upgrades");
            foreach (XElement xCard in xUpgrades.Elements("Card"))
            {
                Card card = DeserializeCard(xCard);
                headQuarters.Upgrades.Add(card);
            }
        }

        private Card DeserializeCard(XElement xCard)
        {
            string playerTypeString = xCard.Attribute("playerType").Value;
            PlayerType playerType = (PlayerType)Enum.Parse(typeof(PlayerType), playerTypeString);

            Card card = new Card(playerType);

            string hostedAgendaTokensString = xCard.Attribute("hostedAgendaTokens").Value;
            int hostedAgendaTokens = int.Parse(hostedAgendaTokensString);
            card.HostedAgendaTokens = hostedAgendaTokens;

            string hostedVirusTokensString = xCard.Attribute("hostedVirusTokens").Value;
            int hostedVirusTokens = int.Parse(hostedVirusTokensString);
            card.HostedVirusTokens = hostedVirusTokens;

            string knownToRunnerString = xCard.Attribute("knownToRunner").Value;
            bool knownToRunner = bool.Parse(knownToRunnerString);
            card.KnownToRunner = knownToRunner;

            string knownToCorporationString = xCard.Attribute("knownToCorporation").Value;
            bool knownToCorporation = bool.Parse(knownToCorporationString);
            card.KnownToCorporation = knownToCorporation;

            if (xCard.Attribute("cardSet") != null && xCard.Attribute("id") != null)
            {
                string cardSetString = xCard.Attribute("cardSet").Value;
                CardSet cardSet = (CardSet)Enum.Parse(typeof(CardSet), cardSetString);

                string idString = xCard.Attribute("id").Value;
                int id = int.Parse(idString);

                card.IdentifyCard(new CardBehaviourID(cardSet, id));
            }

            //TODO: Hosted cards

            return card;
        }

        #endregion
    }
}
