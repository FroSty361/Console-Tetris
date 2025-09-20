using System;
using Core;
using Managers;

namespace Pieces;

class Tetromino
{
    public char Name { get; private set; }

    public List<Coordinates> Pixels { get; private set; } = new List<Coordinates>();

    public Tetromino(char name)
    {
        Name = name;

        Pixels = new List<Coordinates>();
    }

    public void GetPixelCoordinates(Coordinates[] coordinates)
    {
        Pixels = coordinates.ToList();
    }

    public void MoveDown()
    {
        if (CanMoveDown() is false)
        {
            SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectType.Place);

            LineClearManager.Instance.CheckForLines();

            TetriminosManager.Instance.SpawnPiece();

            return;
        }

        List<Coordinates> oldPixels = [.. Pixels];

        Pixels.Clear();

        List<Coordinates> newCoordinates = new List<Coordinates>();

        foreach (Coordinates coordinates in oldPixels)
        {
            int x = coordinates.XCoord;

            int y = coordinates.YCoord + 1; // Down, Y Goes Up

            Coordinates coords = new Coordinates(x, y);

            newCoordinates.Add(coords);
        }

        foreach (Coordinates coordinates in oldPixels)
        {
            Pixel pixel = Grid.Instance.GetPixelByCoordinates(coordinates);

            if (newCoordinates.Contains(pixel.Coordinates) == false)
            {
                pixel.ChangeValue(' ');
            }

            int x = coordinates.XCoord;

            int y = coordinates.YCoord + 1; // Down, Y Goes Up

            Coordinates nextCoordinates = new Coordinates(x, y);

            Pixels.Add(nextCoordinates);

            Pixel newPixel = Grid.Instance.GetPixelByCoordinates(nextCoordinates);

            newPixel.ChangeValue('#');
        }
    }

    private bool CanMoveDown()
    {
        foreach (Coordinates coordinates in Pixels)
        {
            int x = coordinates.XCoord;

            int y = coordinates.YCoord + 1; // Down, Y Goes Up

            Coordinates testCoordinates = new Coordinates(x, y);

            if (Grid.Instance.TryGetPixelByCoordinates(testCoordinates, out Pixel pixel) == true)
            {
                if (pixel.PixelTaken && Pixels.Contains(pixel.Coordinates) == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void MoveRight()
    {
        int offset = 1;

        if (CanMoveToSide(offset) is false)
        {
            return;
        }

        MoveToSide(offset);

        SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectType.MoveTetromino);
    }

    public void MoveLeft()
    {
        int offset = -1;

        if (CanMoveToSide(offset) is false)
        {
            return;
        }

        MoveToSide(offset);

        SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectType.MoveTetromino);
    }

    private void MoveToSide(int offset)
    {
        List<Coordinates> oldPixels = [.. Pixels];

        Pixels.Clear();

        List<Coordinates> newCoordinates = new List<Coordinates>();

        foreach (Coordinates coordinates in oldPixels)
        {
            int x = coordinates.XCoord + offset;

            int y = coordinates.YCoord; // Down, Y Goes Up

            Coordinates coords = new Coordinates(x, y);

            newCoordinates.Add(coords);
        }

        foreach (Coordinates coordinates in oldPixels)
        {
            Pixel pixel = Grid.Instance.GetPixelByCoordinates(coordinates);

            if (newCoordinates.Contains(pixel.Coordinates) == false)
            {
                pixel.ChangeValue(' ');
            }

            int x = coordinates.XCoord + offset;

            int y = coordinates.YCoord; // Down, Y Goes Up

            Coordinates nextCoordinates = new Coordinates(x, y);

            Pixels.Add(nextCoordinates);

            Pixel newPixel = Grid.Instance.GetPixelByCoordinates(nextCoordinates);

            newPixel.ChangeValue('#');
        }
    }

    private bool CanMoveToSide(int offset)
    {
        foreach (Coordinates coordinates in Pixels)
        {
            int x = coordinates.XCoord + offset;

            int y = coordinates.YCoord;

            Coordinates testCoordinates = new Coordinates(x, y);

            if (Grid.Instance.TryGetPixelByCoordinates(testCoordinates, out Pixel pixel) == true)
            {
                if (pixel.PixelTaken && Pixels.Contains(pixel.Coordinates) == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void RotateClockwise()
    {
        if (Pixels.Count == 0)
        {
            return;
        }

        Coordinates pivot = Pixels.First();

        List<Coordinates> newCoordinates = new List<Coordinates>();

        foreach (Coordinates coordinates in Pixels)
        {
            int relativeX = coordinates.XCoord - pivot.XCoord;
            int relativeY = coordinates.YCoord - pivot.YCoord;

            int rotatedX = pivot.XCoord + relativeY;
            int rotatedY = pivot.YCoord - relativeX;

            Coordinates newCoord = new Coordinates(rotatedX, rotatedY);

            if (Grid.Instance.TryGetPixelByCoordinates(newCoord, out Pixel pixel) == false || (pixel.PixelTaken && !Pixels.Contains(pixel.Coordinates)))
            {
                return;
            }

            newCoordinates.Add(newCoord);
        }

        foreach (Coordinates coordinates in Pixels)
        {
            Pixel oldPixel = Grid.Instance.GetPixelByCoordinates(coordinates);

            oldPixel.ChangeValue(' ');
        }

        Pixels.Clear();

        Pixels.AddRange(newCoordinates);

        foreach (Coordinates coordinates in Pixels)
        {
            Pixel newPixel = Grid.Instance.GetPixelByCoordinates(coordinates);
            newPixel.ChangeValue('#');
        }

        SoundEffectManager.Instance.PlaySoundEffect(SoundEffectManager.SoundEffectType.Rotate);
    }
}