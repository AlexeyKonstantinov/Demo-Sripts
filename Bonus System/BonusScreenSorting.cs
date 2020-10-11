using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BonusSystem
{
    public class BonusScreenSorting : MonoBehaviour
    {
        public GameObject content;

        private Transform _transform;
        private void Awake()
        {
            _transform = transform;
        }

        [ContextMenu("Sort")]
        public void Sort()
        {
            List<Transform> children = GetTopLevelChildren(_transform).ToList();

            foreach (var child in children)
            {
                print(child.name);
            }

            TransformComparer transformComparer = new TransformComparer();
            children.Sort(transformComparer);

            for (int i = 0; i < children.Count; i++)
            {
                children[i].SetAsLastSibling();
            }

        }

        public static Transform[] GetTopLevelChildren(Transform parent)
        {
            Transform[] children = new Transform[parent.childCount];
            for (int i = 0; i < parent.childCount; i++)
            {
                children[i] = parent.GetChild(i);
            }
            return children;
        }
    }

    public class TransformComparer : Comparer<Transform>
    {
        public override int Compare(Transform x, Transform y)
        {
            if (x.GetComponent<BonusView>().bonusData.index > y.GetComponent<BonusView>().bonusData.index)
                return 1;
            else if (x.GetComponent<BonusView>().bonusData.index < y.GetComponent<BonusView>().bonusData.index)
                return -1;
            else
                return 0;
        }
    }
}

