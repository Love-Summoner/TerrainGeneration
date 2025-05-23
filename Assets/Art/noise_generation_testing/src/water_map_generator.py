import numpy as np
import random
import math
from PIL import Image, ImageFilter
import noise
from river_generation import generate_river
from water_source_generation import gen_lakes
from cell_noise import generate_cell_noise
from new_river_generation import draw_line


def layer_image(top_img, bottom_img):
    for i in range(len(top_img)):
        for j in range(len(top_img[0])):
            if top_img[i][j] > 0:
                bottom_img[i][j] = top_img[i][j]
    return bottom_img

def generate_water_map(image_size):
    water_map, depth_map = np.zeros((image_size, image_size)), np.zeros((image_size, image_size))
    water_map, source_points = gen_lakes(1000, water_map)
    
    points, river_points = [], []

    for i in range(len(source_points)):
        if len(source_points) < 2:
            break
        river_count = random.randrange(-1, 3)
        river_count = 0 if river_count <0 or  len(source_points)-i <= 1 else river_count
        for j in range(river_count):
            target = random.randrange(i, len(source_points))
            while target == i and len(source_points)-i > 1:
                target = random.randrange(i, len(source_points))
            water_map=draw_line(water_map, np.array(source_points[i]), np.array(source_points[target]), 10, water_map[source_points[i][0]][source_points[i][1]], water_map[source_points[target][0]][source_points[target][1]])

    points = []

    for p in source_points:
        points.append([p, (water_map[p[0]][p[1]])*255])

    print("cell noise generating")
    depth_map = generate_cell_noise(depth_map, points)    
    img = Image.fromarray((water_map*255).astype(np.uint8), mode="L")
    img.save("images/water_map.png")
    depth_img =Image.fromarray(depth_map.astype(np.uint8), mode="L")
    depth_img = depth_img.filter(ImageFilter.GaussianBlur(50))
    depth_img.save("images/cell_noise.png")
    return water_map
    
    
def generate_terrain_map(image_size):
    filter_weight = 1
    filter_matrix = Image.open("images/water_map.png", mode="r").convert("L")
    filter_matrix = filter_matrix.filter(ImageFilter.GaussianBlur(20))
    filter_matrix.save("images/blurred_water_map.png")
    filter_matrix = np.array(filter_matrix)/255
    seed = random.randrange(0, 1000)
    
    noise_matrix = np.zeros((image_size, image_size))
    for i in range(image_size):
        for j in range(image_size):
            pixel_value = noise.pnoise3(x = i/100, 
                                        y = j/100, 
                                        z = seed,
                                        octaves = 5,
                                        lacunarity = 2.0,
                                        persistence = .5) - (filter_matrix[j][i]*filter_weight)
            noise_matrix[i][j] = pixel_value
            
    constrained_matrix = np.interp(noise_matrix, 
                                (np.amin(noise_matrix), 
                                np.amax(noise_matrix)),
                                (0, 1))
    img = Image.fromarray((constrained_matrix*255).astype(np.uint8), mode="L")
    img.save("images/terrain_map.png")
    
if __name__ == "__main__":
    generate_water_map(1000)
    generate_terrain_map(1000)