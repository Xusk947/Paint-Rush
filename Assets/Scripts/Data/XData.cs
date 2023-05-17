using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaintRush.Data
{
    [Serializable]
    public class XData
    {
        public static XData Instance;

        public readonly string Version;

        public Dictionary<string, TextureData> Textures;
        public TextureData Current;
        public XData()
        {
            Version = Application.version;
            Textures = new Dictionary<string, TextureData>();
            Instance = this;
        }
    }
}