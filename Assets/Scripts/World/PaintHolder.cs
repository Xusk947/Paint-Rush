using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PaintRush.World
{
    public class PaintHolder : MonoBehaviour
    {
        private Stack<GameObject> _paintBalls;
        [SerializeField, ReadOnly(true)]
        private Vector3 _size;
        private int _horizontalRow = 3, _currentHorizontalRow, _currentVerticalRow = 0;

        public Stack<GameObject> PaintBalls
        {
            get { return _paintBalls; }
        }
        private void Start()
        {
            _size = Content.PlayerPaintBall.GetComponent<Renderer>().bounds.max;
            _paintBalls = new Stack<GameObject>();
            _currentHorizontalRow = -_horizontalRow;
        }

        public void AddItem(PaintItem paintItem)
        {
            for(int i = 0; i < paintItem.Value; i++)
            {
                GameObject paintBall = SpawnPaintBall();
                paintBall.GetComponent<Renderer>().material.color = paintItem.Color;
                paintBall.transform.SetParent(transform);
                paintBall.transform.localPosition = new Vector3(_size.x * _currentHorizontalRow * 2f, _size.y * _currentVerticalRow * 2f, 0);
                _paintBalls.Push(paintBall);
                _currentHorizontalRow++;
                if (_currentHorizontalRow > _horizontalRow)
                {
                    _currentHorizontalRow = -_horizontalRow;
                    _currentVerticalRow++;
                }
            }
        }

        public void RemoveItem()
        {
            GameObject paintBall = _paintBalls.Pop();
            Destroy(paintBall);
        }
        private GameObject SpawnPaintBall()
        {
            return Instantiate(Content.PlayerPaintBall);
        }
    }
}
