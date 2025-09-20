using System.Collections.Concurrent;
using System.Threading;

namespace Managers;

class InputManager
{
  public static readonly ConcurrentQueue<ConsoleKey> Inputs = new();

  public static void Start()
  {
    Thread inputThread = new Thread(() =>
    {
      while (true)
      {
        var key = Console.ReadKey(true).Key;
        Inputs.Enqueue(key);
      }
    });

    inputThread.IsBackground = true;
    
    inputThread.Start();
  }

  public static void ProcessInput()
  {
    while (Inputs.TryDequeue(out var key))
    {
      switch (key)
      {
        case ConsoleKey.LeftArrow:
          PlayerManager.Instance.CurrentTetrimino?.MoveLeft();
          break;
        case ConsoleKey.RightArrow:
          PlayerManager.Instance.CurrentTetrimino?.MoveRight();
          break;
        case ConsoleKey.UpArrow:
          PlayerManager.Instance.CurrentTetrimino?.RotateClockwise();
          break;
        case ConsoleKey.DownArrow:
          PlayerManager.Instance.CurrentTetrimino?.MoveDown();
          break;
        case ConsoleKey.Spacebar:
          PlayerManager.Instance.MusicManager.NextMusic();
          break;
        case ConsoleKey.M:
          PlayerManager.Instance.MusicManager.MuteOrUnmuteMusic();
          break;
        case ConsoleKey.Escape:
          PlayerManager.Instance.EndGame();
          break;
      }
    }
  }
}
