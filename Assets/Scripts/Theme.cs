using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class Theme
    {
        [Header ("Level Colors-------")]
        [Header ("Ground")]
        public Color groundColor;
        public Color bordersColor;
        public Color sideColor;

        [Header ("Objects & Obstacles")]
        public Color objectColor;
        public Color obstacleColor;

        [Header ("UI (progress)")]
        public Color progressFillColor;

        [Header ("Background")]
        public Color cameraColor;
        public Color fadeColor;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "ThemesControl", menuName = "ThemesControl")]
    public class ThemesControl : ScriptableObject
    {
        public Theme[] themes;
    }
}