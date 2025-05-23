using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using System.Linq;
using System;
using Unity.Mathematics;



public class generatemesh : MonoBehaviour
{
    public bool is_water;
    public float distance, height;
    public Texture2D noise_map;
    public chunk_generation chunk_Generation;

    private Vector3[] vertices;
    private Vector2[] uvs;
    private Color[] colors;
    public Gradient gradient;
    private int[] triangles;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshCollider terrain_collider;
    private Transform player_position;

    private Vector3 last_player_location;
    private int[] offset = new int[2];
    private map_noise_generation noise_generation;
    private MeshCollider meshcollider;
    void Awake()
    {
        Vector3 offsetvector = chunk_Generation.transform.position + transform.position;
        noise_generation = GameObject.Find("map_noise").GetComponent<map_noise_generation>();
        offset[0] = (int)offsetvector.x; offset[1] = (int)offsetvector.z;
        terrain_collider = GetComponent<MeshCollider>();
        mesh = new Mesh();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        meshcollider = GetComponent<MeshCollider>();
        
        player_position = GameObject.Find("Player").transform;
        last_player_location = player_position.position;
        float x_distance = player_position.position.x - transform.position.x;
        float z_distance = player_position.position.z - transform.position.z;

        if (Mathf.Abs(x_distance) > chunk_Generation.render_distance)
            Destroy(gameObject);
        else if (Mathf.Abs(z_distance) > chunk_Generation.render_distance)
            Destroy(gameObject);

        if(xzmagnitude(player_position.position - transform.position) < chunk_Generation.render_distance)
            generate_terrain();

    }
    float max = 0, min = 0;
    private void create_shape()
    {      
        int lod = chunk_Generation.chunk_size;

        vertices = new Vector3[(lod + 1) * (lod + 1)];
        colors = new Color[vertices.Length];
        uvs = new Vector2[vertices.Length];

        for (int i = 0, x = 0; x <= lod + 0; x++)
        {
            for (int z = 0; z <= lod + 0; z++)
            {
                float y;
                if (is_water)
                {
                    y = (noise_map.GetPixel((int)(x * distance) + offset[0], (int)(z * distance) + offset[1]).grayscale > 0) ? 0 : height;
                    colors[i] = gradient.Evaluate((y < height) ? 0f : 1f);
                }
                else
                {
                    y = noise_generation.get_noise_point((int)(x * distance) + offset[0], (int)(z * distance) + offset[1]) * -height;
                    colors[i] = gradient.Evaluate(Mathf.InverseLerp(chunk_Generation.get_max(), chunk_Generation.get_min(), y));
                }
                vertices[i] = new Vector3(x * distance, y, z * distance);
                
                uvs[i] = vertices[i];
                if( max < y || max == 0)
                {
                    max = y;
                }
                if( min > y || min == 0)
                {
                    min = y;
                }

                i++;
            }
        }

        for (int i = 0; i < colors.Length; i++)
        {
            if(colors[i].a == 0 && i == colors.Length -1)
                Destroy(gameObject);
            else if(colors[i].a > 0)
            {
                break;
            }
        }
        triangles = new int[lod * lod * 6];
        int vert = 0;
        int tris = 0;

        for (int j = 0; j < lod; j++)
        {
            for (int i = 0; i < lod; i++)
            {
                triangles[tris + 0] = vert+0;
                triangles[tris + 1] = vert + lod + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + lod + 1;
                triangles[tris + 5] = vert + lod + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }
    private void update_mesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        terrain_collider.sharedMesh = mesh;
    }
    public void recolor()
    {
        for (int i = 0, x = 0; x <= chunk_Generation.chunk_size; x++)
        {
            for (int z = 0; z <= chunk_Generation.chunk_size; z++)
            {
                try
                {

                    colors[i] = gradient.Evaluate(Mathf.InverseLerp(chunk_Generation.get_max(), chunk_Generation.get_min(), vertices[i].y));
                }
                catch 
                {
                    
                }
                i++;
            }
        }
        update_mesh();
    }

    public void generate_terrain()
    {
        create_shape();
        update_mesh();
    }
    private void Update()
    {
        Vector3 distance_vector = player_position.position - transform.position;
        float xzdistance = xzmagnitude(player_position.position - transform.position);

        if (xzdistance > chunk_Generation.render_distance)
        {
            if (mesh.vertexCount != 0)
            {
                mesh.Clear();

                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                meshcollider.enabled = false;
            }
            if (Mathf.Abs(distance_vector.x) > chunk_Generation.render_distance)
                StartCoroutine(generate_new_chunk(1));
            else if (Mathf.Abs(distance_vector.z) > chunk_Generation.render_distance)
                StartCoroutine(generate_new_chunk(-1));
        }
        else if(xzdistance <= chunk_Generation.render_distance)
        {
            if (rendering_chunk)
            {

            }
            else if (mesh.vertexCount == 0)
                generate_terrain();
            else if (xzdistance > 100)
            {
                meshcollider.enabled = false;
            }
            else
            {
                meshcollider.enabled = true;
            }
        }
    }
    private void destroy_chunk(int dir)
    {
        
        Vector3 new_chunk_position = Vector3.zero;
        if(dir == 1)
        {
            new_chunk_position = transform.position + new Vector3(chunk_Generation.render_distance * 2- chunk_Generation.chunk_size, 0,0) * Mathf.Sign(player_position.position.x - transform.position.x);
        }
        else if (dir == -1)
        {
            new_chunk_position = transform.position + new Vector3(0, 0, chunk_Generation.render_distance * 2-chunk_Generation.chunk_size) * Mathf.Sign(player_position.position.z - transform.position.z);
        }
        if(!is_water)
            chunk_Generation.spawn_new_chunk(new_chunk_position);
        else
            chunk_Generation.spawn_new_chunk(new_chunk_position);
        Destroy(gameObject);
    }
    private float xzmagnitude(Vector3 vector)
    {
        return Mathf.Sqrt((vector.x * vector.x) + (vector.z * vector.z));
    }
    private bool rendering_chunk = false;
    IEnumerator render_chunk()
    {
        rendering_chunk = true;
        while (chunk_Generation.rendered_chunks > 0)
        {
            chunk_Generation.rendered_chunks--;
            yield return new WaitForEndOfFrame();
        }
        generate_terrain();
        chunk_Generation.rendered_chunks++;
        rendering_chunk = false;
    }
    private bool generating_chunk  = false;
    IEnumerator generate_new_chunk(int dir)
    {
        generating_chunk = true;
        while (chunk_Generation.generated_chunks > 0)
        {
            chunk_Generation.generated_chunks--;
            yield return new WaitForEndOfFrame();
        }
        
        destroy_chunk(dir);
        chunk_Generation.generated_chunks++;
        generating_chunk = false;
    }
    public Vector2 get_min_max() 
    {
        return new Vector2(min, max);
    }
}
