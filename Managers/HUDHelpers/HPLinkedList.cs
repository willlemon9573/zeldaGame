using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System.Collections.Generic;

namespace SprintZero1.Managers.HUDHelpers
{
    internal class HPLinkedList
    {
        private class Node
        {
            private ISprite heartSprite;
            private Vector2 spritePosition;
            private float healthRep;
            private Node next;
            private Node previous;

            public ISprite HeartSprite { get { return heartSprite; } set { heartSprite = value; } }
            public Vector2 SpritePosition { get { return spritePosition; } set { spritePosition = value; } }
            public float HealthRep { get { return healthRep; } set { healthRep = value; } }
            public Node Next { get { return next; } set { next = value; } }
            public Node Previous { get { return previous; } set { previous = value; } }
        }

        private const float FullHeart = 1f;
        private const float EmptyHeart = 0f;
        private readonly Color _defaultColor; // default color for sprites
        private readonly Node _head;
        private readonly Node _tail;
        // _healthPointer node acts as the pointer to the current node that will be the first to be increased/decreased when taking damage
        private Node _healthPointer;
        private readonly int _horizontalOffset;
        private int _maxHealth;
        private static Dictionary<float, ISprite> _heartSpriteMap;

        /// <summary>
        /// Initializes the linked list
        /// </summary>
        /// <param name="startingPosition">The position that the hearts will start being drawn at</param>
        private void Initialize(Vector2 startingPosition)
        {
            Vector2 position = startingPosition;
            Node currentNode = _head;
            for (int i = 0; i < _maxHealth; i++)
            {
                Node newNode = new()
                {
                    HeartSprite = _heartSpriteMap[FullHeart],
                    SpritePosition = position,
                    HealthRep = FullHeart,
                    Next = currentNode.Next,
                    Previous = currentNode
                };

                currentNode.Next = newNode; // updates the current node to point at the new node
                currentNode = newNode; // moves to the new node
                position.X += _horizontalOffset;
            }
            currentNode.Next = _tail;
            _tail.Previous = currentNode;
            _healthPointer = currentNode;
        }

        /// <summary>
        /// Create a linked list that represents the player's health for the hud
        /// </summary>
        /// <param name="maxHealth">The total max health the player starts with</param>
        /// <param name="startingPosition">The position to start drawing the player's health at</param>
        /// <param name="horizontalOffset">The horizontal offset for adding new hearts [default param = 9]</param>
        public HPLinkedList(int maxHealth, Vector2 startingPosition, int horizontalOffset = 9)
        {
            /* Sprite map that contains an easy way to access sprites */
            _heartSpriteMap = new Dictionary<float, ISprite>()
            {
                {0f, HUDSpriteFactory.Instance.CreateHUDSprite("empty_heart") },
                {0.5f, HUDSpriteFactory.Instance.CreateHUDSprite("half_heart") },
                {1.0f, HUDSpriteFactory.Instance.CreateHUDSprite("full_heart") }
            };
            _horizontalOffset = horizontalOffset;
            /* Create list */
            _head = new Node();
            _tail = new Node();
            _head.Next = _tail;
            _tail.Previous = _head;
            _tail.Next = null;
            _head.Previous = null;
            _maxHealth = maxHealth;
            /* Initialize list */
            Initialize(startingPosition);
            _defaultColor = Color.White;
        }

