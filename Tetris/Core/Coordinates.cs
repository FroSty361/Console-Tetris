using System;

namespace Core;

struct Coordinates
{
  public int XCoord { get; private set; }

  public int YCoord { get; private set; }

  public Coordinates(int xCoord, int yCoord)
  {
    XCoord = xCoord;

    YCoord = yCoord;
  }

  public override string ToString()
  {
    return $"{XCoord}, {YCoord}";
  }
}