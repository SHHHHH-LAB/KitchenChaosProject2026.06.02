using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger Instance {  get; private set; }

   [SerializeField] private AudioClipRefasSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OrderMananger.Instance.OnRecipSuccessed += OrderMager_OnRecipSuccessed;
        OrderMananger.Instance.OnRecipFailed += OrderMager_OnRecipFailed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnObjiecttrashed += TrashCounter_OnObjiecttrashed;
    }

    private void TrashCounter_OnObjiecttrashed(object sender, System.EventArgs e)
    {
        PlayerSond(audioClipRefsSO.trash);
    }

    private void KitchenObjectHolder_OnPickup(object sender, System.EventArgs e)
    {
        PlayerSond(audioClipRefsSO.objictPickup);
    }

    private void KitchenObjectHolder_OnDrop(object sender, System.EventArgs e)
    {
        PlayerSond(audioClipRefsSO.objictDrop);
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {

        PlayerSond(audioClipRefsSO.chhop);
    }

    private void OrderMager_OnRecipSuccessed(object sender, System.EventArgs e)
    {
        PlayerSond(audioClipRefsSO.deliverSuccess);

    }
         private void OrderMager_OnRecipFailed(object sender, System.EventArgs e)
    {
        PlayerSond(audioClipRefsSO.deliverFsil);
    }

    public void PlayStopSound(float volum=1)
    {
        PlayerSond(audioClipRefsSO.footstep, volum);
    }
    private void PlayerSond(AudioClip[] clips,float volume=1.0f)
    {
        PlaySound(clips, Camera.main.transform.position);
    }
    private void PlaySound(AudioClip[] clips,Vector3 position,float volume = 1.0f)
    {
        int index=Random.Range(0,clips.Length);
        AudioSource.PlayClipAtPoint(clips[index],position,volume);
    }
}

