using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateLibrary
{
    public class GameStateManager
    {
        // Instance of the game state manager     
        private static GameStateManager _instance;
        
        // GameStateManager keeps the ContentManager
        private ContentManager _content;

        // Method set the content manager
        public void SetContent(ContentManager content)
        {
            _content = content;
        }

        // Stack for the screens     
        private Stack<GameState> _screens = new Stack<GameState>(); 

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        // Below is where we actually do the management of the screens

        // Adds a new screen to the stack 
        public void AddScreen(GameState screen)
        {
            try
            {
            // Add the screen to the stack
            _screens.Push(screen);
            // Initialize the screen
            _screens.Peek().Initialize();
            // Call the LoadContent on the screen
                if (_content != null)
                {
                    _screens.Peek().LoadContent(_content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Removes the top screen from the stack
        public void RemoveScreen()
        {
            if (_screens.Count > 0)
            {
                try
                {
                    var screen = _screens.Peek();
                    _screens.Pop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        // Clears all the screen from the list
        public void ClearScreens()
        {
            while (_screens.Count > 0)
            {     
                _screens.Pop();
            }
        }

        // Removes all screens from the stack and adds a new one 
        public void ChangeScreen(GameState screen)
        {
            try
            {
                ClearScreens();
                AddScreen(screen);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Updates the top screen. 
        public void Update(GameTime gameTime)
        {
            try
            {
                if (_screens.Count > 0)
                {
                    _screens.Peek().Update(gameTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Renders the top screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (_screens.Count > 0)
                {
                    _screens.Peek().Draw(spriteBatch);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Unloads the content from the screen
        public void UnloadContent()
        {
            foreach (GameState state in _screens)
            {
                state.UnloadContent();
            }   
        }
    }
}