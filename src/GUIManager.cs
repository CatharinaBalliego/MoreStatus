using UnityEngine;

namespace MoreStatus
{
    public class GUIManager : MonoBehaviour 
    {
        public GameObject separatorGO;
        private Vector2 startPos;
        private Vector2 currentOffset = new(0, 0);

        internal void Awake()
        {
            ItemDetailMoreStatus.MoreStatus.Log.LogMessage("start guimanager");
            separatorGO = this.transform.Find("Canvas").gameObject;

            var canvas = separatorGO.GetComponent<Canvas>();
            canvas.sortingOrder = 1;


            var statusRect = separatorGO.GetComponent<RectTransform>();
            var separator = separatorGO.transform.Find("separator").gameObject;
            separator.transform.position = new(1754.5f, 813.5f, 0);

            ItemDetailMoreStatus.MoreStatus.Log.LogMessage("end guimanager");
        }
    }
}
