using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace PathGenerator.GridLoader
{
    public class GridLoader
    {
        private readonly AstarPath _astarPath;

        private string _path;
        
        public GridLoader(AstarPath astarPath)
        {
            _astarPath = astarPath;
            _path = Application.dataPath + "/" + SceneManager.GetActiveScene().name + ".bytes";
        }

        public async void SaveGrid()
        {
            Debug.Log(_path);
            var settings = new Pathfinding.Serialization.SerializeSettings();
            byte[] bytes = _astarPath.data.SerializeGraphs(settings);
            await File.WriteAllBytesAsync(_path, bytes);
        }

        public async void LoadGrid(byte[] data)
        {
            byte[] bytes =  await System.IO.File.ReadAllBytesAsync(_path);
            _astarPath.data.DeserializeGraphs(data);
        }
    }
}
