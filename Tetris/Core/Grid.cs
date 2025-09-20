using System;
using static System.Console;
using System.Linq;
using Pieces;

namespace Core;

class Grid
{
  public static Grid Instance;

  public List<Pixel> Pixels { get; private set; } = new List<Pixel>();

  public int Height { get; private set; } = 20;

  public int Width { get; private set; } = 10;

  public Grid()
  {
    if (Instance is null)
    {
      Instance = this;
    }

    Pixels = new List<Pixel>();

    CreateGrid();
  }

  public bool TryGetPixelByCoordinates(Coordinates coordinates, out Pixel pixel)
  {
    int x = coordinates.XCoord;

    int y = coordinates.YCoord;

    // WriteLine(Pixels.Count);

    pixel = Pixels.FirstOrDefault(p => p.Coordinates.XCoord == x && p.Coordinates.YCoord == y);

    return pixel is not null;
  }

  public Pixel GetPixelByCoordinates(Coordinates coordinates)
  {
    int x = coordinates.XCoord;

    int y = coordinates.YCoord;

    return Pixels.FirstOrDefault(p => p.Coordinates.XCoord == x && p.Coordinates.YCoord == y);
  }

  private void CreateGrid()
  {
    for (int y = 0; y < Height; y++)
    {
      for (int x = 0; x < Width; x++)
      {
        Coordinates coordinates = new Coordinates(x, y);

        Pixels.Add(new Pixel(coordinates));
      }
    }
  }
}