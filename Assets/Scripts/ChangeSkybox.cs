using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{

    [SerializeField]
    private List<Material> _sky;
    
    void Start()
    {
        RenderSettings.skybox = _sky[Random.Range(0, _sky.Count - 1)];
        
    }

}
