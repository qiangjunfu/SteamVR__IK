using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SettingData
{
    [SerializeField, ReadOnly] public int id;
    [SerializeField, ReadOnly] public bool isShowLog;

    // Graphics Settings
    [SerializeField, ReadOnly] public int resolutionWidth;
    [SerializeField, ReadOnly] public int resolutionHeight;
    [SerializeField, ReadOnly] public bool isFullScreen;
    [SerializeField, ReadOnly] public bool vSync;
    [SerializeField, ReadOnly] public int antiAliasing;
    [SerializeField, ReadOnly] public int shadowQuality;
    [SerializeField, ReadOnly] public int textureQuality;
    [SerializeField, ReadOnly] public int effectsQuality;

    // Audio Settings
    [SerializeField, ReadOnly] public float masterVolume;
    [SerializeField, ReadOnly] public float musicVolume;
    [SerializeField, ReadOnly] public float sfxVolume;
    [SerializeField, ReadOnly] public float voiceVolume;

    // Control Settings
    [SerializeField, ReadOnly] public float mouseSensitivity;
    [SerializeField, ReadOnly] public bool invertYAxis;
    [SerializeField, ReadOnly] public Dictionary<string, KeyCode> keyBindings;

    // Game Settings
    [SerializeField, ReadOnly] public string difficultyLevel;
    [SerializeField, ReadOnly] public string crosshairStyle;
    [SerializeField, ReadOnly] public bool showHUD;

    // Network Settings
    [SerializeField, ReadOnly] public string serverRegion;
    [SerializeField, ReadOnly] public int maxPlayers;
    [SerializeField, ReadOnly] public int networkLatency;

    // Advanced Settings
    [SerializeField, ReadOnly] public float fov;
    [SerializeField, ReadOnly] public int fpsLimit;
    [SerializeField, ReadOnly] public float renderDistance;


    public SettingData()
    {
        id = 1;
        isShowLog = false;

        // Default graphics settings
        resolutionWidth = 1920;
        resolutionHeight = 1080;
        isFullScreen = true;
        vSync = true;
        antiAliasing = 2;
        shadowQuality = 2;
        textureQuality = 2;
        effectsQuality = 2;

        // Default audio settings
        masterVolume = 1.0f;
        musicVolume = 0.5f;
        sfxVolume = 0.5f;
        voiceVolume = 0.5f;

        // Default control settings
        mouseSensitivity = 1.0f;
        invertYAxis = false;
        keyBindings = new Dictionary<string, KeyCode>
        {
            {"Forward", KeyCode.W},
            {"Backward", KeyCode.S},
            {"Left", KeyCode.A},
            {"Right", KeyCode.D},
            {"Jump", KeyCode.Space},
            {"Fire", KeyCode.Mouse0},
            {"Aim", KeyCode.Mouse1},
            {"Reload", KeyCode.R}
        };

        // Default game settings
        difficultyLevel = "Normal";
        crosshairStyle = "Default";
        showHUD = true;

        // Default network settings
        serverRegion = "US";
        maxPlayers = 16;
        networkLatency = 100;

        // Default advanced settings
        fov = 90.0f;
        fpsLimit = 60;
        renderDistance = 1000.0f;
    }
}

