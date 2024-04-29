using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletManager))]
public class BulletManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        BulletManager bulletManager = (BulletManager)target;

        if (GUILayout.Button("Regenerate Bullets Pool"))
        {
            bulletManager.ClearBulletPool();
            bulletManager.GenerateBulletPool();
        }
    }
}
