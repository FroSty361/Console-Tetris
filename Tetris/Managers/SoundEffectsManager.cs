using System;
using NAudio.Wave;
using Pieces;

namespace Managers;

class SoundEffectManager
{
  public static SoundEffectManager Instance;

  public enum SoundEffectType
  {
    Place,
    EndGame,
    ClearLine,
    Rotate,
    MoveTetromino
  }

  string path = @"D:\VisualStudioCodeProjects\Projects\Tetris\SoundEffects\";

  private IWavePlayer? outputDevice;

  private AudioFileReader? audioFile;

  public SoundEffectManager()
  {
    if (Instance is null)
    {
      Instance = this;
    }
  }

  public void PlaySoundEffect(SoundEffectType soundEffectType)
  {
    string soundEffect = GetSoundEffect(soundEffectType);

    if (string.IsNullOrEmpty(soundEffect))
    {
      return;
    }

    string soundEffectFile = path + soundEffect;

    try
    {
      outputDevice = new WaveOutEvent();

      audioFile = new AudioFileReader(soundEffectFile);

      outputDevice.Init(audioFile);

      outputDevice.Play();
    }
    catch (Exception)
    {

    }
  }

  private string GetSoundEffect(SoundEffectType soundEffectType)
  {
    switch (soundEffectType)
    {
      case SoundEffectType.Place:
        return "piece_placed.mp3";
      case SoundEffectType.EndGame:
        return "game_over.mp3";
      case SoundEffectType.ClearLine:
        return "line_clear.mp3";
      case SoundEffectType.Rotate:
        return "rotate.mp3";
      case SoundEffectType.MoveTetromino:
        return "move_tetromino.mp3";
    }

    return "";
  }
}