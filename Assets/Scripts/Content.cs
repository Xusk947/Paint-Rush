using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Content : MonoBehaviour
{
    /// --- BLOCK SECTION ---
    public static Block EmptyBlock, FinishBlock;
    public static List<Block> PaintBlocks, DangerBlocks;
    private void Awake()
    {
        EmptyBlock = Resources.Load<Block>("Blocks/EmptyBlock");
        FinishBlock = Resources.Load<Block>("Blocks/FinishBlock");

        PaintBlocks = new List<Block>();
        PaintBlocks.AddRange(Resources.LoadAll<Block>("Blocks/PaintBlock"));

        DangerBlocks = new List<Block>();
        DangerBlocks.AddRange(Resources.LoadAll<Block>("Blocks/DangerBlock"));
    }
}
