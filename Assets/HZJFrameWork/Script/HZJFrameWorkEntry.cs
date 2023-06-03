//=====================================================
// - FileName:      HZJFrameWorkEntry.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/30 16:36:08
// - Description:   ¿ò¼ÜÈë¿Ú
//======================================================
using UnityEngine;

namespace HZJFrameWork
{
    public class HZJFrameWorkEntry : MonoBehaviour
    {
        public GameObject UIRoot;

        private void Awake()
        {
            InitModule();
        }

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            if (UIRoot)
            {
                DontDestroyOnLoad(UIRoot);
            }
            ProcedureBase logoAni = new LogoAniProcedure();
            logoAni.OnInit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitModule()
        {
            ResourcesModule resourcesModule = new ResourcesModule();
            resourcesModule.LoadPrefabs("Cube");
        }
    }
}

