using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private int _currentIndex = 0;
    private float _elapsedTime = 0;
    public float delay = 2f;
    public Material[] Materials;
    public Renderer targetRenderer;
    void Start()
    {
        targetRenderer.material = Materials[_currentIndex];
    }

 
    void Update()
    {

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime <= delay) return;
        _elapsedTime = 0;
        _currentIndex++;
        if (_currentIndex >= Materials.Length) _currentIndex = 0;
        targetRenderer.material = Materials[_currentIndex];
    }
}
