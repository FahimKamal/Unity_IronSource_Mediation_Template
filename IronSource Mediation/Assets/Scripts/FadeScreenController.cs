using System.Collections;
using UnityEngine;

public class FadeScreenController : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    
    public void FadeIn()
    {
        _animation.Play("FadeIn");
    }
    
    public void FadeOut()
    {
        _animation.Play("FadeOut");
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}
