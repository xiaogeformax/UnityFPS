using UnityEngine;
using System.Collections;

public class Animate : MonoBehaviour
{
	Vector2 scrollPosition = Vector2.zero;
	
	void OnGUI()
	{
        int distance = 0;
		
		scrollPosition = GUI.BeginScrollView (new Rect(10,20,300,400),scrollPosition, new Rect (0, 0,250, 1200));

        foreach (AnimationState anim in GetComponent<Animation>())
        {
			if(GUI.Button (new Rect (0,distance,200,20), Formatname(anim.name)))
			{
				GetComponent<Animation>().Play(anim.name);
			}
			
			distance += 37;
        }
   
	    GUI.EndScrollView ();
	}
	
	string Formatname(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;

        s = s.Replace("_", " ");

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        
        return new string(a);
    }
}
