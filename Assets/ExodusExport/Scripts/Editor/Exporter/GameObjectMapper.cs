using UnityEngine;
using System.Collections.Generic;

namespace SceneExport{
	[System.Serializable]
	public class GameObjectMapper: ObjectMapper<GameObject>{
		void gatherObjectIds(Queue<GameObject> objects){
			while(objects.Count > 0){
				var curObject = objects.Dequeue();
				if (!curObject)
					continue;
				/*var curId = */
				getId(curObject);//this creates id for an object

                var lodGroup = curObject.GetComponent<LODGroup>();
                if (lodGroup == null)
                {
                    foreach (Transform curChild in curObject.transform)
                    {
                        if (!curChild)
                            continue;
                        if (!curChild.gameObject)
                            continue;


                        objects.Enqueue(curChild.gameObject);
                    }
                }
                else
                {
                    var lods = lodGroup.GetLODs();
                    
                    foreach(var lod in lods)
                    {
                        var renderers = lod.renderers;
                        foreach(var renderer in renderers)
                        {
                            if(renderer!= null)
                                objects.Enqueue(renderer.gameObject);
                        }
                        break;
                    }
                }
			}
		}
		
		public void gatherObjectIds(GameObject obj){
			var objQueue = new Queue<GameObject>();
			objQueue.Enqueue(obj);
			gatherObjectIds(objQueue);
		}
		
		public void gatherObjectIds(GameObject[] objs){
			var objQueue = new Queue<GameObject>();
			foreach(var cur in objs){
				objQueue.Enqueue(cur);	
			}
			gatherObjectIds(objQueue);
		}
	}	
}
