using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Mirror;

public class GameManagerMultiplayer : NetworkBehaviour
{
    [SerializeField] private string startText;
    public MovePlatform[] platforms;
    [HideInInspector] public PointToPlatformMultiplayer pointCreateManager;
    private PlatformForDots[] platformsForPoints;
    public WinCase[] winCases;

    [Header("Win and Lose")]
    [SerializeField] private GameObject panelWithWinName;
    [SerializeField] private Text winText;
    public PointStructur[] pointsTypes;
    [SerializeField] private float timeBeforePanel;
    private bool openedWinPanel;
    [Header("Next Motion")]
    public Animator motionImage;
    [HideInInspector] public int motionGame = 1;
    public Text motionText;
    [HideInInspector] public Color nextTextColor;
    [HideInInspector] public string nextTriggerForMotionText;
    private Music audioSource;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            platformsForPoints = GameObject.FindObjectsOfType<PlatformForDots>();
        }
        else
        {
            audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Music>();
        }
    }


    public void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (pointCreateManager != null)
                pointCreateManager.canCreatePlayer = true;
            else
                pointCreateManager = GameObject.FindAnyObjectByType<PointToPlatformMultiplayer>();
            foreach (PlatformForDots platform in platformsForPoints)
            {
                if (platform.currentPoint != null && platform.currentPoint.GetComponent<Point>().isMoving)
                {
                    pointCreateManager.canCreatePlayer = false;
                    break;
                }
            }
            if (!openedWinPanel)
            {
                if (pointCreateManager != null && pointCreateManager.canCreatePlayer && !pointCreateManager.GetComponent<MovePointWithScreenMultiPlayer>().canMovePoint)
                {
                    foreach (WinCase winCase in winCases)
                    {
                        bool winWops = winCase.CheckWin(true);
                        bool winPlex = winCase.CheckWin(false);


                        if (!winWops && !winPlex && CheckPlatformsIsFull() || winPlex & winWops)
                        {
                            pointCreateManager.canCreatePlayer = false;
                            if (timeBeforePanel <= 0)
                            {
                                winText.text = "TIE!";
                                panelWithWinName.SetActive(true);
                            }
                            else
                            {
                                timeBeforePanel -= Time.deltaTime;
                            }
                            openedWinPanel = true;
                            break;
                        }
                        else if (winWops && !winPlex)
                        {
                            pointCreateManager.canCreatePlayer = false;
                            if (timeBeforePanel <= 0)
                            {
                                foreach (PointStructur point in pointsTypes)
                                {
                                    if (point.name == "Wops")
                                    {
                                        winText.color = point.textColor;
                                        winText.text = "Won " + point.name + "!";
                                    }
                                }
                                panelWithWinName.SetActive(true);
                                openedWinPanel = true;
                            }
                            else
                            {
                                timeBeforePanel -= Time.deltaTime;
                            }
                            break;
                        }
                        else if (!winWops && winPlex)
                        {
                            pointCreateManager.canCreatePlayer = false;
                            if (timeBeforePanel <= 0)
                            {
                                foreach (PointStructur point in pointsTypes)
                                {
                                    if (point.name == "Plex")
                                    {
                                        winText.color = point.textColor;
                                        winText.text = "Won " + point.name + "!";
                                    }
                                }
                                panelWithWinName.SetActive(true);

                                openedWinPanel = true;
                            }
                            else
                            {
                                timeBeforePanel -= Time.deltaTime;
                            }

                            break;
                        }

                    }
                }
            }


        }

    }

    bool CheckPlatformsIsFull()
    {
        bool AllIsFull = true;
        foreach (PlatformForDots platform in platformsForPoints)
        {
            if (!platform.IsFull)
            {
                AllIsFull = false;
                break;
            }

        }
        return AllIsFull;
    }

    public void ChangeColorTextMotion(Color color)
    {
        motionText.color = color;
    }

    public void NextMotion()
    {
        motionText.color = nextTextColor;
        motionImage.SetTrigger(nextTriggerForMotionText);
        foreach (MovePlatform platform in platforms)
        {
            platform.MovePoint();
            platform.transform.GetComponent<PlatformForDots>().NextPointToCurrent();
        }
        motionGame++;
        motionText.text = startText + Convert.ToString(motionGame);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToOtherScene(int id)
    {
        SceneManager.LoadScene(id);
    }
    public void ChangeLang()
    {
        if (PlayerPrefs.GetString("lang") == "EN")
            PlayerPrefs.SetString("lang", "RU");
        else if (PlayerPrefs.GetString("lang") == "RU")
            PlayerPrefs.SetString("lang", "EN");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeMuteAudio(Text textButton)
    {
        bool isMute = audioSource.ChangeMute();
        if (isMute)
        {
            textButton.text = "Switch on Music";
        }
        else
        {
            textButton.text = "Switch off Music";
        }
    }

    [System.Serializable]
    public struct PointStructur
    {
        public string name;
        public Color textColor;
    }
}
