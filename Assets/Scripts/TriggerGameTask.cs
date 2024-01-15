using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerGameTask : HoangBehavior
{
    public Text convers;
    public GameObject player;
    public GameObject task;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Fighter") && other.gameObject.name == "Player")
            StartCoroutine(ScenceBeginPlayer());
    }

    IEnumerator ScenceBeginPlayer()
    {
        
        task.GetComponent<BoxCollider2D>().enabled = false;

        convers.GetComponent<Text>().text = "Xin chaò chàng trai dũng cảm!!!";
        yield return new WaitForSeconds(2.5f);
        convers.GetComponent<Text>().text = "Nhiệm vụ của cậu là tiêu diệt quái vật và cứu nguy cho công chúa!!";
        yield return new WaitForSeconds(2.8f);
        convers.GetComponent<Text>().text = "Nhìn cái rương kia.Cậu có thể thu thập tiền để mua vũ khí, tăng sát thương";
        yield return new WaitForSeconds(3.9f);
        convers.GetComponent<Text>().text = "Còn cái hồ bên trái để hồi lại máu đã mất";
        yield return new WaitForSeconds(3.5f);

        convers.GetComponent<Text>().text = "Ấn vào rương (chest) để xem vũ khí và thông tin";
        yield return new WaitForSeconds(3.5f);

        convers.GetComponent<Text>().text = "Nhấn ESC để mở cài đặt (Option)!!";
        yield return new WaitForSeconds(3.5f);

        convers.GetComponent<Text>().text = "Chúc cậu may mắn!!";
        yield return new WaitForSeconds(3.0f);
        convers.GetComponent<Text>().text = "";

       
    }
}
