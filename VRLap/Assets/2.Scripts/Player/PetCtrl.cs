// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.Serialization;
// using Unity.VisualScripting;
// using Unity.XR.CoreUtils;
// using UnityEngine;
// using UnityEngine.AI;
// using UnityEngine.Rendering;
// using UnityEngine.UI;


// public enum State
// {
//     WALK,
//     IDLE,
//     SLEEP,
//     REST,
//     DANCE,
//     EATING,
//     DEAD,
// }

// public class PetCtrl : MonoBehaviour
// {
//     private NavMeshAgent agent;
//     private Animator animator;
//     public GameObject interactableMenu;
//     private Transform bed;
//     public bool interactable = true;
//     private bool isMove;
//     public State curState;

//     [HideInInspector]
//     public bool isSleep;

//     Coroutine curCoroutine;

//     [Range(0,1)]
//     public float fullness = 1.0f; // 1 = 최대 배부름 (100%)
//     private float hungerDecayRate = 1f/200f; // 초당 감소율 (1초에 5%)
//     private float hungerStandard = 0.3f; // 배고픔 상태 기준 (30%)
    


//     [Range(0,1)]
//     public float happiness = 1.0f;
//     private float happinessDecayRate = 1f/200f;
//     private float unhappyStandard = 0.5f;

//     [Range(0,1)]
//     public float stamina = 1.0f;
//     private float staminaDecayRate = 1f/200f;
//     private float tiredStandard = 0.3f;


//     public bool isTired;
//     public bool unHappy = false;
//     public bool isHungry = false;


//     public SpriteRenderer hungerBar;
//     public SpriteRenderer staminaBar;
//     public SpriteRenderer happinessBar;
    
    
//     [SerializeField]
//     Material defaultMat;
//     [SerializeField]
//     Material outlineMat;

//     private void Awake() {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponentInChildren<Animator>();
//         bed = GameObject.FindWithTag("Bed").transform;

//     }
//     void Start() {
//         StartCoroutine(StatusDecay());
//         interactableMenu.SetActive(false);
//         StartCoroutine(StateStart());
//     }

//     private void Update()
//     {
//         if(agent.velocity.magnitude>0.1f)
//         {
//             isMove = true;
//         }
//         else
//         {
//             isMove = false;
//         }
//         animator.SetBool("Move", isMove);

//         if(curState == State.DEAD)
//         {
//             StopAllCoroutines();
//             animator.SetBool("Die",true);
//         }
//     }

//     public IEnumerator StateStart()
//     {
//         if(!interactable)
//             yield break;

//         if(curCoroutine != null)
//             StopCoroutine(curCoroutine);

//         if(TimeManager.Instance.hour>21||TimeManager.Instance.hour<7)
//         {
//             curState = State.SLEEP;
//         }
//         yield return curCoroutine = StartCoroutine(curState.ToString());
//     }

//     IEnumerator IDLE()
//     {   
//         if(TimeManager.Instance.hour>21&&TimeManager.Instance.hour<8)
//         {
//             curState = State.SLEEP;
//             yield return null;
//         }
//         Debug.Log("Idle Start");
//         float idleChoice = Random.Range(0,2);
//         animator.SetFloat("IdleChoice",idleChoice);
//         yield return new WaitForSeconds(5f);

//         int actChoice = Random.Range(0,3);

//         if(actChoice == 0||actChoice == 1)
//         {
//             curState = State.WALK;
//             StartCoroutine(StateStart());
//         }
//         else
//         {
//             curState = State.IDLE;
//             StartCoroutine(StateStart());
//         }
//     }

//     IEnumerator WALK()
//     {
//         Debug.Log("WALK");
//         int movingCnt = Random.Range(0,4);


//         for(int i=0; i<movingCnt; i++)
//         {
//             Vector3 movePoint = transform.position;

//             bool found = false;

//             while(!found)
//             {
//                 movePoint = Random.onUnitSphere*3.5f;
//                 movePoint += transform.position;

//                 NavMeshHit hitInfo;

//                 if (NavMesh.SamplePosition(movePoint, out hitInfo, 10f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인합니다.
//                 {
//                     movePoint = hitInfo.position; // NavMesh 위의 랜덤 위치를 반환합니다.
//                     found = true;
//                 }
//             }

//             agent.SetDestination(movePoint);
//             //isMove = true;

            

//             while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
//             {

//                 yield return null; // 다음 프레임까지 대기
//             }
//             //isMove = false;

//             yield return new WaitForSeconds(1.0f);
//         }
//         Debug.Log("Walk End");
//         curState = State.IDLE;
//         StartCoroutine(StateStart());
//     }

//     IEnumerator DANCE()
//     {
//         Debug.Log("Dance");
//         int actChoice = Random.Range(0,3);

//         animator.SetBool("Dance",true);
//         animator.SetFloat("DanceChoice",actChoice);

