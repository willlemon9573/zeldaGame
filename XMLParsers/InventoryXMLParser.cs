

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.InventoryFiles;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{

    /* NOTE: I'm considering making an XDocument Parser tools file for easy parsing of common xml files */

    /// <summary>
    /// A helper for parsing inventory xml files
    /// </summary>
    internal class InventoryXMLParser
    {
        /* ----------------------------- Attribute strings ----------------------------- */
        const string NAME_ATTRIBUTE = "name";
        const string DIRECTION_ATTRIBUTE = "direction";
        const string SPRITE_EFFECT_ATTRIBUTE = "spriteeffect";
        const string X_ATTRIBUTE = "x";
        const string Y_ATTRIBUTE = "y";
        const string ITEMS_ENUM_ATTRIBUTE = "items";
        const string KEY_VALUE_ATTRIBUTE = "kvp";
        const string TUPLE_ELEMENT = "tuple";
        const string STARTING_STOCK_ATTRIBUTE = "startingstock";
        const string MAX_STOCK_ATTRIBUTE = "maxstock";
        const string SPRITE_ATTRIBUTE = "sprite";
        const string PARAM_ELEMENT = "params";
        const string ITEM_ELEMENT = "item";

        /* ----------------------------- Private Members ----------------------------- */
        private readonly XDocument _inventoryDocument;

        /* ----------------------------- Private Functions (Might throw these in another file) ----------------------------- */

        /// <summary>
        /// Checks if the XMLElements enumerable is null, then checks each element and verifies they match
        /// the name given in expectedElementsName
        /// </summary>
        /// <param name="xmlElement">The specific element being checked</param>
        /// <param name="filePath">the file path of the xml file being parsed</param>
        /// <param name="expectedElementName">The expected name of the element being checked</param>
        /// <exception cref="Exception">Throws an exception if there are is no element</exception>
        private void CheckIfNull(XElement xmlElement, string expectedElementName)
        {
            if (xmlElement == null || xmlElement.Name != expectedElementName)
            {
                throw new Exception($"Error parsing {_inventoryDocument}: Missing Element {expectedElementName} or element is null");
            }
        }

        private void CheckAttribute(XAttribute attribute)
        {
            if (string.IsNullOrEmpty(attribute.Value))
            {
                throw new Exception($"Inventory XML file missing key attribute direction or direction value is null");
            }
        }
        /// <summary>
        /// Not exactly necessary....but...Parses attribute as a string
        /// </summary>
        /// <param name="attribute">The attribute to be parsed</param>
        /// <returns>The string form of the attribute value</returns>
        private string ParseAttributeAsString(XAttribute attribute)
        {
            CheckAttribute(attribute);
            return attribute.Value;
        }

        /// <summary>
        /// Parses Element for the value of the direction attribute
        /// </summary>
        /// <param name="keyElement">The element that contains a direction attribute</param>
        /// <returns>The content of the directio enum with its corresponding enum type</returns>
        private Direction ParseElementForDirection(XElement keyElement)
        {
            XAttribute direction = keyElement.Attribute(DIRECTION_ATTRIBUTE);
            CheckAttribute(direction);
            return (Direction)Enum.Parse(typeof(Direction), direction.Value, true);
        }

        /// <summary>
        /// Parses the given element for the key attribute
        /// </summary>
        /// <param name="itemElement"></param>
        /// <returns></returns>
        private Items ParseElementForItem(XElement itemElement)
        {
            XAttribute key = itemElement.Attribute(ITEMS_ENUM_ATTRIBUTE);
            CheckAttribute(key);
            return (Items)Enum.Parse(typeof(Items), key.Value, true);
        }

        /// <summary>
        /// Parses element for the value of the sprite effects
        /// </summary>
        /// <param name="keyElement">The element that contains the SpriteEffects attribute</param>
        /// <returns>The content of the sprite effects attribute as its correspond enum type</returns>
        private SpriteEffects ParseElementForSpriteEffects(XElement keyElement)
        {
            XAttribute spriteEffect = keyElement.Attribute(SPRITE_EFFECT_ATTRIBUTE);
            CheckAttribute(spriteEffect);
            return (SpriteEffects)Enum.Parse(typeof(SpriteEffects), spriteEffect.Value, true);
        }

        private ISprite ParseElementForNonAnimatedItemSprite(XElement element)
        {
            XAttribute spriteAttribute = element.Attribute(SPRITE_ATTRIBUTE);
            CheckAttribute(spriteAttribute);
            return ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(spriteAttribute.Value);
        }

        /// <summary>
        /// Parses the element for the given attribute and returns it as an int
        /// </summary>
        /// <param name="paramElement">the element that contains the attribute to be parsed</param>
        /// <param name="attributeName">the attribute to name to parse</param>
        /// <returns>an integer if successfull</returns>
        /// <exception cref="Exception">Throws exception if attribute cannot be parsed or is an invalid integer</exception>
        private int ParseElementForInt(XElement paramElement, string attributeName)
        {
            XAttribute intAttribute = paramElement.Attribute(attributeName);
            CheckAttribute(intAttribute);
            if (!int.TryParse(intAttribute.Value, out int result))
            {
                throw new Exception($"Invalid integer attribute or attribute is null");
            }
            return result;
        }

        /// <summary>
        /// Parses the given element for the attributes required to create the Tuple
        /// </summary>
        /// <param name="keyElement">The element containing the tuples</param>
        /// <returns>A Tuple containing the required sprite effects for flipping and the position offsets for the weapon</returns>
        private Tuple<SpriteEffects, Vector2> ParseElementForTuple(XElement keyElement)
        {
            CheckIfNull(keyElement, TUPLE_ELEMENT);
            SpriteEffects spriteEffects = ParseElementForSpriteEffects(keyElement);
            int x = ParseElementForInt(keyElement, X_ATTRIBUTE);
            int y = ParseElementForInt(keyElement, Y_ATTRIBUTE);
            return Tuple.Create(spriteEffects, new Vector2(x, y));
        }

        private IStackableItems ParseElementForStackableItems(XElement paramElement)
        {
            CheckIfNull(paramElement, PARAM_ELEMENT);
            int startingStock = ParseElementForInt(paramElement, STARTING_STOCK_ATTRIBUTE);
            int maxStock = ParseElementForInt(paramElement, MAX_STOCK_ATTRIBUTE);
            ISprite itemSprite = ParseElementForNonAnimatedItemSprite(paramElement);
            return new StackableItem(startingStock, maxStock, new InventoryEntityTest(), itemSprite);
        }

        /* ----------------------------- Public functions ----------------------------- */
        /// <summary>
        /// Create an Inventory XML parser for quick and easy parsing on load/death/reset. 
        /// Only to be used for loading inventories after a reset or on startup
        /// </summary>
        /// <param name="inventoryDocument">The document to be parsed</param>
        /// <param name="rootName">The root name of the document</param>
        public InventoryXMLParser(XDocument inventoryDocument, string rootName)
        {
            Debug.Assert(inventoryDocument.Root.Name == rootName, $"Inventory XML Root {inventoryDocument.Root.Name} did not match {rootName}");
            _inventoryDocument = inventoryDocument;
        }

        /// <summary>
        /// Parses the xml document for the player weapon
        /// </summary>
        /// <returns></returns>
        public IWeaponEntity ParsePlayerWeapon(string elementName)
        {
            XElement weaponElement = _inventoryDocument.Root.Element(elementName);
            CheckIfNull(weaponElement, elementName);
            String weaponName = ParseAttributeAsString(weaponElement.Attribute(NAME_ATTRIBUTE));
            Dictionary<Direction, Tuple<SpriteEffects, Vector2>> weaponEffects =
                weaponElement.Elements(KEY_VALUE_ATTRIBUTE).ToDictionary
                (
                        directionElement => ParseElementForDirection(directionElement),
                        directionChildElement => ParseElementForTuple(directionChildElement.Element(TUPLE_ELEMENT))
                );

            return new SwordEntity(weaponName, weaponEffects);
        }

        public Dictionary<Items, IStackableItems> ParseInitialStartingItems(string elementName)
        {
            XElement stackableItemsElement = _inventoryDocument.Root.Element(elementName);
            CheckIfNull(stackableItemsElement, elementName);
            return stackableItemsElement.Elements(ITEM_ELEMENT).ToDictionary
                (
                      itemElement => ParseElementForItem(itemElement),
                      itemChildElement => ParseElementForStackableItems(itemChildElement.Element(PARAM_ELEMENT))
                );
        }
    }
}
