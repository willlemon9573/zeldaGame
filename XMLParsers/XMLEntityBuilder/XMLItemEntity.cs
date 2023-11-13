using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLItemEntity : EntityBase
    {
        private const string EntityNameSpace = "SprintZero1.Entities.LootableItemEntity";
        private int _itemFrames;
        private static string _enumName;
        private string _itemType;
        private DungeonRoom _room;
        public int ItemFrames { set => _itemFrames = value; }
        public string EnumName { set => _enumName = value; }
        public string ItemType { set => _itemType = value; }
        public DungeonRoom DungeonRoom { set => _room = value; }


        private readonly Dictionary<string, Action> delegateMap = new Dictionary<string, Action>()
        {
            { "StackableItemEntity", () => GetStackableItemHandler() },
            { "DungeonItemEntity", () => GetUtilityItemHandler() },
            { "EquipmentItemEntity", () => GetEquipmentItemHandler() },
        };

        private readonly Dictionary<string, Action<Enum>> enumMap = new Dictionary<string, Action<Enum>>()
        {
            { "StackableItemEntity", item =>  item = (DungeonItems)Enum.Parse(typeof(EquipmentItem), _enumName, true) },
            { "EquipmentItemEntity", item => item = (EquipmentItem)Enum.Parse(typeof(EquipmentItem), _enumName, true) },
            { "DungeonItemEntity", item => item = (EquipmentItem)Enum.Parse(typeof(EquipmentItem), _enumName, true)}
        };


        private static StackableItemHandler GetStackableItemHandler()
        {
            return PlayerInventoryManager.AddStackableItemToInventory;
        }

        private static UtilityItemHandler GetUtilityItemHandler()
        {
            return PlayerInventoryManager.AddUtilityItemToInventory;
        }

        private static EquipmentItemHandler GetEquipmentItemHandler()
        {

            return PlayerInventoryManager.AddEquipmentItemToInventory;
        }



        /// <summary>
        /// Create the item entity
        /// </summary>
        /// <returns>a new instance of the item entity</returns>
        public override IEntity CreateEntity()
        {
            ISprite itemSprite = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            RemoveDelegate itemRemval = _room.RemoveItem;
            Type entityType = Type.GetType($"{EntityNameSpace}.{_itemType}");
            object instance = Activator.CreateInstance(entityType, itemSprite, position, itemRemval, delegateMap[_itemType], enumMap[_itemType]);
            // using levelBlockEntity until we make something of items
            return (IEntity)instance;
        }

        // technically shouldn't be in here, but will be removed soon
        public IEntity CreateAnimatedEntity()
        {
            ISprite itemSprite = ItemSpriteFactory.Instance.CreateAnimatedItemSprite(_entityName, _itemFrames);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);
            Rectangle dimensions = ItemSpriteFactory.Instance.GetAnimatedSpriteDimensions(_entityName);
            // using levelBlockEntity until we make something ofr items
            return new FireTrapEntity(itemSprite, position, dimensions);
        }
    }
}
