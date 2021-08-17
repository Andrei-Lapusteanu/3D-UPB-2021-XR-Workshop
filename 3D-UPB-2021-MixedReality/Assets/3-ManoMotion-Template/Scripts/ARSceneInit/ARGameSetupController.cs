using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class ARGameSetupController : MonoBehaviour
{
    public GameInitPhase gamePhase;
    public static GameInitPhase CurrentGamePhase;
    private GameInitPhase lastInitPhase;

    private GameObject gameInitPhasePlaneDetect;
    private GameObject gameInitPhaseScenePlacement;
    private GameObject gameInitPhaseSceneAdjustments;

    // Start is called before the first frame update
    void Start()
    {
        // Setup vars
        gamePhase = GameInitPhase.PlaneDetection;
        CurrentGamePhase = gamePhase;
        lastInitPhase = GameInitPhase.Done;

        // Setup phases objects
        gameInitPhasePlaneDetect = GameObject.Find("InitPhase1");
        gameInitPhaseScenePlacement = GameObject.Find("InitPhase2");
        gameInitPhaseSceneAdjustments = GameObject.Find("InitPhase3");

        gameInitPhasePlaneDetect.SetActive(false);
        gameInitPhaseScenePlacement.SetActive(false);
        gameInitPhaseSceneAdjustments.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGamePhase != lastInitPhase)
        {
            switch (CurrentGamePhase)
            {
                // Phase 1
                case GameInitPhase.PlaneDetection:
                    gameInitPhasePlaneDetect.SetActive(true);
                    gameInitPhaseScenePlacement.SetActive(false);
                    gameInitPhaseSceneAdjustments.SetActive(false);
                    break;

                // Phase 2
                case GameInitPhase.ScenePlacement:
                    gameInitPhasePlaneDetect.SetActive(false);
                    gameInitPhaseScenePlacement.SetActive(true);
                    gameInitPhaseSceneAdjustments.SetActive(false);
                    break;

                // Phase 3
                case GameInitPhase.SceneAdjustments:
                    gameInitPhasePlaneDetect.SetActive(false);
                    gameInitPhaseScenePlacement.SetActive(false);
                    gameInitPhaseSceneAdjustments.SetActive(true);
                    break;

                // Done, game can start
                case GameInitPhase.Done:
                    gameInitPhasePlaneDetect.SetActive(false);
                    gameInitPhaseScenePlacement.SetActive(false);
                    gameInitPhaseSceneAdjustments.SetActive(false);

                    // Enable game canvas objects
                    foreach (Transform child in GameObject.Find("ARGameCanvas").transform)
                        child.gameObject.SetActive(true);

                    // Enable game controller objects
                    MonoBehaviour[] scripts = GameObject.Find("GameController").GetComponents<MonoBehaviour>();
                    foreach (MonoBehaviour mb in scripts)
                        mb.enabled = true;

                    GetComponent<ARGameSetupController>().enabled = false;

                    break;

                default:
                    break;
            }

            // So we don't keep repeating logic inside update
            lastInitPhase = CurrentGamePhase;
        }
    }

    // Confirm button action (phase 3)
    public void StartGame()
    {
        CurrentGamePhase = GameInitPhase.Done;
    }
}
