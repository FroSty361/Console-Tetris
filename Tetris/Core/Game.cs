using System;
using System.Threading;
using static System.Console;
using Managers;
using Pieces;

namespace Core
{
    public class Game
    {
        private readonly MusicManager musicManager;

        private readonly PlayerManager playerManager;

        private readonly int startingFrameTime = 300;

        private readonly int minimumFrameTime = 50;

        public int FrameTime { get; private set; }

        public Game()
        {
            CursorVisible = false;

            musicManager = new MusicManager();

            Initialize();

            playerManager = PlayerManager.Instance;
        }

        public void Run()
        {
            ShowMenu();

            musicManager.PlayMusic();

            TetriminosManager.Instance.SpawnPiece();

            InputManager.Start();
            
            SetCursorPosition(0, 0);

            while (playerManager.Playing)
            {
                UI.Instance.UpdateUI();

                SetFrameTime();

                Thread.Sleep(FrameTime);

                SetCursorPosition(0, 0);

                InputManager.ProcessInput();

                playerManager.Tick();
            }

            EndGame();
        }

        private void Initialize()
        {
            _ = new LineClearManager();
            _ = new Grid();
            _ = new TetriminosManager();
            _ = new UI();
            _ = new PlayerManager(musicManager);
            _ = new SoundEffectManager();
        }

        private void SetFrameTime()
        {
            int level = playerManager.Level;

            FrameTime = (int)(minimumFrameTime + (startingFrameTime - minimumFrameTime) * Math.Pow(0.9, level - 1));
        }

        private void ShowMenu()
        {
            WriteLine("Tetris");
            WriteLine();

            WriteLine("Move Input,");
            WriteLine("LeftArrow = Move Left");
            WriteLine("RightArrow = Move Right");
            WriteLine("UpArrow = Rotate (Clockwise)");
            WriteLine("DownArrow = Move Down");
            WriteLine();

            WriteLine("Input,");
            WriteLine("Space = Change Music");
            WriteLine("M = Mue Or Unmute Music");
            WriteLine("Escape = End Game");
            WriteLine();

            WriteLine("Press Enter To Start Tetris");
            ReadLine();

            PlayerManager.Instance.StartGame();

            Clear();
            SetCursorPosition(0, 0);
        }

        private void EndGame()
        {
            Clear();

            SetCursorPosition(0, 0);

            UI.Instance.EndGameUI();

            // ReadKey();
        }
    }
}