        /// <summary>
        /// Increase the player health by one. For use when the player picks up a Heart Container
        /// </summary>
        public void IncreasePlayerHealth()
        {
            Node currentNode = _head.Next;

            /* Player gets full health back upon picking up heart container */
            while (currentNode != _tail)
            {
                if (currentNode.HealthRep != FullHeart)
                {
                    currentNode.HealthRep = FullHeart;
                    currentNode.HeartSprite = _heartSpriteMap[FullHeart];
                }
                currentNode = currentNode.Next;
            }

            /* setting up new node */
            currentNode = currentNode.Previous; // get previous node before tail
            Vector2 position = currentNode.SpritePosition;
            position.X += _horizontalOffset;

            /* Creating new node */
            Node newNode = new Node()
            {
                HeartSprite = _heartSpriteMap[FullHeart],
                SpritePosition = position,
                HealthRep = FullHeart,
                Next = _tail, /* this node is the last in list, so next will be tail */
                Previous = currentNode /* this node's previous node will be the current node */
            };

            /* update list to allow for new node to be inside the list */
            currentNode.Next = newNode;
            _tail.Previous = newNode;
            _maxHealth++; /* represents the length of the list */
            _healthPointer = newNode; /* update health pointer to the new node */
        }

        /// <summary>
        /// Decrement the on screen heart by the given amount
        /// </summary>
        /// <param name="amount">The total amount to decrement by. Must be in increments of 0.5f</param>
        public void DecrementCurrentHealth(float amount)
        {
            /* using health pointer so we don't have to iterate over what would be empty hearts */
            Node currentNode = _healthPointer;
            /* player should be dead, but just in case */
            if (currentNode.HealthRep == 0 && currentNode.Previous == _head) { return; }
            float tempAmount = amount;
            /* iterate towards head and decrement health as needed */
            while (currentNode != _head && tempAmount > 0)
            {
                float tempHealth = currentNode.HealthRep; // copy of temp health
                if (tempHealth != EmptyHeart)
                {
                    /* update heart based on whether amount to remove is larger or smaller than the current amount */
                    tempHealth = (tempHealth >= amount) ? EmptyHeart : (tempHealth - tempAmount);
                    /* update the current amount to be removed */
                    tempAmount -= currentNode.HealthRep;
                    /* update sprite and rep to reflect change */
                    currentNode.HealthRep = tempHealth;
                    currentNode.HeartSprite = _heartSpriteMap[currentNode.HealthRep];
                }
                currentNode = currentNode.Previous;
                // tracking the current health node for damage/healing
                if (currentNode != _head)
                {
                    _healthPointer = currentNode;
                }
            }
        }

        /// <summary>
        /// Increments the on screen heart by the given amount
        /// </summary>
        /// <param name="amount">The total amount to increment by. Must be in increments of 0.5f</param>
        public void IncrementCurrentHealth(float amount)
        {
            /* TODO: Fix bug here */
            /* using health pointer so we don't have to iterate over full hearts */
            Node currentNode = _healthPointer;
            // copy of the amount to increment
            float tempAmount = amount;
            /* iterate towards head and decrement health as needed */
            while (currentNode != _tail && tempAmount > 0)
            {
                float tempHealth = currentNode.HealthRep;
                // move to next heart if current heart is full
                if (tempHealth != FullHeart)
                {
                    // check if current health + amount is greater than a full heart
                    tempHealth = (tempHealth + amount) > FullHeart ? FullHeart : tempHealth + amount;
                    // update sprite and rep to reflect health change
                    currentNode.HealthRep = tempHealth;
                    // update tempAmount to reflect change in health
                    tempAmount -= currentNode.HealthRep;
                    currentNode.HeartSprite = _heartSpriteMap[currentNode.HealthRep];
                }
                // check next node
                currentNode = currentNode.Next;
                if (currentNode != _tail)
                {
                    _healthPointer = currentNode;
                }
            }
        }

        /// <summary>
        /// Draws each of the hearts for the hud.
        /// </summary>
        /// <param name="spriteBatch">The current batch of sprites being drawn</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            /* Start at the node to the right of head and 
             * draw each sprite until the tail is reached */
            Node n = _head.Next;
            while (n.Next != null)
            {
                ISprite heart = n.HeartSprite;
                Vector2 position = n.SpritePosition;
                heart.Draw(spriteBatch, position, _defaultColor);
                n = n.Next;
            }
        }
    }
}
