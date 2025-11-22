using System.Collections;
using UnityEngine;

public class ChangeCan : MonoBehaviour
{
    public GameObject canPrefab;       
    public float spawnOffset = 8f;     
    public float moveSpeed = 100f;
    public float pushDistance = 3f;

    GameObject currentCan;

    void Start()
    {

        currentCan = GameObject.Find("CanShadow");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCanModel();
        }
    }

    public void ChangeCanModel()
    {
        Vector3 spawnPos = transform.position + Vector3.left * spawnOffset;
        GameObject newCanObj = Instantiate(canPrefab, spawnPos, transform.rotation);

        newCanObj.transform.SetParent(transform);

        StartCoroutine(SlideAndPush(newCanObj));
    }

    IEnumerator SlideAndPush(GameObject newCan)
    {
        GameObject oldCan = currentCan;
        Vector3 centerPos = transform.position;

        while (newCan.transform.position.x < centerPos.x)
        {
            float move = moveSpeed * Time.deltaTime;

            newCan.transform.position += Vector3.right * move;

            if (oldCan != null)
                oldCan.transform.position += Vector3.right * move;

            yield return null;
        }

        newCan.transform.position = centerPos;

        if (oldCan != null)
        {
            Vector3 target = centerPos + Vector3.right * pushDistance;

            while (oldCan.transform.position.x < target.x)
            {
                oldCan.transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
                yield return null;
            }

            Destroy(oldCan); 
        }

        currentCan = newCan;
    }
}
