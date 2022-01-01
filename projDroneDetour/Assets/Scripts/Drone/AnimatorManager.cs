using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class AnimatorManager
{
    public Animator Animator;

    public IEnumerator Die()
    {
        Animator.Play("explode");
        yield return new WaitForSeconds(0.4f);
    }
}
