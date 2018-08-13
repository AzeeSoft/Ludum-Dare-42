using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// To be placed on every game level.
///    
/// Not implemented as a Singleton because Unity allows multiple scenes to be loaded simultaneously.
/// A singleton implementation could become catastrophic!
/// </summary>
public class MainLevelManager : MonoBehaviour
{
    /// <summary>
    /// A static reference to the last loaded LevelManager
    /// </summary>
    [HideInInspector] public static MainLevelManager Instance;

    public int MaxStrikes = 4;
    public float MaxTotalWeight = 100f;

    /// <summary>
    /// Although not needed, a private reference to the GameManager, just to display the GameManager's data in the inspector.
    /// </summary>
    [SerializeField]
    private GameManager _gameManager = GameManager.Instance;

    [SerializeField] [ReadOnly] private float _score = 0f;

//    public GameObject WinScreen;
//    public GameObject LoseScreen;

    public int CurrentDifficulty
    {
        get
        {
            // TODO: Use some advanced algo/function to get difficulty in a better way based on the score;
            return 1 + (int)(_score / 10f);
        }
    }

    public bool IsPlaying
    {
        get { return _isPlaying; }
    }

    [SerializeField] [ReadOnly] private bool _isPlaying;
    private GameObject _playerGameObject;
    

    void OnValidate()
    {
        _gameManager.GetProfileList();  // Just to force the profile list to be loaded in the inspector.
    }

    void Awake()
    {
        Instance = this;
        
        LoadGameData();
        _gameManager.GetProfileList();  // Just to force the profile list to be loaded in the inspector.

        _isPlaying = true;

        _playerGameObject = GameObject.FindWithTag("Player");
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            _score += Time.deltaTime;
        }

        if (IsPlaying)
        {
            CheckStrikes();
        }
        if (IsPlaying)
        {
            CheckTotalWeight();
        }
    }

    private void CheckStrikes()
    {
        if (ProductRequestManager.Instance.Strikes >= MaxStrikes)
        {
            OnGameLost();
        }
    }

    private void CheckTotalWeight()
    {
        if (ProductManager.Instance.GetTotalProductWeight() > MaxTotalWeight)
        {
            OnGameLost();
        }
    }

    private void OnGameLost()
    {
        _isPlaying = false;

        Debug.Log("Game Ended");

        // TODO: Show End Game Screen

        SceneManager.LoadScene("LoseScreen");

        
    }

    public GameObject GetPlayerGameObject()
    {
        return _playerGameObject;
    }

    public void LogMessage(string msg)
    {
        Debug.Log(msg);
    }

    [Button]
    void SaveGameData()
    {
        _gameManager.SaveGameData();
    }

    [Button]
    void LoadGameData()
    {
        _gameManager.LoadGameData();
    }

    [Button]
    void ClearGameData()
    {
        _gameManager.ClearGameData(true);
    }
}
