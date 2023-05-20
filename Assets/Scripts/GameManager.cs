using PaintRush.Data;
using System.Collections;
using UnityEngine;
using PaintRush.Input;
using System.Linq;

namespace PaintRush
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField]
        private float _levelDifficult = 1.0f;

        [SerializeField]
        public float LevelDifficult
        {
            get { return _levelDifficult; }
        }

        private void Start()
        {
            if (Application.isMobilePlatform) gameObject.AddComponent<MobileInputManager>();
            else gameObject.AddComponent<DesktopInputManager>();
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
        }
    }
}