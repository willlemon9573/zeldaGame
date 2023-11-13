

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
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
    /// @Author Aaron Heishman
    /// </summary>
    internal class InventoryXMLParser
    {
        /* ----------------------------- Attribute strings ----------------------------- */
        const string NAME_ATTRIBUTE = "name";
        const string X_ATTRIBUTE = "x";
        const string Y_ATTRIBUTE = "y";
        const string ITEMS_ENUM_ATTRIBUTE = "items";
        const string KEY_VALUE_ATTRIBUTE = "kvp";
        const string DIRECTION_ATTRIBUTE = "direction";
        const string TUPLE_ELEMENT = "tuple";
        const string STARTING_STOCK_ATTRIBUTE = "startingstock";
        const string MAX_STOCK_ATTRIBUTE = "maxstock";
        const string SPRITE_ATTRIBUTE = "sprite";
        const string SPRITE_EFFECTS_ATTRIBUTE = "spriteeffect";
        const string PARAM_ELEMENT = "params";
        const string ITEM_ELEMENT = "item";

        /* ----------------------------- Private Members ----------------------------- */
        private readonly XDocument _inventoryDocument;
        private readonly XDocTools _parseTools;

        /* ----------------------------- Private Functions  ----------------------------- */

        /// <summary>
        /// Parses the given element for the attributes required to create the Tuple
        /// </summary>
        /// <param name="keyElement">The element containing the tuples</param>
        /// <returns>A Tuple containing the required sprite effects for flipping and the position offsets for the weapon</returns>
        private Tuple<SpriteEffects, Vector2> CreateWeaponSpriteEffectsTuple(XElement keyElement)
        {
            SpriteEffects spriteEffects = _parseTools.ParseAttributeAsSpriteEffect(keyElement, SPRITE_EFFECTS_ATTRIBUTE);
            int x = _parseTools.ParseAttributeAsInt(keyElement, X_ATTRIBUTE);
            int y = _parseTools.ParseAttributeAsInt(keyElement, Y_ATTRIBUTE);
            return Tuple.Create(spriteEffects, new Vector2(x, y));
        }
        /// <summary>
        /// Parses the given element for the attributes required to create a stack aitem object
        /// </summary>
        /// <param name="paramElement">the element to parse</param>
        /// <returns>the stackable item object</returns>
        private IStackableItems CreateStackableItemObject(XElement paramElement)
        {
            _parseTools.CheckIfElementNull(paramElement, PARAM_ELEMENT);
            int startingStock = _parseTools.ParseAttributeAsInt(paramElement, STARTING_STOCK_ATTRIBUTE);
            int maxStock = _parseTools.ParseAttributeAsInt(paramElement, MAX_STOCK_ATTRIBUTE);
            ISprite itemSprite = _parseTools.ParseNonAnimatedItemSprite(paramElement, SPRITE_ATTRIBUTE);
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
            Debug.Assert(inventoryDocument.Root.Name == rootName, $"Inventory XML Root {inventoryDocument} did not match {rootName}");
            _inventoryDocument = inventoryDocument;
            _parseTools = new XDocTools();
        }

        /// <summary>
        /// Parses player weapon informnation
        /// </summary>
        /// <param name="weaponElementName">The name of the element that is the "root" of the weapon information</param>
        /// <returns>A weapon entity with the sword information</returns>
        public IWeaponEntity ParsePlayerWeapon(string weaponElementName)
        {
            XElement weaponElement = _inventoryDocument.Root.Element(weaponElementName);
            _parseTools.CheckIfElementNull(weaponElement, weaponElementName);
            String weaponName = _parseTools.ParseAttributeAsString(weaponElement.Attribute(NAME_ATTRIBUTE));
            Dictionary<Direction, Tuple<SpriteEffects, Vector2>> weaponEffects =
                weaponElement.Elements(KEY_VALUE_ATTRIBUTE).ToDictionary
                (
                        directionElement => _parseTools.ParseAttributeAsDirection(directionElement, DIRECTION_ATTRIBUTE),
                        directionChildElement => CreateWeaponSpriteEffectsTuple(directionChildElement.Element(TUPLE_ELEMENT))
                );

            return new SwordEntity(weaponName, weaponEffects);
        }

        /// <summary>
        /// Parses the xml document for The player initial starting items.
        /// </summary>
        /// <param name="stackableItemElement">The name of the element that is the "root" of stackable items</param>
        /// <returns>A dictionary filled with keys and stackable items for easy lookup</returns>
        public Dictionary<StackableItems, IStackableItems> ParseInitialStartingItems(string stackableItemElement)
        {
            XElement stackableItemsElement = _inventoryDocument.Root.Element(stackableItemElement);
            _parseTools.CheckIfElementNull(stackableItemsElement, stackableItemElement);
            return stackableItemsElement.Elements(ITEM_ELEMENT).ToDictionary
                (
                      itemElement => _parseTools.ParseAttributeAsStackableItem(itemElement, ITEMS_ENUM_ATTRIBUTE),
                      itemChildElement => CreateStackableItemObject(itemChildElement.Element(PARAM_ELEMENT))
                );
        }
    }
}
