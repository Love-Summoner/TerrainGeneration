import numpy as np
import random
import math
from PIL import Image
import noise
    
def generate_curved_line(image_size, point1, point2, point3, point4, thickness = 0, thickness_decay=0, initial_depth=1, final_depth=1, noise_matrix = np.zeros((1000, 1000))):
    line_length = math.sqrt((point2[0]-point1[0])**2 + (point2[1]-point1[1])**2) + math.sqrt((point3[0]-point2[0])**2 + (point3[1]-point2[1])**2)
    thickness_decay = thickness*thickness_decay/round(line_length*5)
    for i in range(round(line_length*5)):
        t = i/(line_length*5)
        new_thickness = (1-t)*initial_depth + t*final_depth
        current_position = (1-t)**3 * point1 + 3*(1-t)**2 *t*point2 + 3*(1-t)*t**2*point3 + t**3*point4
        x_pos = math.ceil(current_position[0])
        y_pos = math.ceil(current_position[1])

        noise_matrix[x_pos][y_pos] = new_thickness
        for j in range(math.ceil(thickness)):
            greater_x_pos = x_pos+j if x_pos+j < image_size else x_pos
            greater_y_pos = y_pos+j if y_pos+j < image_size else y_pos
            noise_matrix[x_pos][greater_y_pos] = new_thickness
            noise_matrix[x_pos][y_pos-j] = new_thickness
            noise_matrix[greater_x_pos][y_pos] = new_thickness
            noise_matrix[x_pos-j][y_pos] = new_thickness
            noise_matrix[greater_x_pos][greater_y_pos] = new_thickness
            noise_matrix[x_pos-j][y_pos-j] = new_thickness
            noise_matrix[greater_x_pos][y_pos-j] = new_thickness
            noise_matrix[x_pos-j][greater_y_pos] = new_thickness
            
        thickness-=thickness_decay
    
    img = Image.fromarray((noise_matrix*255).astype(np.uint8), mode="L")
    img.save("curve_noise.png")
    
    return noise_matrix

def catmul_curve(image_size, point1, point2, point3, point4, thickness = 0, thickness_decay=0):
    line_length = math.sqrt((point2[0]-point1[0])**2 + (point2[1]-point1[1])**2) + math.sqrt((point3[0]-point2[0])**2 + (point3[1]-point2[1])**2)
    noise_matrix = np.zeros((image_size, image_size))
    thickness_decay = thickness*thickness_decay/round(line_length*5)
    for i in range(round(line_length*5)):
        t = i/(line_length*5)
        current_position = .5 * np.array((-t**3 + t**2 - t)*point1 + (3*t**3 - 5*t**2 + 2)*point2 + (-3*t**3 + 4 *t**2 + t)*point3 + (t**3 - t**2)*point4)
        noise_matrix[round(current_position[0])][round(current_position[1])] = 1
        for j in range(math.ceil(thickness)):
            noise_matrix[round(current_position[0])][round(current_position[1])+j] = 1
            noise_matrix[round(current_position[0])][round(current_position[1])-j] = 1
            noise_matrix[round(current_position[0]+j)][round(current_position[1])] = 1
            noise_matrix[round(current_position[0]-j)][round(current_position[1])] = 1
            
        thickness-=thickness_decay
    
    img = Image.fromarray((noise_matrix*255).astype(np.uint8), mode="L")
    img.save("curve_noise.png")
    
    return noise_matrix
    
def generate_line(image_size, start_point, end_point, line_thickness=0):
    noise_matrix = np.zeros((image_size+1, image_size+1))
    x_distance = end_point[0]-start_point[0]
    y_distance = end_point[1]-start_point[1]
    distance = math.sqrt((x_distance)**2 + (y_distance)**2)
    x,y = math.floor(start_point[0]), math.floor(start_point[1])
    angle = np.array((x_distance, y_distance))
    angle = angle/ np.linalg.norm(angle)
    
    angle = np.array((-angle[1], angle[0]))
    
    for i in range(int(distance)):
        noise_matrix[x][y] = 1
        x = round(x + x_distance/distance)
        y = round(y + y_distance/distance)
        if x < 0 or x > image_size:
            x = start_point[0]
        if y < 0 or y > image_size:
            y = start_point[1]
        for j in range(int(line_thickness/2)):
            if round(x+j*angle[0]) < image_size and y+j*angle[1] < image_size:
                noise_matrix[x+round(j*angle[0])][y+round(j*angle[1])] = 1
                noise_matrix[x-round(j*angle[0])][y-round(j*angle[1])] = 1
    
    return noise_matrix
    
def generate_river(image_size, start_point, end_point, initial_depth=1, final_depth=1, noise_matrix = np.zeros((1000, 1000))):
    depth_change = final_depth-initial_depth
    line_length = math.sqrt((end_point[0]-start_point[0])**2 + (end_point[1]-start_point[1])**2)
    number_of_points = math.ceil((line_length*random.uniform(.5, 1))/50)
    mid_points = []
    mid_points.append(start_point)
    
    change_in_t = 1/number_of_points
    unit_vector = (end_point-start_point)/np.linalg.norm(end_point-start_point)

    for i in range(number_of_points):
        t = i/number_of_points
        if t-change_in_t > 0 and t+change_in_t < 1:
            t+=random.uniform(-change_in_t, change_in_t)
        
        mid_points.append(np.array((1-t)*start_point + t*end_point+np.array([random.randrange(-int((image_size/10* abs(unit_vector[0]))), int(image_size/10*abs(unit_vector[1]))), random.randrange(-int((image_size/10)), int(image_size/10))])))
        
    
    mid_points.append(end_point)
    
    base_thickness = len(mid_points)
    depth_change/=len(mid_points)
    i = 0
    value_list = []
    while i+3 < len(mid_points):
        value_list.append([mid_points[i], initial_depth+depth_change*i])
        thickness_change = random.uniform(.5, 1)
        noise_matrix=generate_curved_line(image_size, mid_points[i], mid_points[i+1], mid_points[i+2],mid_points[i+3], base_thickness, 1-thickness_change, initial_depth+depth_change*i, initial_depth+depth_change*i*3, noise_matrix)
        i+=3
        base_thickness*=thickness_change
        
    
    img = Image.fromarray((noise_matrix*255).astype(np.uint8), mode="L")
    img.save("images/river_noise.png")
    
    return noise_matrix, value_list

if __name__ == "__main__":
    generate_river(1000, np.array([5, 500]), np.array([900, 500]))
    generate_curved_line(100, np.array([0, 50]), np.array([90, 50]), np.array([50, 50]), np.array([75, 25]), thickness=2)
