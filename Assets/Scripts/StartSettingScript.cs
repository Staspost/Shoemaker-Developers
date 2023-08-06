using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartSettingScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textDiff;
    [SerializeField]
    private Slider sliderDiff;

    [SerializeField]
    private Image fill;

    private int diff;

    private Color c1 = new Color(0.3909745f, 1f, 0.03301889f, 1f);
    private Color c2 = new Color(0.1335269f, 0.3584906f, 0f, 1f);
    private Color c3 = new Color(0.08056474f, 0.06003024f, 0.6698113f, 1f);
    private Color c4 = new Color(0.9312398f, 0.9811321f, 0.04165182f, 1f);
    private Color c5 = new Color(0.8584906f, 0.03644534f, 0.7036499f, 1f);
    private Color c6 = new Color(0.9339623f, 0.4273515f, 0.04846031f, 1f);
    private Color c7 = new Color(0.9333333f, 0.2314164f, 0.04705882f, 1f);
    private Color c8 = new Color(1f, 0.0489275f, 0f, 1f);
    private Color c9 = new Color(0.4433962f, 0.05233593f, 0.02300641f, 1f);


    private void Start()
    {
        if (PlayerPrefs.HasKey("diff"))
        {
            diff = PlayerPrefs.GetInt("diff");
            sliderDiff.value = diff;
        }
        else 
        {
            diff = 1;
            sliderDiff.value = diff;
            PlayerPrefs.SetInt("diff", diff);
            PlayerPrefs.Save();
        }
    }

    private void FixedUpdate()
    {
        if (sliderDiff.value == 1)
        {
            textDiff.text = "Легко";
            fill.color = c1;
        }
        else if (sliderDiff.value == 2)
        {
            textDiff.text = "Нормально";
            fill.color = c4;
        }
        else if (sliderDiff.value == 3)
        {
            textDiff.text = "Тяжело";
            fill.color = c7;
        }
        else
        {
            textDiff.text = "";
            fill.color = Color.grey;
        }
    }

    public void LoadMenu()
    {
        diff = (int)sliderDiff.value;        
        PlayerPrefs.SetInt("diff", diff);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }


}
