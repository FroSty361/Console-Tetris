using System;
using Core;
using Pieces;

namespace Managers;

class TetriminosManager
{
  public static TetriminosManager Instance;

  public List<char> UpcomingPieces { get; private set; } = new List<char>();

  private char[] piecesNames;

  public TetriminosManager()
  {
    if (Instance is null)
    {
      Instance = this;
    }

    piecesNames = ['I', 'O', 'T', 'S', 'Z', 'J', 'L'];

    UpcomingPieces = [GetRandomPieceName(), GetRandomPieceName(), GetRandomPieceName()];
  }

  public void SpawnPiece()
  {
    char pieceName = UpcomingPieces.First();

    UpcomingPieces.Remove(pieceName);

    Tetromino tetromino = new Tetromino(pieceName);

    UpcomingPieces.Add(GetRandomPieceName());

    PlayerManager.Instance.SetCurrentTetromino(tetromino);

    Coordinates[] spawnPixelsCoordinates = GetSpawnArea(tetromino.Name);

    tetromino.GetPixelCoordinates(spawnPixelsCoordinates);

    foreach (Coordinates coordinates in spawnPixelsCoordinates)
    {
      if (Grid.Instance.TryGetPixelByCoordinates(coordinates, out Pixel pixel) == true)
      {
        if (pixel.PixelTaken is true)
        {
          PlayerManager.Instance.EndGame();

          break;
        }
        else
        {
          pixel.ChangeValue('#');
        }
      }
      else
      {
        PlayerManager.Instance.EndGame();

        break;
      }
    }
  }

  private Coordinates[] GetSpawnArea(char tetriminoType)
  {
    List<Coordinates> coordinates = new List<Coordinates>();

    int center = Grid.Instance.Width / 2;

    center = center - 1;

    switch (tetriminoType)
      {
        case 'I':
          coordinates.Add(new Coordinates(center - 2, 0));
          coordinates.Add(new Coordinates(center - 1, 0));
          coordinates.Add(new Coordinates(center, 0));
          coordinates.Add(new Coordinates(center + 1, 0));
          break;
        case 'O':
          coordinates.Add(new Coordinates(center - 1, 0));
          coordinates.Add(new Coordinates(center, 0));
          coordinates.Add(new Coordinates(center - 1, 1));
          coordinates.Add(new Coordinates(center, 1));
          break;
        case 'T':
          coordinates.Add(new Coordinates(center - 1, 0));
          coordinates.Add(new Coordinates(center - 2, 1));
          coordinates.Add(new Coordinates(center - 1, 1));
          coordinates.Add(new Coordinates(center, 1));
          break;
        case 'S':
          coordinates.Add(new Coordinates(center - 1, 0));
          coordinates.Add(new Coordinates(center, 0));
          coordinates.Add(new Coordinates(center - 2, 1));
          coordinates.Add(new Coordinates(center - 1, 1));
          break;
        case 'Z':
          coordinates.Add(new Coordinates(center - 2, 0));
          coordinates.Add(new Coordinates(center - 1, 0));
          coordinates.Add(new Coordinates(center - 1, 1));
          coordinates.Add(new Coordinates(center, 1));
          break;
        case 'J':
          coordinates.Add(new Coordinates(center - 2, 0));
          coordinates.Add(new Coordinates(center - 2, 1));
          coordinates.Add(new Coordinates(center - 1, 1));
          coordinates.Add(new Coordinates(center, 1));
          break;
        case 'L':
          coordinates.Add(new Coordinates(center, 0));
          coordinates.Add(new Coordinates(center - 2, 1));
          coordinates.Add(new Coordinates(center - 1, 1));
          coordinates.Add(new Coordinates(center, 1));
          break;
      }

    return coordinates.ToArray();
  }

  private char GetRandomPieceName()
  {
    Random random = new Random();

    int number = random.Next(0, piecesNames.Length);

    return piecesNames[number];
  }
}