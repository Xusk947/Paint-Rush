using PaintRush.Data;
using PaintRush.Utils;
using PaintRush.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using World;

namespace PaintRush
{
    public class Content : MonoBehaviour
    {
        /// --- BLOCK SECTION ---
        public static Block EmptyBlock;
        public static PaintBlock FinishBlock;
        public static List<Block> PaintBlocks, DangerBlocks;

        public static GameObject PlayerPaintBall;
        public static Dictionary<string, List<Texture2D>> Textures;
        public static Bullet Bullet;

        public static Material FillerMaterial;

        public static void Init()
        {
            EmptyBlock = Resources.Load<Block>("Blocks/EmptyBlock");
            FinishBlock = Resources.Load<PaintBlock>("Blocks/FinishBlock");

            PaintBlocks = new List<Block>();
            PaintBlocks.AddRange(Resources.LoadAll<Block>("Blocks/PaintBlock"));

            DangerBlocks = new List<Block>();
            DangerBlocks.AddRange(Resources.LoadAll<Block>("Blocks/DangerBlock"));

            PlayerPaintBall = Resources.Load<GameObject>("Collectable/PlayerPaintBall");

            Bullet = Resources.Load<Bullet>("Prefabs/Bullet");

            FillerMaterial = Resources.Load<Material>("Images/Textures/Materials/TextureMaterial");

            LoadTextures();
        }

        private static void LoadTextures()
        {
            Textures = new Dictionary<string, List<Texture2D>>();
            List<Texture2D> textures = new List<Texture2D>(Resources.LoadAll<Texture2D>("Images/Textures"));
            foreach (Texture2D texture in textures)
            {
                string textureName = texture.name;
                string baseName = textureName[..textureName.LastIndexOf("_")];
                if (Textures.ContainsKey(baseName))
                {
                    Textures[baseName].Add(texture);
                }
                else
                {
                    Textures.Add(baseName, new List<Texture2D> { texture });
                }
            }
            SortTextures();
        }

        private static void SortTextures()
        {
            foreach (KeyValuePair<string, List<Texture2D>> kvp in Textures)
            {
                List<Texture2D> textures = kvp.Value;

                // Sort the textures based on their names
                textures.Sort(new TextureSorter());
            }
        }
    }
}
