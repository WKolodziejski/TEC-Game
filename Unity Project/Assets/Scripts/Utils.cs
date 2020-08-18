using System.Collections;
using UnityEngine;
 
public static class Utils
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}

    public static IEnumerator FadeOutAudio(AudioSource a)
    {
        a.volume = 1;

        while (a.volume >= 0)
        {
            a.volume -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        a.mute = true;
    }

    public static IEnumerator FadeInAudio(AudioSource a)
    {
        a.mute = false;
        a.volume = 0;

        while (a.volume <= 1.0f)
        {
            a.volume += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public static IEnumerator FadeOutLowPass(AudioLowPassFilter f)
    {
        //f.cutoffFrequency = 1000f;

        while (f.cutoffFrequency < 22000f)
        {
            f.cutoffFrequency += 350f;

            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    public static IEnumerator FadeInLowPass(AudioLowPassFilter f)
    {
        //f.cutoffFrequency = 22000f;

        while (f.cutoffFrequency >= 1000f)
        {
            f.cutoffFrequency -= 350f;

            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

}