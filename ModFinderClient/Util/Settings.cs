﻿namespace ModFinder.Util
{
  public class Settings
  {
    public string AutoWrathPath { get; set; }
    public string WrathPath { get; set; }

    private static Settings _Instance;
    public static Settings Load()
    {
      if (_Instance == null)
      {
        if (Main.TryReadFile("Settings.json", out var settingsRaw))
          _Instance = IOTool.FromString<Settings>(settingsRaw);
        else
          _Instance = new();
      }

      return _Instance;
    }

    public void Save()
    {
      IOTool.Safe(() =>
      {
        IOTool.Write(this, Main.AppPath("Settings.json"));
      });
    }
  }
}
