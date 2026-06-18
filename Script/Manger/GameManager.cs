using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField]private Player player;

    private State state;

    private float maitingToStartTimer = 1;
    private float countDownToStarTimer = 3;
    private float gamePlayingTimer = 10;
 

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        TurnToWaitingToStart();

    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.WaitingToStart:
                maitingToStartTimer -= Time.deltaTime;
                if (maitingToStartTimer <= 0)
                {
                    TurnToCountDownToStart();
                }
                break;

            case State.CountDownToStart:
                countDownToStarTimer -= Time.deltaTime;
                if (countDownToStarTimer <= 0)
                {
                    TurnToGamePlaying();
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurnToGameOver();
                }
                break;

            case State.GameOver:
                break;

            default:
                break; ;
        }
    }

    public void TurnToWaitingToStart()
    {
        state = State.WaitingToStart;
        DisabePlayer();
        OnStateChanged?.Invoke(this,EventArgs.Empty);
    }

    private void TurnToCountDownToStart()
    {
        state = State.CountDownToStart;
        DisabePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnToGameOver()
    {
        state = State.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void DisabePlayer()
    {
        player.enabled = false;
    }
    private void EnablePlayer()
    {
        player.enabled = true;
    }

    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }

    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }

    public float GetCountDownTimer()
    {
        return countDownToStarTimer;
    }

}
