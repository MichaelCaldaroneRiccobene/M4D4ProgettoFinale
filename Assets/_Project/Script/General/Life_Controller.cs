using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Life_Controller : MonoBehaviour, I_IDamage
{
    [Header("Setting")]
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHp = 100;

    public UnityEvent<int, int> onLFChange;
    public UnityEvent onDeath;

    public int Hp { get => hp; set => hp = Mathf.Clamp(value, 0, maxHp); }
    public int MaxHp { get => maxHp; set => maxHp = Mathf.Max(0,value); }

    public void UpdateHp(int ammount)
    {
        int currentHp = Hp;
        Hp += ammount;

        if(currentHp > Hp)
        {
            if (isDead()) OnDead();
            onLFChange?.Invoke(Hp,MaxHp);
        }
        else onLFChange?.Invoke(Hp, MaxHp);
    }

    public void Damage(int ammount) => UpdateHp(ammount);

    public bool isDead() => Hp <= 0;

    private void OnDead() => onDeath?.Invoke();
}
