using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using Pieces;

namespace Managers;

class PlayerManager
{
  public static PlayerManager Instance;

  private int points;

  public int Points
  {
    get { return points; }
    private set
    {
      if (value >= 999999)
      {
        points = 999999;
      }
      else
      {
        points = value;
      }
    }
  }

  public int TotalPoints { get; private set; }

  private int level;

  public int Level
  {
    get { return level; }
    private set
    {
      if (value >= 99)
      {
        level = 99;
      }
      else
      {
        level = value;
      }
    }
  }

  public int TotalLevel { get; private set; }

  public int LinesCleared { get; private set; }

  public bool Playing { get; private set; }

  public Tetromino CurrentTetrimino { get; private set; }

  public MusicManager MusicManager { get; private set; }

  const int lineClearPoints = 150;

  public PlayerManager(MusicManager musicManager)
  {
    if (Instance is null)
    {
      Instance = this;
    }

    MusicManager = musicManager;

    Points = 0;

    Level = 1;

    LineClearManager.Instance.OnLineCleared += AddPointsLineClear;
  }

  public void AddPointsLineClear()
  {
    LinesCleared++;

    int amount = lineClearPoints * Level;

    Points += amount;

    TotalPoints += amount;

    level =  (Points / 1500) + 1;

    Level = level;

    TotalLevel = level;
  }

  public void StartGame()
  {
    Playing = true;
  }

  public void EndGame()
  {
    Playing = false;

    MusicManager.StopMusic();

    SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectType.EndGame);
  }

  public void CheckForInput()
  {
    ConsoleKeyInfo? keyInfo = null;

    // Drain the buffer and keep only the last key pressed
    while (Console.KeyAvailable)
    {
        keyInfo = Console.ReadKey(true);
    }

    // Check Move Input

    if (keyInfo is not null && CurrentTetrimino is not null)
    {
      switch (keyInfo.Value.Key)
      {
        case ConsoleKey.LeftArrow:
          CurrentTetrimino.MoveLeft();
          break;
        case ConsoleKey.RightArrow:
          CurrentTetrimino.MoveRight();
          break;
        case ConsoleKey.UpArrow:
          CurrentTetrimino.RotateClockwise();
          break;
        case ConsoleKey.DownArrow:
          CurrentTetrimino.MoveDown();
          break;
      }
    }

    // Check Other Input

    if (keyInfo is not null)
    {
      switch (keyInfo.Value.Key)
      {
        case ConsoleKey.Spacebar:
          MusicManager.NextMusic();
          break;
        case ConsoleKey.Escape:
          EndGame();
          break;
      }
    }
  }

  public void Tick()
  {
    if (CurrentTetrimino is not null)
    {
      CurrentTetrimino.MoveDown();
    }
  }

  public void SetCurrentTetromino(Tetromino tetromino)
  {
    CurrentTetrimino = tetromino;
  }
}