using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using Size = System.Drawing.Size;

namespace SprintZero1.Colliders
{
    internal class DynamicCollider : ICollider
    {
        Rectangle _collider;

        public int Delta { get { return _delta; } set { _delta = value; } }
        private int _delta;
        public Rectangle Collider { get { return _collider; } set { _collider = value; } }

        private readonly int _offsetX;
        private int _offsetY;
        Size _colliderDimensions;
        private readonly float _scaleFactor;

        /// <summary>
        /// Initializes a new instance of the DynamicCollider class.
        /// </summary>
        /// <param name="position">The initial position of the collider in 2D space.</param>
        /// <param name="dimensions">The size of the collider.</param>
        /// <param name="scaleFactor">The scale factor of the collider, defaulting to 0.8f.</param>
        /// <param name="offsetX">The horizontal offset of the collider, defaulting to 0.</param>
        /// <param name="offsetY">The vertical offset of the collider, defaulting to 0.</param>
        public DynamicCollider(Vector2 position, Size dimensions, float scaleFactor = 1f, int offsetX = 0, int offsetY = 0)
        {
            _scaleFactor = scaleFactor;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _colliderDimensions = new Size((int)(dimensions.Width * _scaleFactor), (int)(dimensions.Height * _scaleFactor));
            _collider = new Rectangle((int)position.X - (_colliderDimensions.Width / 2) + _offsetX, (int)position.Y - (_colliderDimensions.Height / 2) + _offsetY, _colliderDimensions.Width, _colliderDimensions.Height);
        }

        /// <summary>
        /// Updates this collider
        /// </summary>
        /// <param name="parent"></param>
        public void Update(IEntity parent)
        {
            _collider.X = (int)parent.Position.X - (_colliderDimensions.Width / 2) + _offsetX;
            _collider.Y = (int)parent.Position.Y - (_colliderDimensions.Height / 2) + _offsetY;
        }
    }
}
