using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public delegate float RuleFn(List<Element> elems);

    public struct Group {
        public Group(List<Element> nelems, float nalignment) {
            elems = nelems;
            alignment = nalignment;
        }

        public List<Element> elems;
        public float alignment;
    }

    public abstract class Hotspot {
        public abstract bool IsInside(Vector2 npos);
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

    public static Group getGroupAtHotspot(RuleFn[] rules, List<Element> elems)
    {
        Group group = new Group(new List<Element>(), 1);

		for (int i = 0; i < rules.Length; i++) {
            float alignment = rules[i](elems);

            if (alignment < group.alignment)
                group = new Group(elems, alignment);

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
            groups[i] = getGroupAtHotspot(rules, hotspotElems[i]);

        }

        return groups;
    }

    public static float somerule(List<Element> elems) {
        if (elems.Count == 0)
            return 1;
        return Random.Range(0, 1000) / 1000.0f;
    }

    // Use this for initialization
    void Start () {
        RuleFn[] rules = new RuleFn[] { somerule };

        Hotspot[] hotspots = new Hotspot[] { new CircleHotspot(new Vector2(0, 0), 5) };

        List<Element> elems = new List<Element>();
        elems.Add(new Element((int)ELEMENT.ONE, new Vector2(10, 0)));

        Debug.Log(getGroups(hotspots, rules, elems)[0].alignment);
	}

	// Update is called once per frame
	void Update () {

	}
}
