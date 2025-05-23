using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunk_generation : MonoBehaviour
{
    public GameObject chunk_prefab;
    public int rendered_chunks = 0, generated_chunks = 0, chunk_size = 10;
    public int render_distance = 0;

    private float distance = 0, max_height=0, min_height=0;
    private Transform player_position;
    private int size = 50;
    void Start()
    {
        size = render_distance/chunk_size;
        chunk_prefab.GetComponent<generatemesh>().chunk_Generation = GetComponent<chunk_generation>();
        player_position = GameObject.Find("Player").transform;
        distance = (size*chunk_size)/2;

        transform.position = new Vector3(-distance, transform.position.y ,-distance);
        loadchunks();
        render_distance /= 2;
    }
    private void loadchunks()
    {
        Vector2 temp;
        GameObject temp_g;
        for (int x = 0; x < size; x++) 
        {
            for (int z = 0; z < size; z++)
            {
                temp_g = Instantiate(chunk_prefab, transform.position + new Vector3(x*chunk_size, 0 , z*chunk_size), Quaternion.identity);
                temp = temp_g.GetComponent<generatemesh>().get_min_max();
                if (temp.x < min_height || min_height == 0)
                {
                    min_height = temp.x;
                }
                if (temp.x > max_height || max_height == 0)
                {
                    max_height = temp.x;
                }
            }
        }
        StartCoroutine(update_min_max());
    }
    
    public void spawn_new_chunk(Vector3 position)
    {
        Instantiate(chunk_prefab, position, Quaternion.identity);
    }
    public float get_min()
    {
        return min_height;
    }
    public float get_max()
    {
        return max_height;
    }
    IEnumerator update_min_max()
    {
        Vector2 temp;
        yield return new WaitForEndOfFrame();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("chunk"))
        {
            temp = g.GetComponent<generatemesh>().get_min_max();

            if (temp.x < min_height || min_height == 0)
            {
                min_height = temp.x;
            }
            if (temp.y > max_height || max_height == 0)
            {
                max_height = temp.y;
            }
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("chunk"))
        {
            g.GetComponent<generatemesh>().recolor();
        }

    }
}
