using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Win32.SafeHandles;

namespace Mas;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _food;
    private Texture2D _snake;

    private int cellSize = 40; // the size of the cell
    private int Columns = 0;
    private int Rows = 0;

    private int currentFoodCellX = 0;

    private int currentFoodCellY = 0;
    
    
    private int currentSnakeCellX = 0;

    private int currentSnakeCellY = 0;
    
    

    private int dx = 0;
    private int dy = 0;


    private bool foodIsEaten = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1600; // this is in pixels right
        // so how many columns can we have from this
        Columns = _graphics.PreferredBackBufferWidth / cellSize;
        _graphics.PreferredBackBufferHeight = 800;
        Rows = _graphics.PreferredBackBufferHeight / cellSize;
        (currentFoodCellX, currentFoodCellY) = AssignRandomPosition();
        (currentSnakeCellX, currentSnakeCellY) = AssignRandomPosition();
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _food = new Texture2D(GraphicsDevice, 1, 1);
        _food.SetData(new[] {Color.Firebrick});

        _snake = new Texture2D(GraphicsDevice, 1, 1);
        _snake.SetData(new[] {Color.Aquamarine});
        // these two lines above makes the image.
    }

    private double elapsedTime = 0;
    protected override void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        var keyboardState = Keyboard.GetState();

        if (elapsedTime > 150) // the elapsed time should be more than  150 ms, if it is then we can go ahead and do ahead 
        {
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if(currentSnakeCellX < Columns -1)
                    currentSnakeCellX++;
                elapsedTime = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if(currentSnakeCellX > 0)
                    currentSnakeCellX--;
                elapsedTime = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if(currentSnakeCellY < Rows -1)
                    currentSnakeCellY++;
                elapsedTime = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if(currentSnakeCellY > 0)
                    currentSnakeCellY--;
                elapsedTime = 0;
            }

            if (currentFoodCellX == currentSnakeCellX && currentFoodCellY == currentSnakeCellY)
            {
                // this is where we make the food disappear and create another food object;
                (currentFoodCellX, currentFoodCellY) = AssignRandomPosition();
            }
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSkyBlue);
        _spriteBatch.Begin();

        var foodRect = new Rectangle(currentFoodCellX*cellSize, currentFoodCellY*cellSize, cellSize, cellSize);
        _spriteBatch.Draw(_food, foodRect, Color.Firebrick); // draw only decides where and how big to put that image on screen, Draw cannot make pixels itself, it always copied them from a texture you give it.

        var snakeRect = new Rectangle(currentSnakeCellX*cellSize, currentSnakeCellY*cellSize, cellSize, cellSize);
        _spriteBatch.Draw(_snake, snakeRect, Color.Aquamarine);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    
    #region game logic

    private (int, int) AssignRandomPosition()
    {
        var rand = new Random();
        var x = rand.Next(0, Columns);
        var y = rand.Next(0, Rows);
        return (x, y);
    }
    
    // now i would like to make
    
    #endregion
}