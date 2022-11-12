using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private enum State
    {
        NoLoseConTarget, LoseConTarget
    }
    private State state;

    public float unitCost;
    [HideInInspector] public GameObject target;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject warningUI;

    private void Start()
    {
        gamemanager.enemies.Add(gameObject);
        LoseConTargetOff();
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.NoLoseConTarget:
                if (target != null)
                    if (target.name == "LoseCon") LoseConTargetOn();
                break;
            case State.LoseConTarget:
                if (target == null || target.name != "LoseCon") LoseConTargetOff();
                break;
        }
        warningUI.transform.LookAt(new Vector3(1, gamemanager.camera.transform.position.y * 10, gamemanager.camera.transform.position.z * 10));
    }
    void LoseConTargetOn()
    {
        state = State.LoseConTarget;
        anim.SetInteger("State", 1);
    }
    void LoseConTargetOff()
    {
        state = State.NoLoseConTarget;
        anim.SetInteger("State", 0);
    }
}
