using UnityEngine;

public class Pojazd_skrypt : MonoBehaviour
{
    public WheelCollider[] kolo;
    public Transform[] kogo_gfx;
    public WheelCollider[] kolo_skretne;
    public float maksymalny_skret_kol = 30;
    public WheelCollider[] kolo_napedowe;
    public float sila_przyspieszenia1 = 500;
    public WheelCollider[] kola_hamujace;
    public float sila_hamowania;
    public GameObject swiatlo_s_nd;
    public GameObject swiatlo_s_d;

    void Update()
    {
        float pionowy_input = Input.GetAxis("Vertical");
        float brakeTorqueValue = 0f;
        bool aktywne_hamowanie = Input.GetKey(KeyCode.Space);


        for (int i = 0; i < kolo.Length; ++i)
        {
            Vector3 v;
            Quaternion q;
            kolo[i].GetWorldPose(out v, out q);
            kogo_gfx[i].position = v;
            kogo_gfx[i].rotation = q;
        }
        //skręt:
        for (int i = 0; i < kolo_skretne.Length; ++i)
            kolo_skretne[i].steerAngle = maksymalny_skret_kol * Input.GetAxis("Horizontal");

        //napęd:
        for (int i = 0; i < kolo_napedowe.Length; ++i)
            kolo_napedowe[i].motorTorque = sila_przyspieszenia1 * pionowy_input;
        
        if (aktywne_hamowanie)
        {
            for (int i = 0; i < kolo_napedowe.Length; ++i)
                kolo_napedowe[i].motorTorque = 0f; 

            brakeTorqueValue = sila_hamowania * -1;
        }

        //hamowanie:
        for (int i = 0; i < kola_hamujace.Length; ++i)
            kola_hamujace[i].brakeTorque = brakeTorqueValue; 
        
        //światło stopu:
        if (swiatlo_s_nd != null && swiatlo_s_d != null)
        {
            if (aktywne_hamowanie || pionowy_input < 0) 
            {
                swiatlo_s_nd.SetActive(false);
                swiatlo_s_d.SetActive(true);
            }
            else
            {
                swiatlo_s_nd.SetActive(true);
                swiatlo_s_d.SetActive(false);
            }
        }
    }
}