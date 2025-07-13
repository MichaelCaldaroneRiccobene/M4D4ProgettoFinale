using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Life_Controller : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHp = 100;

    public int Hp { get => hp; set => hp = Mathf.Clamp(value, 0, maxHp); }
    public int MaxHp { get => maxHp; set => maxHp = Mathf.Max(0,value); }

    public void UpdateHp(int ammount)
    {
        int currentHp = Hp;
        Hp += ammount;

        if(currentHp > Hp)
        {
            Debug.Log("Damage");

            if (isDead()) StartCoroutine(Dying());
        }
        else
        {
            Debug.Log("Heal");
        }
    }

    public bool isDead() => Hp <= 0;

    private void Update()
    {
        //Debug.Log(Hp);
    }

    private IEnumerator Dying()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
