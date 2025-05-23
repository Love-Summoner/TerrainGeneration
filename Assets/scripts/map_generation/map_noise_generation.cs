using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class map_noise_generation : MonoBehaviour
{
    public Texture2D map_filter, depth_filter;
    public float filter_weight, depth_weight, persistence, lacunarity, scale;
    public int seed = 0, octaves;

    private float max_height, min_height;
    void Start()
    {
        Random.InitState(seed);
        set_min_max();
    }

    public float get_noise_point(float x, float y)
    {
        float perlin_value = 0, amplitude = 1, frequency = 1, noise_height = 0, samplex, sampley;

        for(int i = 0; i < octaves; i++)
        {
            samplex = x / scale*frequency; sampley = y / scale*frequency;

            perlin_value = Mathf.PerlinNoise(samplex, sampley)*2 - 1;
            noise_height += perlin_value * amplitude;

            amplitude*=persistence; frequency*=lacunarity;
        }

        noise_height = Mathf.InverseLerp(min_height, max_height, noise_height);

        noise_height-=(map_filter.GetPixel((int)x, (int)y).grayscale * filter_weight);
        noise_height -= depth_filter.GetPixel((int)x, (int)y).grayscale * depth_weight;

        return noise_height;
    }
    private void set_min_max()
    {
        float amplitude = 1, frequency = 1;
        for (int i = 0; i < octaves; i++)
        {
            max_height += 1 * amplitude;
            min_height += -1 * amplitude;
            amplitude *= persistence; frequency *= lacunarity;
        }
        min_height -= 1;
    }
    public float get_min() 
    { 
        return min_height;
    }
    public float get_max()
    {
        return max_height;
    }
}
