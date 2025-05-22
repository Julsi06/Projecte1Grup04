using UnityEngine;
using UnityEngine.Rendering.Universal;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AddLightToManyObjects : MonoBehaviour
{
    [Tooltip("Assign here the GameObjects to which you want to add the Light2D component.")]
    public GameObject[] targets;

    [Header("Light2D Settings")]
    public Light2D.LightType lightType = Light2D.LightType.Point;
    public Color lightColor = Color.white;
    public float intensity = 1f;
    public float pointLightInnerRadius = 0f;
    public float pointLightOuterRadius = 5f;

    // Call this method to add lights to all targets
    public void AddLights()
    {
        if (targets == null || targets.Length == 0)
        {
            Debug.LogWarning("No targets assigned to AddLightToManyObjects.");
            return;
        }

        foreach (var go in targets)
        {
            if (go == null)
                continue;

            Light2D existingLight = go.GetComponent<Light2D>();
            if (existingLight == null)
            {
                Light2D newLight = go.AddComponent<Light2D>();
                newLight.lightType = lightType;
                newLight.color = lightColor;
                newLight.intensity = intensity;
                newLight.pointLightInnerRadius = pointLightInnerRadius;
                newLight.pointLightOuterRadius = pointLightOuterRadius;

#if UNITY_EDITOR
                EditorUtility.SetDirty(go);
#endif
                Debug.Log($"Added Light2D to {go.name}");
            }
            else
            {
                Debug.Log($"{go.name} already has a Light2D component.");
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AddLightToManyObjects))]
    public class AddLightToManyObjectsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AddLightToManyObjects script = (AddLightToManyObjects)target;
            if (GUILayout.Button("Add Lights to Targets"))
            {
                script.AddLights();
            }
        }
    }
#endif
}

