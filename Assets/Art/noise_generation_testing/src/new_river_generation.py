import numpy as np
import random
import math
from PIL import Image
import noise
from datetime import datetime

def interpolate(direction):
    if direction == [0,0]:
        return [1,1]
    elif direction[0] > direction[1]:
        direction[1]/=direction[0]
        direction[0] = 1
    else:
        direction[0]/=direction[1]
        direction[1] = 1
    return direction

def line_test(noise_matrix, point0, point1, thickness=0):
    dx, dy = point1[1]- point0[1], point1[0] - point0[0]
    direction = [dy, dx]
    cur_pixel = point0
    
    while cur_pixel[0] != point1[0] and cur_pixel[1] != point1[1]:
        interp = interpolate(direction)
        cur_pixel+=[math.floor(interp[0]), math.floor(interp[1])]
        noise_matrix = draw_circle(noise_matrix, cur_pixel, thickness)
        
        if interp[0] == 1:
            direction[1]+=dx
        if interp[1] == 1:
            direction[0]+=dy
        
    return noise_matrix

def draw_circle(noise_matrix, point, radius, color):
    if point[0] < 0 or point[0] > 999 or point[1] < 0 or point[1] > 999:
        return noise_matrix
    
    if radius == 0:
        noise_matrix[point[0]][point[1]] = color
        return noise_matrix
    x, y, err = -radius, 0, 2-2*radius
    
    while True:
        if point[0]-x >= 0 and point[1]+y >= 0 and point[0]-x < 1000 and point[1]+y < 1000:
            noise_matrix[point[0]-x][point[1]+y] = color
        if point[0]-y >= 0 and point[1]-x >= 0 and point[0]-y < 1000 and point[1]-x < 1000:
            noise_matrix[point[0]-y][point[1]-x] = color
        if point[0]+x >= 0 and point[1]-y >= 0 and point[0]+x < 1000 and point[1]-y < 1000:
            noise_matrix[point[0]+x][point[1]-y] = color
        if point[0]+y >= 0 and point[1]+x >= 0 and point[0]+y < 1000 and point[1]+x < 1000:
            noise_matrix[point[0]+y][point[1]+x] = color
        r = err
        if r <=  y:
            y+=1
            err+=y*2+1
        if r > x or err > y:
            x+=1
            err+=x*2+1
        if x >0:
            break
    return draw_circle(noise_matrix, point, radius-1, color)

def draw_line(noise_matrix, point0, point1, thickness=0, start_color=1, end_color=1):
    dydx, cur_pixel = abs(point1 - point0), point0
    dydx[0]*=-1
    sxsy = [1 if point0[1] < point1[1] else -1, 1 if point0[0] < point1[0] else -1 ]
    err, e2 = dydx[0] + dydx[1], 0
    line_length = math.sqrt((point1[0]-point0[0])**2 + (point1[1] - point0[1])**2)
    color_change = end_color-start_color
    color_change = color_change/line_length
    while not (cur_pixel[0] == point1[0] and cur_pixel[1] == point1[1]):
        
        noise_matrix = draw_circle(noise_matrix, cur_pixel, thickness, start_color)
        start_color+=color_change
        if cur_pixel[0] == point1[0] and cur_pixel[1] == point1[1]:
            break
        if start_color > end_color:
            break
        e2 = 2*err
        if e2 >= dydx[0]:
            err+=dydx[0]
            cur_pixel[1]+=sxsy[1]
        if e2 <= dydx[1]:
            err+=dydx[1]
            cur_pixel[0]+=sxsy[0]
    
    return noise_matrix
    

if __name__ == "__main__":
    startTime =  datetime.now()

    world_map = np.zeros((1000, 1000))
    img = Image.fromarray((draw_line(world_map, np.array([100, 100]),np.array([900, 900]), 10, 1, 0)*255).astype(np.uint8), mode="L")
    img.save("line_image.png")

    print(datetime.now() - startTime)