import numpy as np
import random
import math
from PIL import Image
import noise

def draw_circle(image_size, radius, increment, origin):
    size = 100 * image_size/100
    seed = random.randrange(0, 1000)
    angle = 0
    solution = np.zeros((image_size, image_size))
    solution[origin[0]][origin[1]] = 1
    for i in range(radius):
        while(angle < 2*math.pi):
            x, y = round((radius-i) * math.cos(angle))+origin[0], round((radius-i) * math.sin(angle))+origin[1]
            if x < 1000 and y < 1000:
                pixel_value = noise.pnoise3(x = x/size, 
                                        y = y/size, 
                                        z = seed,
                                        octaves = 1)
                
                solution[x][y] = pixel_value
            angle+=(increment/4)
        
        increment = (math.pi * 2)/(2 * math.pi * (radius-i))
        angle = 0

    return solution

def generate_noise(image_size, x_size, y_size, origin):
    filter_weight = .5
    filter_matrix = Image.open("filters/lake1.png", mode="r").convert("L")
    filter_matrix = filter_matrix.resize((x_size+1, y_size+1))
    filter_matrix = np.array(filter_matrix)/255
    
    noise_matrix = np.zeros((image_size, image_size))
    seed = random.randrange(0, 1000)

    for i in range(x_size):
        for j in range(y_size):
            x_index,y_index = i+origin[0]-round(x_size/2), j+origin[1]-round(y_size/2)
            x_index = x_index if x_index < image_size else image_size-1
            y_index = y_index if y_index < image_size else image_size-1
            pixel_value = noise.pnoise3(x = i/50, 
                                        y = j/50, 
                                        z = seed,
                                        octaves = 5) - (filter_matrix[j][i]*filter_weight)
            noise_matrix[x_index][y_index] = pixel_value
    
    constrained_matrix = np.interp(noise_matrix, 
                                (np.amin(noise_matrix), 
                                np.amax(noise_matrix)),
                                (0, 1))
    for i in range(image_size):
        for j in range(image_size):
            constrained_matrix[i][j] = 1 if constrained_matrix[i][j] > .82 else 0
    return constrained_matrix
    
def gen_lakes(water_map):
    image_size = len(water_map)
    lake_count = int(image_size/100)
    lake_count = random.randrange(int(lake_count/2), int(lake_count))
    noise_matrix = np.zeros((image_size, image_size))

    source_points = []
    for i in range(lake_count):
         source_points.append([random.randrange(0, image_size), random.randrange(0, image_size)])
         width = random.randrange(50, 300)
         length = random.randrange(round(width/3), round(width*1.3))

         noise_matrix+=generate_noise(image_size, length, width, source_points[i])

    img = Image.fromarray((noise_matrix*255).astype(np.uint8), mode="L")
    img.save("images/lake_noise.png")
    return noise_matrix

water_map = np.zeros((1000,1000))
gen_lakes(water_map)