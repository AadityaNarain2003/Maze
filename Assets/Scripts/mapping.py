import matplotlib.pyplot as plt
import numpy as np

# New data based on the provided CSV
new_data = [
    (0, 0.00, 1.50, 0.00), 
(2.34, 2.00, 1.50, 0.00 ),
(3.08, 2.00, 1.50, 2.00 ),
(4.08, -1.00, 1.50, 2.00 ),
(4.56, -1.00, 1.50, 4.00 ),
(5.22, 1.00, 1.50, 4.00 ),
(6.42, 1.00, 1.50, 1.00 ),
(6.98, -2.00, 1.50, 1.00 ),
(7.72, -2.00, 1.50, -3.00 ),
(10.66, 1.00, 1.50, -3.00 ),
]

# Extract x and z coordinates
x_coords_new = [entry[1] for entry in new_data]
z_coords_new = [entry[3] for entry in new_data]

# Define grid resolution based on the range of coordinates (e.g., from -5 to +5)
grid_size = 1  # Each unit corresponds to a grid cell

# Define grid bounds (you can adjust based on the actual maze size)
min_x, max_x = -5, 5
min_z, max_z = -5, 5

# Create an empty grid to track visits (density map)
grid_x = np.arange(min_x, max_x + grid_size, grid_size)
grid_z = np.arange(min_z, max_z + grid_size, grid_size)
heatmap = np.zeros((len(grid_x), len(grid_z)))

# Function to interpolate between two points
def interpolate_points(x1, z1, x2, z2, num_steps=10):
    # Interpolate points between (x1, z1) and (x2, z2)
    x_interp = np.linspace(x1, x2, num_steps)
    z_interp = np.linspace(z1, z2, num_steps)
    return x_interp, z_interp

# Iterate through consecutive pairs of points and interpolate
for i in range(len(x_coords_new) - 1):
    x1, z1 = x_coords_new[i], z_coords_new[i]
    x2, z2 = x_coords_new[i + 1], z_coords_new[i + 1]
    
    # Interpolate points between the two consecutive points
    x_interp, z_interp = interpolate_points(x1, z1, x2, z2, num_steps=10)
    
    # Add the interpolated points to the heatmap
    for x, z in zip(x_interp, z_interp):
        if min_x <= x <= max_x and min_z <= z <= max_z:
            x_idx = np.digitize(x, grid_x) - 1  # Index of x in grid
            z_idx = np.digitize(z, grid_z) - 1  # Index of z in grid
            heatmap[x_idx, z_idx] += 1  # Increment visit count for this grid cell

# Plot heatmap
plt.figure(figsize=(6, 6))
plt.imshow(heatmap.T, cmap='hot', origin='lower', interpolation='nearest', extent=[min_x, max_x, min_z, max_z])

# Add color bar and labels
plt.colorbar(label='Frequency of Visits')
plt.title('Heatmap of the Path Taken in the Maze')
plt.xlabel('X Coordinate')
plt.ylabel('Z Coordinate')

# Show the heatmap
plt.show()
