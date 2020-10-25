using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    private static PlayerManager instance;

    public static PlayerManager GetInstance { get { return instance; } }

    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject player;

    internal void KillPlayer() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
