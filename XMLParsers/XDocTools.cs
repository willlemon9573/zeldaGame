using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;
using System.Xml.Linq;

namespace SprintZero1.XMLParsers
{
    /// <summary>
    /// Container for methods to parse files that only need to be loaded at the start of the game
    /// @author Aaron Heishman
    /// </summary>
    internal class XDocTools
    {
        /* ----------------------------- null checks ----------------------------- */

        /// <summary>
        /// Check if an atribute is null
        /// </summary>
        /// <param name="attribute">The attribute to check</param>
        /// <exception cref="Exception">Throws an exception if attribute is null</exception>
        public void CheckAttribute(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrEmpty(attribute.Value))
            {
                throw new Exception($"Inventory XML file missing key attribute direction or direction value is null");
            }
        }

        /// <summary>
        /// Checks if the XMLElements enumerable is null, then checks each element and verifies they match
        /// the name given in expectedElementsName
        /// </summary>
        /// <param name="xmlElement">The element to check</param>
        /// <param name="expectedElementName">The expected name of the element</param>
        /// <exception cref="Exception">
        /// Throws an exception if the element is null or if the element's name does not match the expected element name
        /// </exception>
        public void CheckIfElementNull(XElement xmlElement, string expectedElementName)
        {
            if (xmlElement == null || xmlElement.Name != expectedElementName)
            {
                throw new Exception($"Error parsing file: Elemen name did not match {expectedElementName} or element is null");
            }
        }

        /* ----------------------------- Attribute Parsing ----------------------------- */

        /// <summary>
        /// Not exactly necessary....but...Parses attribute as a string
        /// </summary>
        /// <param name="attribute">The attribute to be parsed</param>
        /// <returns>The string form of the attribute value</returns>
        public string ParseAttributeAsString(XAttribute attribute)
        {
            CheckAttribute(attribute);
            return attribute.Value;
        }

        /// <summary>
        /// Parses a direction enum from the given the element
        /// </summary>
        /// <param name="element">The element that contains the attribute to parse</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>A Direction Enum</returns>
        public Direction ParseAttributeAsDirection(XElement element, string attributeName)
        {
            XAttribute direction_attribute = element.Attribute(attributeName);
            CheckAttribute(direction_attribute);
            return (Direction)Enum.Parse(typeof(Direction), direction_attribute.Value, true);
        }


        /// <summary>
        /// Parses a direction enum from the given the element
        /// </summary>
        /// <param name="element">The element that contains the attribute to parse</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>A stackable item enum</returns>
        public StackableItems ParseAttributeAsStackableItem(XElement element, string attributeName)
        {
            XAttribute itemAttribute = element.Attribute(attributeName);
            CheckAttribute(itemAttribute);
            return (StackableItems)Enum.Parse(typeof(StackableItems), itemAttribute.Value, true);
        }

        public Rectangle CreateRectangle(XElement rectangleElement)
        {
            int x = ParseAttributeAsInt(rectangleElement, "x");
            int y = ParseAttributeAsInt(rectangleElement, "y");
            int width = ParseAttributeAsInt(rectangleElement, "width");
            int height = ParseAttributeAsInt(rectangleElement, "height");
            return new Rectangle(x, y, width, height);
        }

        public EquipmentItem ParseAttributeAsEquipmentItem(XElement element, string attributeName)
        {
            XAttribute key = element.Attribute(attributeName);
            CheckAttribute(key);
            return (EquipmentItem)Enum.Parse(typeof(EquipmentItem), key.Value, true);
        }
        /// <summary>
        /// Parses a Sprite Effect enum from the given element
        /// </summary>
        /// <param name="element">The element that contains the SpriteEffects attribute</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>The content of the sprite effects attribute as its correspond enum type</returns>
        public SpriteEffects ParseAttributeAsSpriteEffect(XElement element, string attributeName)
        {
            XAttribute spriteEffectAttribute = element.Attribute(attributeName);
            CheckAttribute(spriteEffectAttribute);
            return (SpriteEffects)Enum.Parse(typeof(SpriteEffects), spriteEffectAttribute.Value, true);
        }

        /// <summary>
        /// Parses a non animated item sprite from the given element
        /// </summary>
        /// <param name="element">The element that contains the Sprite attribute</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>The content of the sprite effects attribute as its correspond enum type</returns>
        public ISprite ParseNonAnimatedItemSprite(XElement element, string attributeName)
        {
            XAttribute spriteAttribute = element.Attribute(attributeName);
            CheckAttribute(spriteAttribute);
            return ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(spriteAttribute.Value);
        }

