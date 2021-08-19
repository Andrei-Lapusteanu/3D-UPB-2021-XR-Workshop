using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private const float BASE_HP = 100;

    private TextMeshProUGUI hpTmp;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        hpTmp = GameObject.Find("TextHP").GetComponent<TextMeshProUGUI>();

        // Setup vars and UI values
        currentHP = BASE_HP;
        hpTmp.text = currentHP.ToString("0");
    }

    public void TakeDamage(float damageAmount)
    {
        // Setup vars and UI values
        currentHP -= damageAmount;
        hpTmp.text = currentHP.ToString("0");

        if (currentHP <= 0)
            Die();
    }

    public void Die()
    {
        // Nothing fancy, just restart the game
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
