using System;
using NAudio.Wave;
using Pieces;

namespace Managers;

class MusicManager
{
  int amountOfMusic = 8;

  string tetrisTheme = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\Original Tetris theme (Tetris Soundtrack).mp3";

  string waltzNoFour = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\Dmitri Shostakovich - Waltz No. 2.mp3";

  string katyushaEightBit = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\Katyusha (8-bit).mp3";

  string kalinka = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\Russian Folk Song - Kalinka (Калинка).mp3";

  string sovietAnthem = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\[SS] USSR Anthem Chiptune Cover.mp3";

  string katyusha = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\Girls und Panzer - Katyusha.mp3";

  string nutcracker = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\1337722_8-Bit-Remix-Dance-of-the-S.mp3";

  string marchOfTheReadCavalry = @"D:\VisualStudioCodeProjects\Projects\Tetris\Music\Марш Буденного! March of the Red Cavalry! (English Lyrics).mp3";

  private IWavePlayer? outputDevice;

  private AudioFileReader? audioFile;

  private bool musicMuted = false;

  public void NextMusic()
  {
    bool stoppedMusic = StopMusic();

    if (stoppedMusic is true)
    {
      PlayMusic();
    }
  }

  public void MuteOrUnmuteMusic()
  {
    if (musicMuted is true)
    {
      UnmuteMusic();
    }
    else
    {
      MuteMusic();
    }
  }

  private void UnmuteMusic()
  {
    musicMuted = false;

    PlayMusic();
  }

  private void MuteMusic()
  {
    musicMuted = true;

    StopMusic();
  }

  public void PlayMusic()
  {
    if (musicMuted is true)
    {
      return;
    }

    string music = ChooseMusic();

    try
    {
      outputDevice = new WaveOutEvent();

      audioFile = new AudioFileReader(music);

      outputDevice.Init(audioFile);

      outputDevice.Play();
    }
    catch (Exception)
    {

    }
  }

  public bool StopMusic()
  {
    try
    {
      outputDevice.Stop();

      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

  private string ChooseMusic()
  {
    Random random = new Random();

    int number = random.Next(1, amountOfMusic + 1);

    if (number is 1)
    {
      return tetrisTheme;
    }
    else if (number is 2)
    {
      return waltzNoFour;
    }
    else if (number is 3)
    {
      return katyushaEightBit;
    }
    else if (number is 4)
    {
      return kalinka;
    }
    else if (number is 5)
    {
      return sovietAnthem;
    }
    else if (number is 6)
    {
      return katyusha;
    }
    else if (number is 7)
    {
      return nutcracker;
    }
    else if (number is 8)
    {
      return marchOfTheReadCavalry;
    }

    return null;
  }
}