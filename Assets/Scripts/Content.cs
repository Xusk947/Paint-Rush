using PaintRush.Data;
using PaintRush.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace PaintRush
{
    public class Content : MonoBehaviour
    {
        /// --- BLOCK SECTION ---
        public static Block EmptyBlock, FinishBlock;
        public static List<Block> PaintBlocks, DangerBlocks;

        public static GameObject PlayerPaintBall;
        public static Dictionary<string, List<Texture2D>> Textures;

        private void Awake()
        {
            XData.Instance = DataManager.LoadGame();

            EmptyBlock = Resources.Load<Block>("Blocks/EmptyBlock");
            FinishBlock = Resources.Load<Block>("Blocks/FinishBlock");

            PaintBlocks = new List<Block>();
            PaintBlocks.AddRange(Resources.LoadAll<Block>("Blocks/PaintBlock"));

            DangerBlocks = new List<Block>();
            DangerBlocks.AddRange(Resources.LoadAll<Block>("Blocks/DangerBlock"));

            PlayerPaintBall = Resources.Load<GameObject>("Collectable/PlayerPaintBall");
            LoadTextures();
        }

        private void LoadTextures()
        {
            Textures = new Dictionary<string, List<Texture2D>>();
            List<Texture2D> textures = new List<Texture2D>(Resources.LoadAll<Texture2D>("Images"));
            foreach (Texture2D texture in textures)
            {
                string textureName = texture.name;
                // Check if the texture has a scale suffix
                if (textureName.Contains("_scaled"))
                {
                    string baseName = textureName[..textureName.LastIndexOf("_")]; // Get the base name without the scale suffix
                    // Add the texture to the dictionary under the base name
                    if (Textures.ContainsKey(baseName))
                    {
                        Textures[baseName].Add(texture);
                    }
                    else
                    {
                        Textures.Add(baseName, new List<Texture2D> { texture });
                    }
                }
                else
                {
                    // Add the original texture to the dictionary
                    if (!Textures.ContainsKey(textureName))
                    {
                        Textures.Add(textureName, new List<Texture2D> { texture });
                    }

                }
            }
            RemoveFinishedTextures();
            SortTextures();
        }

        private void SortTextures()
        {
            foreach (KeyValuePair<string, List<Texture2D>> kvp in Textures)
            {
                List<Texture2D> textures = kvp.Value;

                // Sort the textures based on their names
                textures.Sort((t1, t2) => t1.name.CompareTo(t2.name));
            }
        }
        
        private void RemoveFinishedTextures()
        {
            var keys = Textures.Keys.ToList();
            var values = Textures.Values.ToList();
            for (int i = 0; i < Textures.Count; i++)
            {
                var key = keys[i];
                var value = values[i];

                if (XData.Instance.Textures.ContainsKey(key))
                {
                    TextureData texture = XData.Instance.Textures[key];
                    if (texture.Finished)
                    {
                        print(texture + " : was finished and removed from list");
                        Textures.Remove(texture.Name);
                    }
                }
            }
        }
    }
}