        /// <summary>
        /// Parses an integer value from the given element
        /// </summary>
        /// <param name="element">the element that contains the attribute to be parsed</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>an integer if successful</returns>
        /// <exception cref="Exception">Throws exception if attribute cannot be parsed or is an invalid integer</exception>
        public int ParseAttributeAsInt(XElement element, string attributeName)
        {
            XAttribute intAttribute = element.Attribute(attributeName);
            CheckAttribute(intAttribute);
            if (!int.TryParse(intAttribute.Value, out int result))
            {
                throw new Exception($"Invalid integer attribute or attribute is null");
            }
            return result;
        }

        /// <summary>
        /// Parses a keyboard keys enum from the given element
        /// </summary>
        /// <param name="element">The element that contains the attribute to be parses</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>A Keys enum</returns>
        public Keys ParseAttributeAsKeys(XElement element, string attributeName)
        {
            XAttribute keys = element.Attribute(attributeName);
            CheckAttribute(keys);
            return (Keys)Enum.Parse(typeof(Keys), keys.Value);
        }

        /// <summary>
        /// Parses a gamepad buttons enum from the given element
        /// </summary>
        /// <param name="element">The element that contains the attribute to be parses</param>
        /// <param name="attributeName">The name of the attribute that contains the value being parsed</param>
        /// <returns>A Button enum</returns>
        public Buttons ParseAttributeAsButton(XElement element, string attributeName)
        {
            XAttribute buttonAttribute = element.Attribute(attributeName);
            CheckAttribute(buttonAttribute);
            return (Buttons)Enum.Parse(typeof(Buttons), buttonAttribute.Value, true);
        }

        /// <summary>
        /// Parses a Rectangle from the given XElement
        /// </summary>
        /// <param name="rectangleElement">The element containing the attirbutes to parse</param>
        /// <returns>a Rectangle with the correct values</returns>
        public Rectangle ParseRectangleElement(XElement rectangleElement)
        {
            int x = ParseAttributeAsInt(rectangleElement, "x");
            int y = ParseAttributeAsInt(rectangleElement, "y");
            int width = ParseAttributeAsInt(rectangleElement, "width");
            int height = ParseAttributeAsInt(rectangleElement, "height");
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Parse a Vector2 from the given XElement
        /// </summary>
        /// <param name="vectorElement">The element that contains the attributes to parse</param>
        /// <returns>A new instance of a vector2 object</returns>
        public Vector2 ParseVector2Element(XElement vectorElement)
        {
            int x = ParseAttributeAsInt(vectorElement, "x");
            int y = ParseAttributeAsInt(vectorElement, "y");
            return new Vector2(x, y);
        }

        /// <summary>
        ///  Parses an ICommand value related to player action commands from the given element
        /// </summary>
        /// <param name="element">The element that contains the attribute for parsing</param>
        /// <param name="attributeName">the attribute that contains the value being parsed</param>
        /// <param name="nameSpace">The specific namespace of the class file being instantiated</param>
        /// <param name="player">The player that will be using the command</param>
        /// <returns>An instance of a class as an ICommand representing a player action command</returns>
        public ICommand ParsePlayerActionCommands(XElement element, string attributeName, string nameSpace, ICombatEntity player)
        {
            XAttribute command = element.Attribute(attributeName);
            CheckAttribute(command);
            return (ICommand)Activator.CreateInstance(Type.GetType($"{nameSpace}.{command.Value}"), player);
        }

        /// <summary>
        ///  Parses an ICommand value related to a menu action from the given element
        /// </summary>
        /// <param name="element">The element that contains the attribute for parsing</param>
        /// <param name="attributeName">the attribute that contains the value being parsed</param>
        /// <param name="nameSpace">The specific namespace of the class file being instantiated</param>
        /// <param name="game">The player that will be using the command</param>
        /// <returns>An instance of a class as an ICommand representing a player menu command</returns>
        public ICommand ParsePlayerMenuCommands(XElement element, string attributeName, string nameSpace, Game1 game)
        {
            XAttribute command = element.Attribute(attributeName);
            CheckAttribute(command);
            return (ICommand)Activator.CreateInstance(Type.GetType($"{nameSpace}.{command.Value}"), game);
        }
    }
}
