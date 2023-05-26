using PaintRush.Controller;
using PaintRush.Tools;
using PaintRush.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World;

namespace PaintRush.World
{
    /// <summary>
    /// Spawns and manages blocks in the game world.
    /// </summary>
    public class BlockSpawner : MonoBehaviour
    {
        public static BlockSpawner Instance;

        // Length of all spawned blocks in summary
        private float _length;

        // Max size of last block on z origin 
        private float _lastBlockLength;

        // List to add/remove Blocks on the map
        private List<Block> _blocksStack;

        // Counter for number of placed block sections
        private int _id = 0;

        [SerializeField]
        private int _fixedSpawnAmount = -1;

        private RouletteWheelSelection<BlockCollection> _blockRoulette;
        private List<PaintBlock> _paintBlocks;
        private List<Texture2D> _textures;

        public List<PaintBlock> PaintBlocks
        {
            get { return _paintBlocks; }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Randomly select textures
            _textures = ArrayUtils.GetRandomValue(Content.Textures);
            _paintBlocks = new List<PaintBlock>();

            _length = 0;
            _blocksStack = new List<Block>();

            LoadBlocks();
            SpawnStartBlocks();
        }

        private void SpawnStartBlocks()
        {
            // Spawn initial empty blocks
            SpawnBlock(Instantiate(Content.EmptyBlock));
            SpawnBlock(Instantiate(Content.EmptyBlock));

            // Spawn initial blocks
            SpawnBlock(GetNextBlock());
            SpawnBlock(GetNextBlock());
            SpawnBlock(GetNextBlock());
        }

        private void LoadBlocks()
        {
            // Set up the block roulette for different block types

            _blockRoulette = new RouletteWheelSelection<BlockCollection>();

            // Add Paint Blocks to the roulette
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
            // Check if player controller exists
            if (PlayerController.Instance == null) return;

            // Check if the maximum spawn amount has been reached
            if (_fixedSpawnAmount != -1 && _id >= _fixedSpawnAmount) return;

            PlayerController player = PlayerController.Instance;
            float size = transform.position.z + _length - _lastBlockLength * 2.5f;

            // Check if the player's position is beyond the spawn size
            if (player.transform.position.z > size)
            {
                // Remove and destroy the oldest block
                Block block = _blocksStack.First();
                _blocksStack.Remove(block);
                Destroy(block.gameObject);

                // Spawn a new block and check if the maximum spawn amount has been reached
                SpawnBlock(GetNextBlock());
                if (_fixedSpawnAmount > 0 && _id > _fixedSpawnAmount)
                {
                    SpawnFinishLine();
                }
            }
        }

        private void SpawnPaintBlock()
        {
            // Instantiate a finish block and assign a texture
            PaintBlock finishBlock = Instantiate(Content.FinishBlock);
            finishBlock.Renderer.material.mainTexture = _textures[0];

            // Add the finish block to the paint blocks list
            _paintBlocks.Add(finishBlock);
            _textures.RemoveAt(0);

            // Check if there are available textures
            if (_textures.Count <= 0)
            {
                _textures = ArrayUtils.GetRandomValue(Content.Textures);
            }
            // Spawn the finish block
            SpawnBlock(finishBlock);
        }

        private void SpawnFinishLine()
        {
            // Spawn an empty block to create a finish line
            SpawnBlock(Instantiate(Content.EmptyBlock));
            SpawnBlock(Instantiate(Content.EmptyBlock));
            SpawnBlock(Instantiate(Content.EmptyBlock));
            SpawnBlock(Instantiate(Content.EmptyBlock));
            SpawnBlock(Instantiate(Content.EmptyBlock));
        }

        private void SpawnBlock(Block block)
        {
            // Get the bounds of the block and its children
            block.transform.position = Vector3.zero;
            Bounds bounds = new Bounds(block.transform.position, Vector3.zero);
            Renderer[] renderers = block.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }

            float size = bounds.max.z * 2;

            // Assign a name to the block using the ID counter
            block.name = _id.ToString();

            // Calculate the z-position for the block
            float zpos = _length + size / 2;

            // Set the parent and position of the block
            block.transform.SetParent(transform, false);
            block.transform.position = transform.position + new Vector3(0, 0, zpos);

            // Update the total length and the last block length
            _length += size;
            _lastBlockLength = size;

            // Add the block to the stack and increment the ID counter
            _blocksStack.Add(block);
            _id++;

            // Check if it's time to spawn a paint block
            if (_id <= _fixedSpawnAmount && _id % 5 == 0)
            {
                SpawnPaintBlock();
            }
        }

        private Block GetNextBlock()
        {
            // Spin the block roulette to get the next block
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