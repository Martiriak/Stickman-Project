using UnityEngine;
using UnityEditor;

namespace Stickman.Levels.Editors
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        /*private Obstacle mObstacleEdited;

        private void OnEnable() => mObstacleEdited = (Obstacle) target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Bounding Box"))
            {
                mObstacleEdited.ComputeBounds();
            }
        }*/
    }
}
