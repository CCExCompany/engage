using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick2 : MonoBehaviour {
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	int previousLevel = PlayerPrefs.GetInt( "previousLevel" );
	Application.LoadLevel( previousLevel );
    }
}
