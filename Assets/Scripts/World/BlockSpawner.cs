﻿using PaintRush.Controller;
using PaintRush.Tools;
using PaintRush.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World;

namespace PaintRush.World
{
    public class BlockSpawner : MonoBehaviour
    {
        public static BlockSpawner Instance;
        /// <summary>
        /// Length of all spawned blocks in summary
        /// </summary>
        private float _length;
        /// <summary>
        /// Max size of last block on z origin 
        /// </summary>
        private float _lastBlockLength;
        /// <summary>
        /// Use for add/remove Blocks on a map
        /// </summary>
        private List<Block> _blocksStack;
        /// <summary>
        /// To count number of placed block sections
        /// </summary>
        private int _id = 0;

        [SerializeField]
        private int _fixedSpawnAmount = -1;

        private RouletteWheelSelection<BlockCollection> _blockRoulette;
        private List<FinishBlock> _finishBlocks;

        public List<FinishBlock> FinishBlocks
        {
            get { return _finishBlocks; }
        } 

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _length = 0;
            _blocksStack = new List<Block>();

            LoadBlocks();
            SpawnStartBlocks();
        }

        private void SpawnStartBlocks()
        {
            SpawnBlock(Instantiate(Content.EmptyBlock));
            SpawnBlock(Instantiate(Content.EmptyBlock));

            SpawnBlock(GetNextBlock());
            SpawnBlock(GetNextBlock());
            SpawnBlock(GetNextBlock());
        }

        private void LoadBlocks()
        {
            _blockRoulette = new RouletteWheelSelection<BlockCollection>();
            // Add Paint Block to the roulette
            BlockCollection paintBlocks = new BlockCollection
            {
                difficulty = 4,
                roulette = new RouletteWheelSelection<Block>()
            };
            foreach (Block block in Content.PaintBlocks)
            {
                paintBlocks.roulette.Add(block, block.Difficulty);
            }
            _blockRoulette.Add(paintBlocks, paintBlocks.difficulty);

            // Add Danger Blocks to the roulette
            BlockCollection dangerBlocks = new BlockCollection
            {
                difficulty = 1,
                roulette = new RouletteWheelSelection<Block>()
            };
            foreach (Block block in Content.DangerBlocks)
            {
                dangerBlocks.roulette.Add(block, block.Difficulty);
            }
            _blockRoulette.Add(dangerBlocks, dangerBlocks.difficulty);
        }

        private void Update()
        {
            if (PlayerController.Instance == null) return;
            if (_fixedSpawnAmount != -1 && _id >= _fixedSpawnAmount) return;

            PlayerController player = PlayerController.Instance;
            float size = transform.position.z + _length - _lastBlockLength * 2.5f;

            if (player.transform.position.z > size)
            {
                Block block = _blocksStack.First();
                _blocksStack.Remove(block);
                Destroy(block.gameObject);
                SpawnBlock(GetNextBlock());
                if (_id == _fixedSpawnAmount)
                {
                    SpawnFinishLine();
                }
            }
        }

        private void SpawnFinishLine()
        {
            SpawnBlock(Instantiate(Content.EmptyBlock));
            _finishBlocks = new List<FinishBlock>();

            List<Texture2D> _textures = ArrayUtils.GetRandomValue(Content.Textures);

            for (int i = 0; i < _textures.Count; i++)
            {
                FinishBlock finishBlock = Instantiate(Content.FinishBlock);
                finishBlock.Renderer.material.mainTexture = _textures[i];
                _finishBlocks.Add(finishBlock);
                SpawnBlock(finishBlock);
            }
        }

        private void SpawnBlock(Block block)
        {
            // Get the bounds of a GameObject and all its children
            block.transform.position = Vector3.zero;
            Bounds bounds = new Bounds(block.transform.position, Vector3.zero);
            Renderer[] renderers = block.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }
            float size = bounds.max.z * 2;

            block.name = _id.ToString();

            float zpos = _length + size / 2;

            block.transform.SetParent(transform, false);
            block.transform.position = transform.position + new Vector3(0, 0, zpos);
            // Get the z value of the total bounds
            _length += size;
            _lastBlockLength = size;

            _blocksStack.Add(block);
            _id++;
        }

        private Block GetNextBlock()
        {
            return Instantiate(_blockRoulette.Spin().roulette.Spin());
        }

        private struct BlockCollection
        {
            public float difficulty;
            public RouletteWheelSelection<Block> roulette;
        }

        private struct BlockObject
        {
            public Block reference;
            public float difficulty;
        }
    }
}

    