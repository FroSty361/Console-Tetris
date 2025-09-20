using System;
using Core;
using Managers;

namespace Pieces;

class Pixel
{
  private char _value;

  public char Value
  {
    get { return _value; }
    private set
    {
      _value = value;

      if (value == ' ')
      {
        PixelTaken = false;
      }
      else if (value == '#')
      {
        PixelTaken = true;
      }
    }
  }

  public bool PixelTaken { get; private set; }

  public Coordinates Coordinates { get; private set; }

  public Pixel(Coordinates coordinates)
  {
    Coordinates = coordinates;

    Value = ' ';

    LineClearManager.Instance.OnLineCleared += OnLineCleared;
  }

  public void ChangeValue(char _value_)
  {
    Value = _value_;
  }

  private void OnLineCleared()
  {
    int x = Coordinates.XCoord;

    int y = Coordinates.YCoord + 1; // Down, Y Goes Up

    Coordinates coordinates = new Coordinates(x, y);

    if (Grid.Instance.TryGetPixelByCoordinates(coordinates, out Pixel pixel))
    {
      if (pixel.PixelTaken is false && PixelTaken is true)
      {
        ChangeValue(' ');

        pixel.ChangeValue('#');
      }
    }
  }
}