﻿using ModFinder.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ModFinder.Mod
{
  public class SettingsData
  {
    [JsonProperty]
    public List<string> EnabledModifications { get; set; } = new();
  }

  public class OwlcatModificationSettingsManager
  {
    public void Remove(string uniqueid)
    {
      IOTool.Safe(() =>
      {
        if (OwlcatSettings.EnabledModifications.Remove(uniqueid))
          Save();
      });
    }
    public void Add(string uniqueid)
    {
      IOTool.Safe(() =>
      {
        if (!OwlcatSettings.EnabledModifications.Contains(uniqueid))
        {
          OwlcatSettings.EnabledModifications.Add(uniqueid);
          Save();
        }
      });
    }

    private void Save()
    {
      IOTool.Write(OwlcatSettings, SettingsPath);
    }

    private static SettingsData Load()
    {
      if (File.Exists(SettingsPath))
        return IOTool.Read<SettingsData>(SettingsPath);
      else
        return new();
    }

    private static string SettingsPath => Path.Combine(Main.WrathDataDir, "OwlcatModificationManangerSettings.json");

    private SettingsData _Data;
    private SettingsData OwlcatSettings => _Data ??= Load();
  }
}