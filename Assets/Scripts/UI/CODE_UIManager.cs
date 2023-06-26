using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class CODE_UIManager : MonoBehaviour
{
    public GameObject panelGameOver;
    public GameObject panel;
    public Image equippedItem;
    public Image newItem;
    public TextMeshProUGUI nameEQ;
    public TextMeshProUGUI bonusEq;
    public TextMeshProUGUI nameNew;
    public TextMeshProUGUI bonusNew;

    public bool desejatrocar = false;


    private void Start()
    {
        panel.SetActive(false);
        panelGameOver.SetActive(false);
    }
    public void OnChangedItem()
    {
        panel.SetActive(true);

    }
    public void GetImageEquippedItem(Sprite itemImage, string name, string bonus)
    {
        equippedItem.sprite = itemImage;
        nameEQ.text = name;
        bonusEq.text = "+ " + bonus;
    }
    public void Gameover()
    {
        panelGameOver.SetActive(true);
    }
    public void GetNewItemImage(Sprite itemImage, string n, string b)
    {
        newItem.sprite = itemImage;
        nameNew.text = n;
        bonusNew.text = "+ " + b;
    }

    public void YesChange()
    {
        panel.SetActive(false);
        desejatrocar = true;
        GameManager.Instance.menuState = false;

        // Verifica se há um item sendo exibido na interface
        if (newItem.sprite != null)
        {
            // Chama o método Trocar() no item correspondente ao item exibido na interface
            ItemCollected item = GameManager.Instance.GetDisplayedItem();
            if (item != null)
            {
                item.Trocar();
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Destroy(gameObject);
    }


    public void NoChange()
    {
        panel.SetActive(false);
        desejatrocar = false;
        GameManager.Instance.menuState = false;

        // Verifica se há um item sendo exibido na interface
        if (newItem.sprite != null)
        {
            GameManager.Instance.DestroyDisplayedItem();
        }
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }



}
