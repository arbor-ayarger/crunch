using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLightSensitive : MonoBehaviour
{
    public List<Renderer> renderers = new List<Renderer>(); // TODO : Replace with auto-renderer-finder when ArborCodebase arrives.

    List<Material> materials = new List<Material>(); // Should be all unlit_tinted materials.

    void Start()
    {
        for(int i = 0; i < renderers.Count; i++)
        {
            //Material new_mat = new Material(renderers[i].material);
            //renderers[i].material = new_mat;
            //materials.Add(new Material(new_mat));

            for(int m = 0; m < renderers[i].materials.Length; m++)
            {
                //Material new_mat = new Material(renderers[i].sharedMaterials[m]);
                //renderers[i].sharedMaterials[m] = new_mat;
                materials.Add(renderers[i].materials[m]);

            }

        }

        Debug.Log("Found materials " + materials.Count);
    }

    void Update()
    {
        Color light_tint = GetLightTint();

        foreach(Material mat in materials)
        {
            mat.SetColor("_Tint", light_tint);
            Debug.Log("set tint [" + light_tint + "]");
        }
    }

    Color GetLightTint ()
    {
        Color result = Color.black;

        for(int i = 0; i < IsLight.lights.Count; i++)
        {
            IsLight is_light = IsLight.lights[i];
            Light light = is_light.GetComponent<Light>();

            float distance = Vector3.Distance(transform.position, light.transform.position);
            float light_range = light.range * 1.5f;

            result += Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 1.0f), light.color, 1.0f - (distance / light_range));
        }


        result = new Color(Mathf.Max(0.2f, result.r), Mathf.Max(0.2f, result.g), Mathf.Max(0.2f, result.b), 1.0f);
        return result;
    }
}
