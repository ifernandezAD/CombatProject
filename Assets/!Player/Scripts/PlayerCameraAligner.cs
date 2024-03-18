using Cinemachine;
using System.Linq;
using UnityEngine;

public class PlayerCameraAligner : MonoBehaviour
{
   [SerializeField] CinemachineVirtualCameraBase[] cameras;
   CinemachineBrain brain;
   
   private void Awake()
   {
       brain = Camera.main.GetComponent<CinemachineBrain>();
   }
   
   private void Update()
   {
       if (cameras.Contains(brain.ActiveVirtualCamera))
       {
           foreach (ICinemachineCamera camera in cameras)
           {
               if (camera != brain.ActiveVirtualCamera)
               {
                   if (camera is CinemachineFreeLook)
                   {
                       CinemachineFreeLook freeLook = camera as CinemachineFreeLook;
                       //freeLook.m_YAxis.Value = 
                   }
               }
           }
       }
   }
}
