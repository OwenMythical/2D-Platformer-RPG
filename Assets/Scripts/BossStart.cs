using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public bool Active = false;
    WizardScript WS;
    void Awake()
    {
        GameObject Wizard = GameObject.FindGameObjectWithTag("Wizard");
        WS = (WizardScript)Wizard.GetComponent("WizardScript");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Active == false)
        {
            if (collision.gameObject.name == "Player")
            {
                Active = true;
                WS.BossStart();
            }
        }
    }

    public void Restart()
    {
        Active = false;
    }
}
