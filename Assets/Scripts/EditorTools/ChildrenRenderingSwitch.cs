using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [ExecuteInEditMode]
    public class ChildrenRenderingSwitch : MonoBehaviour
    {
        // Start is called before the first frame update
        //[SerializeField]
        public bool childrenRendering;
        
        public bool ChildrenRendering
        {
            get { return childrenRendering; }
            set
            {
                //Debug.Log("Value set");
                childrenRendering = value;
                var allChildren = GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    var render = child.gameObject.GetComponent<Renderer>();
                    if (render != null)
                    {
                        render.enabled = childrenRendering;
                    }
                }
            }
        }

        public void SwitchChildrenRendering()
        {
            var allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                var render = child.gameObject.GetComponent<Renderer>();
                if (render != null)
                {
                    render.enabled = !render.enabled;
                }
            }
        }

    }

    
    
