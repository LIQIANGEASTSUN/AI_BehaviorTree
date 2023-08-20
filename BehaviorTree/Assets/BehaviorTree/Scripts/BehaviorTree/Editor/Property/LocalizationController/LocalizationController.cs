using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class LocalizationController
    {
        private LocalizationView localizationView;

        public LocalizationController()
        {
            Init();
        }

        public void Init()
        {
            localizationView = new LocalizationView();
        }

        public void OnDestroy()
        {

        }

        public void OnGUI()
        {
            localizationView.Draw();
        }

    }

}
