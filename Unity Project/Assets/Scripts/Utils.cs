using System.Collections;
using UnityEngine;
 
public static class Utils
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}

    public static IEnumerator FadeOut(AudioSource a)
    {
        a.volume = 1;

        while (a.volume >= 0)
        {
            a.volume -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        a.mute = true;
    }

    public static IEnumerator FadeIn(AudioSource a)
    {
        a.mute = false;
        a.volume = 0;

        while (a.volume <= 1.0f)
        {
            a.volume += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

}