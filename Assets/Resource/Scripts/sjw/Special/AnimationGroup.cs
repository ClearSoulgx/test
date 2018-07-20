using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using EasyUIAnimator;
using UnityEngine.UI;
public class AnimationGroup : MonoBehaviour {

    // Use this for initialization
    private RectTransform[] animationsInGroup;
    private UIAnimation[] animations;
    private UIGroupAnimation WindGroupAnimation;
    private float wind=0,offset=0;
    void Start () {

        // AnimationsInGroup= transform.c
        InitAnimationGroup();
        StartCoroutine(WindEmulator());
	}
	
	

    private void   InitAnimationGroup()
    {
        
        animations = new UIAnimation[transform.childCount];
        int i = 0;
        wind = UnityEngine.Random.Range(-0.01f,0.01f);
        foreach (RectTransform child in transform)
        {
            animations[i] = UIAnimator._Move(child, new Vector2(0f, 0f), new Vector2(0f, 0f), 3f,true);
           //offset = wind;
            i++;
        }
        WindGroupAnimation = new UIGroupAnimation(animations);
    }

    IEnumerator WindEmulator()
    {
        yield return new WaitForSeconds(3f);
        if (UIAnimator.Animations.Count != 0)
        {
  
        }
        else
        {
            // explosionGroupEffect = new UIGroupAnimation(groupEffDemo, UIAnimator.Move(groupEffDemo[0], new Vector2(0.3f, 0.6f), new Vector2(0.7f, 0.6f), 2f));
            //new UIGroupAnimation(AnimationsInGroup, 
            //    UIAnimator.MoveTo(AnimationsInGroup[0],  new Vector2(0f, 0.5f), 2f))
            //    .SetGroupEffect(Effect.ExplosionGroup(), min: 0.1f, maxAngle: 360).SetGroupModifier(Modifier.CircularOut).SetGroupEffect(Effect.ExplosionGroup(), min: 0.1f, maxAngle: 360)
            //     .SetGroupModifier(Modifier.CircularOut).Play();
            
              WindGroupAnimation.SetGroupEffect(Effect.WaveGroup(), max: 0.02f,min: 0.01f, maxAngle: 360)
                  .SetGroupModifier(Modifier.CircularOut).Play();

            yield return new WaitForSeconds(7f);

        }
        InitAnimationGroup();
        StartCoroutine(WindEmulator());
    }
}
