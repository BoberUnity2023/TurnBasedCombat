#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace PathGenerator.GridGenerator
{
    [CustomEditor(typeof(GridGenerator))]
    public class GridGeneratorEditor : Editor
    {
        private GridGenerator _gridGenerator;

        private void OnEnable()
        {
            _gridGenerator = (GridGenerator)target;
           
        }

        private void OnGUI()
        {
            GUI.color = Color.cyan;
            GUI.DrawTexture(new Rect(Vector2.zero, new Vector2(300 , 300)), EditorGUIUtility.whiteTexture);
            GUI.color = Color.white;
        }

        public override void OnInspectorGUI()
        {
            TopButtons();
            GenerateButton();
        }


        private void TopButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.EndHorizontal();
        }

        private void GenerateButton()
        {
            if (GUILayout.Button(
                    "Generate Grid"
                ))
            {
                _gridGenerator.GenerateGrid();
            }
        }
    }
}
#endif