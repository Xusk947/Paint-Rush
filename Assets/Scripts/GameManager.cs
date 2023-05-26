using PaintRush.Data;
using System.Collections;
using UnityEngine;
using PaintRush.Input;
using System.Linq;

namespace PaintRush
{
    /// <summary>
    /// Manages the game flow and settings.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private float _levelDifficult = 1.0f;

        /// <summary>
        /// The current level difficulty.
        /// </summary>
        public float LevelDifficult
        {
            get { return _levelDifficult; }
        }

        private void Start()
        {
            if (Application.isMobilePlatform)
                gameObject.AddComponent<MobileInputManager>();
            else
                gameObject.AddComponent<DesktopInputManager>();
        }

        private void Update()
        {
            _levelDifficult += 0.00003f;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnApplicationQuit()
        {
            // Clean up any necessary resources or save game state here.
        }
    }
}
