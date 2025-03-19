using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.XR;

public class VR_PC : MonoBehaviour
{
    void Start()
    {
        CheckVRDevice();
    }

    void CheckVRDevice()
    {
        if (XRGeneralSettings.Instance.Manager.activeLoader != null) // Si hay un visor VR
        {
            EnableVR();
        }
        else
        {
            DisableVR();
        }
    }

    void EnableVR()
    {
        StartCoroutine(StartXR());
    }

    void DisableVR()
    {
        StartCoroutine(StopXR());
    }

    System.Collections.IEnumerator StartXR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.activeLoader.Start();
            Debug.Log("Modo VR activado");
        }
        yield return null;
    }

    System.Collections.IEnumerator StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.activeLoader.Stop();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("Modo estándar activado (teclado y mouse)");
        }
        yield return null;
    }
}

