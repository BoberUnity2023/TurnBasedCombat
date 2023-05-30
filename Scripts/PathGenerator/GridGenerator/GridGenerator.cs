using System;
using UnityEngine;

namespace PathGenerator.GridGenerator
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private AstarPath _astarPath;

        private GridLoader.GridLoader _gridLoader;

        public void GenerateGrid()
        {
            _gridLoader = new GridLoader.GridLoader(_astarPath);
            _astarPath.Scan();
            _gridLoader.SaveGrid();
        }
    }
}
