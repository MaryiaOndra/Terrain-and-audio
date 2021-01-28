using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TerrainLightAudio
{
    public class LevelMenager : MonoBehaviour
    {
        DroppedObjectsDetector[] levelsWithObjects;
        bool IsAllLevelsDone;
        int countLevels;

        void Start()
        {
            levelsWithObjects = GetComponentsInChildren<DroppedObjectsDetector>();
        }

        void Update()
        {
            if (!IsAllLevelsDone)
            {
                foreach (var level in levelsWithObjects)
                {
                    if (!level.IsAllObjectsDropped)
                    {
                        countLevels = 0;
                    }
                    else
                    {
                        countLevels++;

                        if (countLevels == levelsWithObjects.Length - 1)
                        {
                            IsAllLevelsDone = true;
                        }
                    }

                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }
}
