using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.GameStateMenu
{
    /// <summary>
    /// Represents the post-game credits screen.
    /// This class extends GameStateAbstract and manages the display of the flashing effect, merging animation,
    /// and scrolling credits once the game is beaten.
    /// </summary>
    /// <author>Zihe Wang</author>
    public class GameBeatMenu : GameStateAbstract
    {
        // Flags to control the state of the menu.
        private bool isFlashing = true;
        private bool isMerging = false;
        private bool hasMerged = false;
        private bool shouldDrawFlashing = false;
        private bool isWaiting = false;

        // Timers to manage the timing of flashing, merging, and waiting periods.
        private float flashTimer = 0;
        private float mergeTimer = 0;
        private float waitTimer = 0;

        // Position for the merge animation.
        private float mergePosition = 0;

        // Counter for the number of flashes.
        private int flashCount = 0;

        // Constants for timing configurations.
        private const float flashMaxTime = 0.4f; // The interval of each flash.
        private const int flashAmount = 4; // The total number of flashes.
        private const float mergeFinishTime = 3.0f; // The duration of the merging animation.
        private const float waitTime = 2.0f; // The wait time between flashing and merging.

        // Constants for the overlay color.
        private const int RGB_BLACK = 225; // The RGB value for the white overlay color.
        private const int RGB_ALPHA = 60; // The alpha (transparency) value for the white overlay.

        // Position for the scrolling credits text.
        private float scrollPosition;

        // Text for the credits.
        private string creditsText = "Thank you \n for playing! \n Created by: EnterGame\n...\nTeamMember:\n Zihe Wang\n Aaron Heishman\n Muhammad Gheith\n Chih-Hsiang Jarek Tseng\n Will Lemon. ";

        // Font for the credits text.
        private SpriteFont creditsFont;

        // Flag to indicate if the credits have finished scrolling.
        private bool creditsRolled = false;


        public GameBeatMenu(Game1 game) : base(game)
        {
            // Set the overlay color and load the font.
            Color WhiteOverlay = new Color(RGB_BLACK, RGB_BLACK, RGB_BLACK, RGB_ALPHA);
            creditsFont = game.Content.Load<SpriteFont>("PauseSetting");
            scrollPosition = HEIGHT;
            _overlay.SetData(new[] { WhiteOverlay });
        }

        public override void Update(GameTime gameTime)
        {
            if (isFlashing)
            {
                updateFlashTime(gameTime);
            }

            if (isWaiting)
            {
                updateWaitingTime(gameTime);
            }

            if (isMerging)
            {
                updateMerge(gameTime);
            }
        }
        private void updateFlashTime(GameTime gameTime)
        {
            flashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (flashTimer >= flashMaxTime)
            {
                flashTimer = 0;
                flashCount++;
                shouldDrawFlashing = !shouldDrawFlashing;

                if (flashCount >= flashAmount)
                {
                    isFlashing = false;
                    isWaiting = true;
                    shouldDrawFlashing = false;
                }
            }
        }
        private void updateWaitingTime(GameTime gameTime)
        {
            waitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                isMerging = true;
                waitTimer = 0;
            }
        }

        private void updateMerge(GameTime gameTime)
        {
            mergeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            mergePosition = MathHelper.Lerp(0, WIDTH / 2, mergeTimer / mergeFinishTime);
            if (mergeTimer >= mergeFinishTime)
            {
                isMerging = false;
                hasMerged = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (shouldDrawFlashing)
            {
                spriteBatch.Draw(_overlay, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.11f);
            }

            if (isMerging)
            {
                Texture2D blackTexture = new Texture2D(_GraphicsDevice, 1, 1);
                blackTexture.SetData(new[] { Color.Black });
                spriteBatch.Draw(blackTexture, new Rectangle(0, 0, (int)mergePosition, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
                spriteBatch.Draw(blackTexture, new Rectangle(WIDTH - (int)mergePosition, 0, (int)mergePosition, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);
            }

            if (hasMerged)
            {
                Texture2D fullBlackTexture = new Texture2D(_GraphicsDevice, 1, 1);
                fullBlackTexture.SetData(new[] { Color.Black });
                spriteBatch.Draw(fullBlackTexture, new Rectangle(0, 0, WIDTH, HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01f);

                if (!creditsRolled) 
                {
                    Vector2 textPosition = new Vector2(20, scrollPosition);
                    spriteBatch.DrawString(creditsFont, creditsText, textPosition, Color.White,0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    scrollPosition -= 1;
                    if (scrollPosition < -creditsFont.MeasureString(creditsText).Y)
                    {
                        creditsRolled = true;
                    }
                }
            }

        }
    }
}
