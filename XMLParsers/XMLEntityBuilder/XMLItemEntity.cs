using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Entities.LootableItemEntity;
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
        private const string StackableItem = "StackableItemEntity";
        private const string EquipmenItem = "EquipmentItemWithoutPlayerEntity";
        private const string DungeonItem = "DungeonItemEntity";
        private const string HeartContainer = "HeartContainerEntity";
        private const string TriforcePiece = "TriforceEntity";
        private const string EntityNameSpace = "SprintZero1.Entities.LootableItemEntity";
        private int _itemFrames;
        private string _enumName;
        private string _itemType;
        private RemoveDelegate _removeDelegate;
        public int ItemFrames { set => _itemFrames = value; }
        public string EnumName { set => _enumName = value; }
        public string ItemType { set => _itemType = value; }
        public RemoveDelegate RemoveDelegateHandler { set { _removeDelegate = value; } }

        private readonly Dictionary<string, Func<ILootableEntity>> entityCreationMethods;

        /// <summary>
        /// This specific function creates entities that do not require a delegate
        /// </summary>
        /// <returns></returns>
        private ILootableEntity CreateMiscEntity()
        {
            return (ILootableEntity)Activator.CreateInstance(CreateEntityType(), CreateSprite(), CreatePosition(), _removeDelegate);
        }
        private ILootableEntity CreateStackableEntity()
        {
            StackableItemHandler stackableItemhandler = PlayerInventoryManager.AddStackableItemToInventory;
            StackableItems dungeonItem = (StackableItems)Enum.Parse(typeof(StackableItems), _enumName, true);
            return (ILootableEntity)Activator.CreateInstance(CreateEntityType(), CreateSprite(), CreatePosition(), _removeDelegate, stackableItemhandler, dungeonItem);
        }

        private ILootableEntity CreateEquipmentEntity()
        {
            EquipmentItemHandler equipmentItemhandler = PlayerInventoryManager.AddEquipmentItemToInventory;
            EquipmentItem equipmentItem = (EquipmentItem)Enum.Parse(typeof(EquipmentItem), _enumName, true);
            return (ILootableEntity)Activator.CreateInstance(CreateEntityType(), CreateSprite(), CreatePosition(), _removeDelegate, equipmentItemhandler, equipmentItem);
        }

        private ILootableEntity CreateDungeonItemEntity()
        {
            UtilityItemHandler utilityItemHandler = PlayerInventoryManager.AddUtilityItemToInventory;
            DungeonItems dungeonItem = (DungeonItems)Enum.Parse(typeof(DungeonItems), _enumName, true);
            return (ILootableEntity)Activator.CreateInstance(CreateEntityType(), CreateSprite(), CreatePosition(), _removeDelegate, utilityItemHandler, dungeonItem);
        }

        private ISprite CreateSprite()
        {
            return ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite(_entityName.ToLower());
        }

        private Vector2 CreatePosition()
        {
            return new Vector2(_entityPositionX, _entityPositionY);
        }

        private Type CreateEntityType()
        {
            return Type.GetType($"{EntityNameSpace}.{_itemType}");
        }

        public XMLItemEntity()
        {
            entityCreationMethods = new Dictionary<string, Func<ILootableEntity>>()
            {
                { StackableItem, this.CreateStackableEntity },
                { DungeonItem, this.CreateDungeonItemEntity },
                { EquipmenItem, this.CreateEquipmentEntity },
                { HeartContainer, this.CreateMiscEntity },
                { TriforcePiece, this.CreateMiscEntity }
            };
        }

        /// <summary>
        /// Create the item entity
        /// </summary>
        /// <returns>a new instance of the item entity</returns>
        public override IEntity CreateEntity()
        {
            return entityCreationMethods[_itemType].Invoke();
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
