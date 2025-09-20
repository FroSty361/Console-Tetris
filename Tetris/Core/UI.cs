using System;
using static System.Console;
using Pieces;
using Managers;

namespace Core;

class UI
{
  public static UI Instance;

  public UI()
  {
    if (Instance is null)
    {
      Instance = this;
    }
  }

  public void UpdateUI()
  {
    UpdateStatsUI();

    WriteLine();

    int height = Grid.Instance.Height;

    int width = Grid.Instance.Width;

    SetCursorPosition(0, 6);

    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        Coordinates coordinates = new Coordinates(x, y);

        if (Grid.Instance.TryGetPixelByCoordinates(coordinates, out Pixel pixel) == true)
        {
          if (pixel.PixelTaken is false)
          {
            Write($". ");
          }
          else
          {
            Write($"{pixel.Value} ");
          }
        }
      }

      WriteLine();
    }
  }

  public void EndGameUI()
  {
    CursorVisible = false;

    WriteLine("Game Over!");

    WriteLine();

    WriteLine("Press Enter To See How You Did");

    ReadLine();

    ShowTetrisStats();
  }

  private void ShowTetrisStats()
  {
    int Points = PlayerManager.Instance.TotalPoints;

    int Level = PlayerManager.Instance.TotalLevel;

    int LinesCleared = PlayerManager.Instance.LinesCleared;

    WriteLine();

    WriteLine("Stats,");

    WriteLine();

    WriteLine($"1. Points = {Points}");

    WriteLine($"2. Level = {Level}");

    WriteLine($"3. Lines Cleared = {LinesCleared}");

    ReadKey();
  }

  private void UpdateStatsUI()
  {
    int Points = PlayerManager.Instance.Points;

    int Level = PlayerManager.Instance.Level;

    char nextTetrominoOne = TetriminosManager.Instance.UpcomingPieces.First();

    char nextTetrominoTwo = TetriminosManager.Instance.UpcomingPieces[1];

    char nextTetrominoThree = TetriminosManager.Instance.UpcomingPieces[2];

    string stats = $"* {Points}, {Level} *";

    string statsSpacing = GetTextSpacing(stats.Length);

    string nextTetrominos = $"* Next, {nextTetrominoOne}, {nextTetrominoTwo}, {nextTetrominoThree} *";

    string nextTetrominosSpacing = string.Empty;

    if (nextTetrominos.Length % 2 == 0)
    {
      int length = nextTetrominos.Length;

      nextTetrominosSpacing = GetTextSpacing(length);
    }
    else
    {
      int length = (nextTetrominos.Length + 1);

      nextTetrominosSpacing = GetTextSpacing(length);
    }

    WriteLine("* * * * * * * * * *");

    WriteLine($"* Points,    Lvl *");

    WriteLine($"* {Points},{statsSpacing}{Level} *");

    WriteLine($"*{nextTetrominosSpacing} Next, {nextTetrominoOne}, {nextTetrominoTwo}, {nextTetrominoThree} {nextTetrominosSpacing}*");

    WriteLine("* * * * * * * * * *");
  }

  private string GetTextSpacing(int stringLength)
  {
    int totalLength = 20;

    int neededLength = totalLength - stringLength;

    string spacing = string.Empty;

    for (int i = 0; i < neededLength; i++)
    {
      spacing += " ";
    }

    return spacing;
  }
}