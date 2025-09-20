using System;
using Core;
using Pieces;

namespace Managers;

class LineClearManager
{
  public static LineClearManager Instance;

  public Action OnLineCleared { get; set; }

  public LineClearManager()
  {
    if (Instance is null)
    {
      Instance = this;
    }
  }

  public void CheckForLines()
  {
    int height = Grid.Instance.Height;

    int width = Grid.Instance.Width;

    for (int y = height - 1; y >= 0; y--)
    {
      int filled = 0;

      for (int x = 0; x < width; x++)
      {
        Coordinates coords = new Coordinates(x, y);
            
        if (Grid.Instance.GetPixelByCoordinates(coords).PixelTaken)
        {
          filled++;
        }
      }

      if (filled == width)
      {
        LineCleared(y, width);

        y++;
      }
    }
  }

  private void LineCleared(int y, int width)
  {
    SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectType.ClearLine);

    for (int x = 0; x < width; x++)
    {
      Coordinates coords = new Coordinates(x, y);

      Grid.Instance.GetPixelByCoordinates(coords).ChangeValue(' ');
    }

    for (int row = y - 1; row >= 0; row--)
    {
      for (int x = 0; x < width; x++)
      {
        Coordinates fromCoords = new Coordinates(x, row);

        Pixel fromPixel = Grid.Instance.GetPixelByCoordinates(fromCoords);

        Coordinates toCoords = new Coordinates(x, row + 1);

        Pixel toPixel = Grid.Instance.GetPixelByCoordinates(toCoords);

        if (fromPixel.PixelTaken)
        {
          toPixel.ChangeValue('#');

          fromPixel.ChangeValue(' ');
        }
      }
    }

    OnLineCleared?.Invoke();
  }
}