//         yield return new WaitForSeconds(5.0f);
//         animator.SetBool("Dance",false);
//         happiness += 0.5f;
//         stamina -= 0.05f;
//         curState = State.IDLE;
//         StartCoroutine(StateStart());

//         if(!interactable)
//         {
//             interactable = true;
//             StartCoroutine(StateStart());
//         }
//     }
//     IEnumerator SLEEP()
//     {
//         agent.SetDestination(bed.position);
        
//         while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
//         {
//             bed.GetComponentInChildren<OffMeshLink>().activated = true;
//             yield return new WaitForSeconds(1.0f); // 다음 프레임까지 대기
//         }
//         interactable = false;
//         animator.SetBool("Sleep",true);

//         while(TimeManager.Instance.isNight)
//         {
//             yield return null;
//         }
//         animator.SetBool("Sleep",false);
//         agent.SetDestination(bed.GetComponent<OffMeshLink>().startTransform.position);
//         while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
//         {
//             yield return null; // 다음 프레임까지 대기
//         }
//         yield return new WaitForSeconds(0.5f);
//         bed.GetComponentInChildren<OffMeshLink>().activated = false;
//         happiness += 0.1f;
//         stamina += 0.2f;
//         fullness -= 0.1f;
//         interactable = true;
//         curState = State.IDLE;
//         StartCoroutine(StateStart());
//     }


//     IEnumerator StatusDecay()
//     {
//         while (true)
//         {
//             yield return new WaitForSeconds(1f);

//             fullness -= hungerDecayRate;
//             hungerBar.material.SetFloat("_FillAmount", fullness);
//             fullness = Mathf.Clamp(fullness, 0f, 1f);
//             if (fullness <= hungerStandard && !isHungry)
//             {
//                 isHungry = true;
//                 Debug.Log("배고픔 상태! 음식을 먹어야 합니다.");
//             }
//             else if (fullness > hungerStandard && isHungry)
//             {
//                 isHungry = false;
//                 Debug.Log("배부름 상태!");
//             }
//             if (fullness <= 0f)
//             {
//                 Debug.Log("사망, 사인: 아사");
//                 curState = State.DEAD;
//                 yield break;
//             }

//             happiness -= happinessDecayRate;
//             happinessBar.material.SetFloat("_FillAmount", happiness);
//             happiness = Mathf.Clamp(happiness, 0f, 1f);
//             if (happiness <= unhappyStandard && !unHappy)
//             {
//                 unHappy = true;
//             }
//             else if (happiness > unhappyStandard && unHappy)
//             {
//                 unHappy = false;
//             }
//             if (happiness <= 0f)
//             {
//                 Debug.Log("사망, 사인 : 고독사");
//                 curState = State.DEAD;
//                 yield break;
//             }

//             stamina -= staminaDecayRate;
//             staminaBar.material.SetFloat("_FillAmount", stamina);
//             stamina = Mathf.Clamp(stamina, 0f, 1f);
//             if (stamina <= tiredStandard && !isTired)
//             {
//                 isTired = true;
//                 Debug.Log("피로 상태! 휴식이 필요합니다.");
//             }
//             else if (stamina > tiredStandard && isTired)
//             {
//                 isTired = false;
//                 Debug.Log("회복됨!");
//             }
//             if (stamina <= 0f)
//             {
//                 Debug.Log("실신, 사인: 과로");
//                 curState = State.DEAD;
//                 yield break;
//             }
//         }
//     }

//     IEnumerator EATING()
//     {
//         GameObject player = GameObject.FindWithTag("Player");
//         if(!player.GetComponent<PlayerCtrl>().hasFood)
//         {
//             Debug.Log("음식이 없습니다");
//         }
//         else
//         {
//             if(player.GetComponent<PlayerCtrl>().isBakedFood)
//             {
//                 fullness += 0.5f;
//                 happiness += 0.1f;
//                 player.GetComponent<PlayerCtrl>().DestoryFood();
//                 player.GetComponent<PlayerCtrl>().hasFood = false;
//             }
//             else
//             {
//                 fullness += 0.3f;
//                 happiness -= 0.1f;
//                 player.GetComponent<PlayerCtrl>().DestoryFood();
//                 player.GetComponent<PlayerCtrl>().hasFood = false;
//             }
//         }
//         yield return new WaitForSeconds(1.0f);
//         curState = State.IDLE;
//         interactable = true;
//         StartCoroutine(StateStart());
        
//     }
//     IEnumerator REST()
//     {
//         yield return new WaitForSeconds(5.0f);
//         Debug.Log("REST...");
//         fullness -=0.05f;
//         happiness -= 0.1f;
//         stamina += 0.5f;

//         curState = State.IDLE;
//         interactable = true;
//         StartCoroutine(StateStart());
//     }
    
// }
