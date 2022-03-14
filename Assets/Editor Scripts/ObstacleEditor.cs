using UnityEngine;
using UnityEditor;

namespace Stickman.Obstacles.Editors
{
    [CustomEditor(typeof(Obstacle))]
    public class ObstacleEditor : Editor
    {
        private Obstacle mObstacleEdited;

        private void OnEnable() => mObstacleEdited = (Obstacle) target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Bounding Box"))
            {
                mObstacleEdited.ComputeBounds();
            }
        }
    }
}
