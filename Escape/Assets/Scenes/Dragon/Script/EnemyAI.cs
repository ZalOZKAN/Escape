using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkspped, chaseSpeed, MinidleTime,MaxidleTime,idleTime,rayCastDistance,cathDistance,chaseTime,minChaseTime,maxChaseTime,jumpScareTime;
    public int destinitionAmount;
    public bool walking, chassing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum,randNum2;
    public Vector3 rayCastOfset;
    public string deathScene;


    private void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinitionAmount);
        currentDest = destinations[randNum];
    }
    private void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position+rayCastOfset, direction, out hit,rayCastDistance)) {
            if(hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");

                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                aiAnim.SetTrigger("sprint");
                chassing = true;
            }

        }
        if(chassing==true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;

            if (ai.remainingDistance <= cathDistance)
            {
                player.gameObject.SetActive(false);
                aiAnim.ResetTrigger("sprint");
                aiAnim.SetTrigger("jumpscare");
                StartCoroutine(deathRoutine());
                chassing=false;
            }

        }
        if (walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkspped;
            if(ai.remainingDistance <= ai.stoppingDistance)
            {
                randNum2 = Random.Range(0, 2);
                if(randNum2 == 0) {

                    randNum = Random.Range(0, destinitionAmount);
                    currentDest = destinations[randNum];

                }
                if(randNum == 1)
                {
                    aiAnim.ResetTrigger("walk");
                    aiAnim.SetTrigger("idle");
                    StopCoroutine("stayIdle");
                    StartCoroutine("stayIdle");
                    walking = false;

                }
            }


        }
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(MinidleTime, MaxidleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true ;
        randNum = Random.Range(0, destinitionAmount);
        currentDest = destinations[randNum];
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime,maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chassing = false;
        randNum = Random.Range(0, destinitionAmount);
        currentDest = destinations[randNum];
        aiAnim.ResetTrigger("sprint");
        aiAnim.SetTrigger("walk");
    }
    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpScareTime);
        SceneManager.LoadScene(deathScene);
    }



}
