using System.Collections;
using UnityEngine;

public class WizardScript : MonoBehaviour
{
    bool Active = false;
    System.Random RNG = new();
    int Timer = 0;
    int Attack = 0;
    Vector3 StartPos;
    public GameObject WizardS;
    BossStart WS;
    EnemyHealthScript WH;
    public GameObject BO1;
    ProjectileScript BOS1;
    public GameObject BO2;
    ProjectileScript BOS2;
    public GameObject BO3;
    ProjectileScript BOS3;
    public GameObject BO4;
    ProjectileScript BOS4;

    void Awake()
    {
        WS = (BossStart)WizardS.GetComponent("BossStart");
        WH = (EnemyHealthScript)gameObject.GetComponent("EnemyHealthScript");
        BOS1 = (ProjectileScript)BO1.GetComponent("ProjectileScript");
        BOS2 = (ProjectileScript)BO2.GetComponent("ProjectileScript");
        BOS3 = (ProjectileScript)BO3.GetComponent("ProjectileScript");
        BOS4 = (ProjectileScript)BO4.GetComponent("ProjectileScript");
        StartPos = gameObject.transform.position;
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (Active == true)
            {
                Timer += 1;
                if (Timer > 5)
                {
                    if (Attack > 5)
                    {
                        Timer = 0;
                        gameObject.transform.position = StartPos + new Vector3(0, -1.5f, 0);
                        StartCoroutine(Return(5f, true));
                    }
                    else
                    {
                        Timer = 0;
                        Attack += 1;
                        int PosOffset = RNG.Next(-5, 6);
                        gameObject.transform.position = StartPos + new Vector3(PosOffset, 0, 0);
                        StartCoroutine(SummonOrbs());
                    }
                }
            }
        }
    }

    public void BossStart()
    {
        Active = true;
        gameObject.transform.position = StartPos;
        Attack = 0;
        Timer = 0;
        WH.Heal();
    }

    IEnumerator SummonOrbs()
    {
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(BOS1.Fired(gameObject.transform.position,RNG.Next(3,5),2));
        StartCoroutine(BOS2.Fired(gameObject.transform.position,RNG.Next(3,5),-2));
        StartCoroutine(BOS3.Fired(gameObject.transform.position,RNG.Next(3,5),RNG.Next(5,9)));
        StartCoroutine(BOS4.Fired(gameObject.transform.position,RNG.Next(3,5),RNG.Next(-8,-4)));
    }
    public IEnumerator Return(float Time,bool ActiveState)
    {
        Active = false;
        yield return new WaitForSeconds(Time);
        gameObject.transform.position = StartPos;
        Attack = 0;
        Timer = 0;
        Active = ActiveState;
        WS.Active = ActiveState;
    }
}
