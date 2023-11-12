using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;
using SprintZero1.Controllers;
using SprintZero1.Enums;
using SprintZero1.Entities.BowAndMagicFireEntity;
using SprintZero1.Entities;
using SprintZero1.Entities.BoomerangEntity;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameItemSelectionState : BaseGameState
    {
        private ItemSelectionMenu itemSelectionMenu;
        private IController controllerForItemSelection;
        public GameItemSelectionState(Game1 game) : base(game)
        {
            itemSelectionMenu = new ItemSelectionMenu(game, ProgramManager.player);
            IWeaponEntity newEquipment = new RegularBowEntity("Bow");
            PlayerInventoryManager.AddEquipmentItemToInventory(ProgramManager.player,EquipmentItem.Bow, newEquipment);
            IWeaponEntity newEquipment2 = new RegularBoomerangEntity("Boomerang", ProgramManager.player);
            PlayerInventoryManager.AddEquipmentItemToInventory(ProgramManager.player, EquipmentItem.Boomerang, newEquipment2);
            IWeaponEntity newEquipment3 = new BetterBowEntity("BetterBow");
            PlayerInventoryManager.UpgradeEquipment(ProgramManager.player, EquipmentItem.Bow, EquipmentItem.BetterBow, newEquipment3);
           /* IWeaponEntity newEquipment4 = new BetterBoomerangEntity("BetterBoomerang", ProgramManager.player);
            PlayerInventoryManager.UpgradeEquipment(ProgramManager.player, EquipmentItem.Boomerang, EquipmentItem.BetterBoomerang, newEquipment4);*/
            controllerForItemSelection = new KeyboardControllerForItemSelection(game, ProgramManager.player, itemSelectionMenu);

        }

        public override void Update(GameTime gameTime)
        {
            controllerForItemSelection.Update();
            itemSelectionMenu.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            itemSelectionMenu.Draw(spriteBatch);
        }

        public override void Handle()
        {
            (itemSelectionMenu as ItemSelectionMenu).SynchronizeInventory();
        }

    }
}
