using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Measure : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Hello");
	}
	
	public struct Element {
		public int type;
		public Vector2 pos;
		
	}

	class ClassicPermutationProblem
	{
		ClassicPermutationProblem() { }

		private static void PopulatePosition<T>(List<List<T>> finalList, List<T> list, List<T> temp, int position)
		{
			foreach (T element in list)
			{
				List<T> currentTemp = temp.ToList();
				if (!currentTemp.Contains(element))
					currentTemp.Add(element);
				else
					continue;

				if (position == list.Count)
					finalList.Add(currentTemp);
				else
					PopulatePosition(finalList, list, currentTemp, position + 1);
			}
		}

		public static List<List<int>> GetPermutations(List<int> list)
		{
			List<List<int>> results = new List<List<int>>();
			PopulatePosition(results, list, new List<int>(), 1);
			return results;
		}
	}


	float CalcDiff(List<float> l1, List<float> l2, float max) {
		if (l1.Count != l2.Count)
			return 1;
		float diff = 0;
		for (var i = 0; i < l1.Count; i++) {
			max = Math.Max(max, l1[i]);
			max = Math.Max(max, l2[i]);
			diff += Math.Abs(l1[i] - l2[i]);
		}
		return (diff / l1.Count) / max;
	}

	float MeasurePolygon(List<Element> elems, List<float> angs) {
	    float min=100000;
		if (elems.Count != angs.Count) {
   	 	    return 1;
   		 }

    var nodes = new List<int>();
    for(var i = 0; i < elems.Count; i++)
	{
        nodes.Add(i);
    }
	var polys = ClassicPermutationProblem.GetPermutations (nodes);

    for (var i = 0; i < polys.Count; i++) {
        var poly = polys[i];
        var angles = new List<float>();
        for (var j = 0; j < poly.Count; j++) {
            var ea = elems[poly[(1+j)%poly.Count]].pos;
            var eb = elems[poly[(0+j)%poly.Count]].pos;
            var ec = elems[poly[(2+j)%poly.Count]].pos;

            var Ab = Math.Abs(eb.x - ec.x);
            var Ac = Math.Abs(eb.y - ec.y);

            var a = Math.Sqrt((Ab * Ab) + (Ac * Ac));

            var Bb = Math.Abs(ea.x - ec.x);
            var Bc = Math.Abs(ea.y - ec.y);

            var b = Math.Sqrt((Bb * Bb) + (Bc * Bc));

            var Cb = Math.Abs(ea.x - eb.x);
            var Cc = Math.Abs(ea.y - eb.y);

            var c = Math.Sqrt((Cb * Cb) + (Cc * Cc));
				angles.Add((float)Math.Acos((b * b + c * c - a * a) / (2 * b * c)));
        }
			min = Math.Min(min,CalcDiff(angles.OrderBy(item => item).ToList(), angs.OrderBy(item => item).ToList(), (float) (2 * Math.PI)));
    }
    return min;
}
}
