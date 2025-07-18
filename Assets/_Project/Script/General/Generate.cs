using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    [SerializeField] private GameObject[] objs;
    [SerializeField] private int xNumber;
    [SerializeField] private int zNumber;
    [SerializeField] private int yNumber;
    [SerializeField] private float space = 1;
    [SerializeField] private float timeSpawn = 1;
    [SerializeField] private bool isRandomPos;

    private Vector3 startPos;
    private Vector3 upDatePos;

    private void Start()
    {
        startPos = transform.position;
        upDatePos = transform.position;

        StartCoroutine(GenerateObj());
    }

    private IEnumerator GenerateObj()
    {
        for (int i = 0; i < zNumber; i++)
        {
            for(int j = 0; j < yNumber; j++)
            {
                for(float k = 0; k < xNumber; k++)
                {         
                    int randomObj = Random.Range(0,objs.Length);
                    GameObject obj =  Instantiate(objs[randomObj],upDatePos,Quaternion.identity,transform);

                    if (isRandomPos) obj.transform.rotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
                    upDatePos.x += space;
                    yield return new WaitForSeconds(timeSpawn);
                }
                upDatePos.x = startPos.x;
                upDatePos.y += space;
            }
            upDatePos.y = startPos.y;
            upDatePos.z += space;
        }
    }
}
