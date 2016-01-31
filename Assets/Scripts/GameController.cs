using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameController : MonoBehaviour {
	enum ELEMENT:int {
		ONE = 0,
		TWO,
		THREE,
	}

	public struct Element {
		public Element(int ntype, Vector2 npos) {
			type = ntype;
			pos = npos;
		}

		public int type;
		public Vector2 pos;
	}

	public struct Group {
		public Group(List<Element> nelems, float nalignment, float ndistance) {
			elems = nelems;
			alignment = nalignment;
			distance = ndistance;
		}

		public List<Element> elems;
		public float alignment;
		public float distance;
	}

	public abstract class Hotspot {
		public abstract bool IsInside(Vector2 npos);
		public abstract Vector2 getCenter();
		public abstract float getRadius();
	}

	public class RectHotspot : Hotspot {
		public RectHotspot(Vector2 nbottomLeft, Vector2 ntopRight) {
			bottomLeft = nbottomLeft;
			topRight = ntopRight;
		}

		public Vector2 bottomLeft;
		public Vector2 topRight;

		public override bool IsInside(Vector2 pos)
		{
			if (pos.x < bottomLeft.x || pos.x > topRight.x || pos.y < bottomLeft.y || pos.y > topRight.y)
				return false;
			else
				return true;
		}

		public override Vector2 getCenter() {
			return topRight - (topRight - bottomLeft) / 2;
		}

		public override float getRadius() {
			return Vector2.Distance((topRight - bottomLeft) / 2, new Vector2(0, 0));
		}
	}

	public class CircleHotspot : Hotspot {
		public CircleHotspot(Vector2 npos, float nraduis) {
			pos = npos;
			radius = nraduis;
		}

		public Vector2 pos;
		public float radius;

		public override bool IsInside(Vector2 npos) {
			return Vector2.Distance(npos, pos) < radius;
		}

		public override Vector2 getCenter() {
			return pos;
		}

		public override float getRadius()
		{
			return radius;
		}
	}

	public static List<Element> getHotspotElements(Hotspot hotspot, List<Element> elems)
	{
		List<Element> hotspotElems = new List<Element>();

		for (int i = 0; i < elems.Count; i++) {
			if (hotspot.IsInside(elems[i].pos)) {
				hotspotElems.Add(elems[i]);
			}
		}
		return hotspotElems;
	}

	public static Vector2 getCenterOfElements(List<Element> elems) {
		Vector2 center = new Vector2(0, 0);
		for (int i = 0; i < elems.Count; i++)
			center += elems[i].pos;
		center /= elems.Count;
		return center;
	}

	public static Group getGroupAtHotspot(Hotspot hotspot, RuleFn[] rules, List<Element> elems)
	{
		Group group = new Group(new List<Element>(), 1, 1);

		for (int i = 0; i < rules.Length; i++) {
			float alignment = rules[i](elems);

			if (alignment < group.alignment) {
				float distance = Vector2.Distance(hotspot.getCenter(), getCenterOfElements(elems)) / hotspot.getRadius();
				group = new Group(elems, alignment, distance);
			}
		}

		return group;
	}

	public static Group[] getGroups(Hotspot[] hotspots, RuleFn[] rules, List<Element> elems)
	{
		// fill an array with objs close to hotspots
		List<Element>[] hotspotElems = new List<Element>[hotspots.Length];
		for (int i = 0; i < hotspotElems.Length; i++) {
			hotspotElems[i] = new List<Element>();
		}

		for (int i = 0; i < hotspotElems.Length; i++) {
			hotspotElems[i] = getHotspotElements(hotspots[i], elems);
		}

		// groups to return
		Group[] groups = new Group[hotspotElems.Length];

		// for every hotspot
		for (int i = 0; i < hotspotElems.Length; i++) {
			// create a group
			groups[i] = getGroupAtHotspot(hotspots[i], rules, hotspotElems[i]);

		}

		return groups;
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


	static float CalcDiff(List<float> l1, List<float> l2, float max) {
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

	static float MeasurePolygon(List<Element> elems, List<float> angs) {
		Debug.Log ("HERE! MeasurePolygon");
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

	public delegate float RuleFn(List<Element> elems);

	public static float somerule(List<Element> elems) {
		if (elems.Count == 0)
			return 1;
		return UnityEngine.Random.Range(0, 1000) / 1000.0f;
	}

	public static float squareRule(List<Element> elems) {
		if (elems.Count != 4)
			return 1;
		return MeasurePolygon(elems,new List<float>() {(float)Math.PI/2,(float)Math.PI/2,(float)Math.PI/2,(float)Math.PI/2});
	}

	public static float perfectTriangleRule(List<Element> elems) {
		if (elems.Count != 3)
			return 1;
		return MeasurePolygon(elems,new List<float>() {(float)Math.PI/3,(float)Math.PI/3,(float)Math.PI/3});
	}

	public static float threeInLineRule(List<Element> elems) {
		if (elems.Count != 3)
			return 1;
		return MeasurePolygon(elems,new List<float>() {(float)0,(float)0,(float)Math.PI});
	}

	private List<GameObject> elemGameObjects;
	private Hotspot[] hotspots;
	private RuleFn[] rules;

	// Use this for initialization
	void Start () {
		{
			// collect elements
			GameObject parent = GameObject.Find("ElementContainer");
			elemGameObjects = new List<GameObject>();
			foreach (Transform child in parent.transform) {
				elemGameObjects.Add(child.gameObject);
			}
		}

		{
			// collect hotspots
			GameObject parent = GameObject.Find("HotspotContainer");
			List<GameObject> hotspotGameObjects = new List<GameObject>();
			foreach (Transform child in parent.transform) {
				hotspotGameObjects.Add(child.gameObject);
			}

			hotspots = new Hotspot[hotspotGameObjects.Count];
			for (int i = 0; i < hotspotGameObjects.Count; i++) {
				hotspots[i] = new CircleHotspot(hotspotGameObjects[i].transform.position, 5);
			}
		}

		rules = new RuleFn[] { somerule,perfectTriangleRule,threeInLineRule,squareRule };
	}

	// Update is called once per frame
	void Update () {
		List<Element> elems = new List<Element>(elemGameObjects.Count);
		for (int i = 0; i < elemGameObjects.Count; i++) {
			elems.Add(new Element(0, elemGameObjects[i].transform.position));
		}

		//Debug.Log(getGroups(hotspots, rules, elems)[0].distance);
	}
}
