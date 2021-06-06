using System.Collections.Generic;
using UnityEngine;

namespace Pinwheel.Griffin
{
    [CreateAssetMenu(fileName = "Splat Prototype Group", menuName = "Polaris V2/Prefab Prototype Group")]
    public class GPrefabPrototypeGroup : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> prototypes;
        public List<GameObject> Prototypes
        {
            get
            {
                if (prototypes == null)
                    prototypes = new List<GameObject>();
                return prototypes;
            }
            set
            {
                prototypes = value;
            }
        }

#if UNITY_EDITOR
        public List<string> editor_PrefabAssetPaths;
        public List<string> Editor_PrefabAssetPaths
        {
            get
            {
                if (editor_PrefabAssetPaths == null)
                {
                    editor_PrefabAssetPaths = new List<string>();
                }
                GUtilities.EnsureLengthSufficient(editor_PrefabAssetPaths, prototypes.Count);
                return editor_PrefabAssetPaths;
            }
            set
            {
                editor_PrefabAssetPaths = value;
            }
        }
#endif
    }
}
