using System.Collections.Generic;
using UnityEngine;

public class IntroPhaseManager : MonoBehaviour
{
    [SerializeField] Animator introAnimator;
    [SerializeField] List<Animation> animationPhases = new();
    private int phase = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            PassPahse();
        }
    }

    private void PassPahse()
    {
        if (phase < animationPhases.Count && animationPhases[phase] != null)
        {
            phase++;
            animationPhases[phase].Play();
        }
    }
}
