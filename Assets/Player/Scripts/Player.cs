using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField]
    private RoadSpawner _roadSpawner;

    //[SerializeField]
    private float speed = 10f;
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private TMP_Text textSpeed;
    [SerializeField]
    private TMP_Text textTimer;

    [SerializeField]
    private TMP_Text textEndGame;
    [SerializeField]
    private TMP_Text textLive;
    [SerializeField]
    private TMP_Text textBugs;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Slider sliderBugs;
    [SerializeField]
    private Slider sliderReady;

    private int timer;
    private int live;
    private int bugs;
    private int congestion; //забагованность
    private int readiness; //готовность
    private int diff; //сложность
    private float diffFactor;

    private float timerInterval;

    [SerializeField]
    private GameObject character;

    //[SerializeField]
    //private AudioSource audios;
    //[SerializeField]
    //private AudioSource audioShot;
    //[SerializeField]
    //private AudioClip backgroungAudio;
    //[SerializeField]
    //private AudioClip winAudio;
    //[SerializeField]
    //public AudioClip collectAudio;
    //[SerializeField]
    //private AudioClip failAudio;




    void Start()
    {
        if (PlayerPrefs.HasKey("diff"))
        {
            diff = PlayerPrefs.GetInt("diff");            
        }
        else
        {
            diff = 1;            
            PlayerPrefs.SetInt("diff", diff);
            PlayerPrefs.Save();
        }
        if (diff == 1) diffFactor = 0.5f;
        else if (diff == 2) diffFactor = 1f;
        else if (diff == 3) diffFactor = 1.5f;

        //audios = GetComponent<AudioSource>();
        //audioShot = GetComponent<AudioSource>();
        //audioShot.Play(10);
        //audios.PlayOneShot(backgroungAudio);

        rb = GetComponent<Rigidbody>();
        timer = 90;
        //live = 3;
        bugs = 0;
        readiness = 30;
        congestion = 75;
        StartCoroutine(UpdateTimerTick());
        textLive.text = live.ToString();
        textBugs.text = bugs.ToString();
        slider.value = 0.5f;
        timerInterval = 1f;

    }

    IEnumerator UpdateTimerTick()
    {
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(timerInterval);
            timer--;
            textTimer.text = timer.ToString() + " сек.";            
        }
        EndGame(3);
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + Vector3.right * moveX * speed * Time.deltaTime);
        //sfera.transform.Rotate(0, 0, 5f, Space.Self);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //rb.AddForce(Vector2.up * 500);           
        //}      
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            //case "blue":                
                //Road._speed = Road._speed + diffFactor * 3;                
                //speed = speed + 10f * diffFactor;
                //audioShot.PlayOneShot(collectAudio, 1.0f);
                //audioShot.Play(10);
                //break;
            case "red":
                Road._speed = Road._speed - diffFactor * 3;
                speed = speed - 10f * diffFactor;
               // audioShot.Play(10);
                break;
            case "green":
                Road._speed = Road._speed + diffFactor * 3;
                speed = speed + 10f * diffFactor;
               // audioShot.Play(10);
                break;
            case "white":
                //live--;
                //UpdateDisplayLive(live);
                int rnd = Random.Range(0, 100);
                //Debug.Log(rnd);
                if (rnd > 50)
                {
                    Road._speed = Road._speed + diffFactor * 3;
                    speed = speed + 10f * diffFactor;
                }
                else {
                    Road._speed = Road._speed - diffFactor * 3;
                    speed = speed - 10f * diffFactor;
                }
                break;
            //case "bluebox":
                //live--;
                //UpdateDisplayLive(live);
                //break;
            case "dead":
                EndGame(2);
                break;
            case "bug":
                //audioShot.Play(10);
                bugs++;
                slider.value = slider.value + 0.1f;
                if (slider.value > 1f) slider.value = 1f;
                timerInterval = timerInterval + slider.value;
                congestion--;
                sliderBugs.value = congestion;
                readiness = readiness + Random.Range(1, 5);
                sliderReady.value = readiness;
                UpdateDisplayBugs(bugs);
                Destroy(other.gameObject);
                break;
            case "bug_min_time":                
                bugs++;
                slider.value = slider.value - 0.1f;
                if (slider.value < 0.1f) slider.value = 0.1f;
                timerInterval = timerInterval - slider.value;
                congestion--;
                sliderBugs.value = congestion;
                readiness = readiness + Random.Range(1, 5);                
                sliderReady.value = readiness;
                UpdateDisplayBugs(bugs);
                Destroy(other.gameObject);
                break;
            case "road":
                _roadSpawner.Spawn();
                break;
            case "sfera_red":                
                readiness = readiness + Random.Range(1, 4);
                sliderReady.maxValue = sliderReady.maxValue + Random.Range(3, 7);
                sliderReady.value = readiness;
                congestion = congestion + Random.Range(0, 2);
                sliderBugs.value = congestion;
                Destroy(other.gameObject);
                if (readiness*100/sliderReady.maxValue < 10) EndGame(4);
                break;
            case "sfera_yellow":
                //audioShot.Play(10);
                readiness = readiness + Random.Range(1, 5);                
                sliderReady.value = readiness;
                Destroy(other.gameObject);
                break;
            case "sfera_green":
                //audioShot.Play(10);
                sliderReady.maxValue = sliderReady.maxValue - Random.Range(3, 10);
                //sliderBugs.maxValue = sliderBugs.maxValue + Random.Range(5, 10);
                congestion = congestion + Random.Range(3, 6);
                sliderBugs.value = congestion;
                Destroy(other.gameObject);
                break;
            case "pass_bug":
                readiness = readiness - 1;
                sliderReady.value = readiness;
                congestion++;
                //sliderBugs.maxValue = sliderBugs.maxValue + Random.Range(1, 10);
                sliderBugs.value = congestion;
                if (congestion == sliderBugs.maxValue) EndGame(5);
                if (readiness * 100 / sliderReady.maxValue < 10) EndGame(4);
                break;
        }

        if (speed < 10f) speed = 10f;
        if (Road._speed < 20f) Road._speed = 20f;
        UpdateDisplaySpeed(Road._speed);
    }

    private void UpdateDisplaySpeed(float speed)
    {
        textSpeed.text = speed.ToString() + " зн/сек";
        if (speed > 100) EndGame(0);
    }

    //private void UpdateDisplayLive(int live)
    //{
       // textLive.text = live.ToString();
       // if (live <= 0) EndGame(1);
    //}

    private void UpdateDisplayBugs(int bugs)
    {
        textBugs.text = bugs.ToString();        
    }

    private void Win()
    {
        textEndGame.text = "Вы победили!";
        StartCoroutine(RestartGame());
    }


    private void EndGame(int typeEndGame)
    {
        if (typeEndGame == 3)
        {
            if (readiness * 100 / sliderReady.maxValue >= 80 && readiness >= 100 && congestion * 100 / sliderBugs.maxValue < 10)
            {
                Win();
            }
            else if (readiness * 100 / sliderReady.maxValue >= 80 && readiness < 100 && congestion * 100 / sliderBugs.maxValue < 10)
            {
                typeEndGame = 6;
            }
            else if (congestion * 100 / sliderBugs.maxValue > 50)
            {
                typeEndGame = 5;
            }
            else
            {
                typeEndGame = 7;
            }

        }
        
        
        
        Road._speed = 0f;
        speed = 0f;
        character.SetActive(false);
        switch (typeEndGame)
        {
            case 0:
                textEndGame.text = "Вы превысили скорость и проиграли!";
            break;
            case 1:
                textEndGame.text = "Ваши жизни закончились и Вы проиграли!";
            break;
            case 2:
                textEndGame.text = "Вы покинули трассу и проиграли!";
            break;
            case 3:
                textEndGame.text = "Врата не готовы, Вы проиграли!";
            break;
            case 4:
                textEndGame.text = "Ваш проект неинтересен, Вы проиграли!";
            break;
            case 5:
                textEndGame.text = "Баги съели Ваш проект, Вы проиграли!";
            break;
            case 6:
                textEndGame.text = "ВАМ ПОЧТИ УДАЛОСЬ - вы написали хороший проект, но был тот, у кого он КРУЧЕ!";
            break;
            case 7:
                textEndGame.text = "Время истекло, Вы не успели завершить проект!";
            break;

        }
        
        StartCoroutine(RestartGame());

    }
    IEnumerator RestartGame()
    {        
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(3);
    }
}
