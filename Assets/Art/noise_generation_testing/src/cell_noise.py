import numpy as np
import random
import math
from PIL import Image, ImageFilter
import noise


def generate_cell_noise(noisemap, points):
    for i in range(len(noisemap)):
        for j in range(len(noisemap[0])):
            closest = [distance(np.array([i, j]), points[0][0]), points[0][1]]
            for p in points:
                dist = distance(p[0], np.array([i, j]))
                if dist < closest[0]:
                    closest = [dist, p[1]]
            noisemap[i][j] = closest[1]
            if i < 50 or j < 50 or i > 950 or j > 950:
                noisemap[i][j] = 125
                
    return noisemap

def distance(point1, point2):
    point1 = point2-point1
    
    return math.sqrt(point1[0]**2 + point1[1]**2)

if __name__ == "__main__":
    noise_map = np.zeros((1000, 1000))
    points = []

    for i in range(20):
        points.append([np.array([random.randrange(0, 999), random.randrange(0, 999)]), 1/(i+1)*255])

    noise_map = generate_cell_noise(noise_map, points)

    img = Image.fromarray(noise_map.astype(np.uint8), mode="L")
    img = img.filter(ImageFilter.GaussianBlur(30))
    img.save("cell_noise.png")
