using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnEnter : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         PlayerPrefs.SetString ("lastLoadedScene", SceneManager.GetActiveScene ().name);
         SceneManager.LoadScene ("Main Game");
    }
}
