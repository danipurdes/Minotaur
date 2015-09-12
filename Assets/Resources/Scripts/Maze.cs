using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	readonly int MAZE_SIZE = 100;

	bool[,] tile;
	Vector2 origin = new Vector2(0,0);

	void Start () {
		tile = new bool[MAZE_SIZE, MAZE_SIZE];
		carveMaze ();

		Tile[,] tiles = new Tile[MAZE_SIZE, MAZE_SIZE];
		for (int i = 0; i < MAZE_SIZE; i++) {
			for (int j = 0; j < MAZE_SIZE; j++) {
				if (tile[i,j])
					tiles[i,j] = new Floor();
				else
					tiles[i,j] = new Wall();

				tiles [i,j].go.transform.position = new Vector2(origin.y + j, origin.x + i);
				tiles [i,j].go.transform.parent = this.transform;
			}
		}
	}

	void carveMaze () {
		int startX = MAZE_SIZE / 2;
		int startY = startX;
		MazeCarver[] carvers = new MazeCarver[60];
		carvers [0] = new MazeCarver (startX, startY, this);
		int nextIndex = 1;
		int activeIndex = 0;
		int carverCount = 1;
		System.Random rand = new System.Random();
		while (carverCount > 0) {
			MazeCarver carver = carvers[activeIndex];
			bool lived = carver.update();

			if (lived) { //avoids out-of-bounds error, probability at 30 = 0%) {
				if (rand.Next(1,100) < (carvers.Length - nextIndex)) {
					carvers[nextIndex++] = new MazeCarver(carver.x, carver.y, this);
					carverCount++;
				}
			}
			else {
				if (rand.Next(10) < 4) {
					for(int i = carver.x - 2; i <= carver.x + 2; i++) {
						for(int j = carver.y - 2; j <= carver.y + 2; j++) {
							if(i >= 0 && i < MAZE_SIZE && j >= 0 && j < MAZE_SIZE) {
								tile[i,j] = true;
							}
						}
					}
				}
				carverCount--;
				activeIndex++;
			}
		}

		int startDir = -1;

		switch (carvers[activeIndex-1].prevDir) {
		case 0:  
			startDir = 1; 
			break;
		case 1:  
			startDir = 0; 
			break;
		case 2:  
			startDir = 3; 
			break;
		case 3:  
			startDir = 2;
			break;
		}

		Coordinate startpt = new Coordinate(carvers[activeIndex-1].x, carvers[activeIndex-1].y, startDir);
		print("start: (" + startpt.x + ", " + startpt.y + ", " + startDir + ")");
	}

	void carveTile (int x, int y) {
		tile [x, y] = true;
	}

	List<Coordinate> getUncarvedNeighbors (int x, int y, int dir) {

		bool prevDirValid = false;
		List<Coordinate> neighbors = new List<Coordinate> ();
		if (x - 1 >= 1 && !tile [x - 1, y]) {
			neighbors.Add(new Coordinate(x - 1, y, 0));
			if(dir == 0) {
				prevDirValid = true;
			}
		}
		if (x + 1 < MAZE_SIZE - 1 && !tile [x + 1, y]) {
			neighbors.Add(new Coordinate(x + 1, y, 1));
			if(dir == 1) {
				prevDirValid = true;
			}
		}
		if (y - 1 >= 1 && !tile [x, y - 1]) {
			neighbors.Add(new Coordinate(x, y - 1, 2));
			if(dir == 2) {
				prevDirValid = true;
			}
		}
		if (y + 1 < MAZE_SIZE - 1 && !tile [x, y + 1]) {
			neighbors.Add(new Coordinate(x, y + 1, 3));
			if(dir == 3) {
				prevDirValid = true;
			}
		}

		int size = neighbors.Count;
		for (int i = 0; i < size && prevDirValid; i++) {
			switch(dir) {
			case 0:
				neighbors.Add(new Coordinate(x - 1, y, 0));
				break;
			case 1:
				neighbors.Add(new Coordinate(x + 1, y, 1));
				break;
			case 2:
				neighbors.Add(new Coordinate(x, y - 1, 2));
				break;
			case 3:
				neighbors.Add(new Coordinate(x, y + 1, 3));
				break;

			}
		}

		return neighbors;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private class MazeCarver {
		static System.Random rand = new System.Random ();
		public int x;
		public int y;
		int steps;
		Maze mazeRef;
		public int prevDir;

		public MazeCarver(int x, int y, Maze mazeRef) {
			this.x = x;
			this.y = y;
			this.steps = rand.Next(40, 60);
			this.mazeRef = mazeRef;
			this.prevDir = rand.Next(3);
		}

		/* Pick a direction, move */
		public bool update() {
			if (steps-- > 0) {
				// talk to maze to get a list of neighbors
				List<Coordinate> neighbors = this.mazeRef.getUncarvedNeighbors (this.x, this.y, this.prevDir);
				// select a direction at random
				if (neighbors.Count == 0) {
					return false;
				}
				print (neighbors.Count);
				Coordinate coord = neighbors [rand.Next (neighbors.Count)];
				print ("getcoord: "+coord.x + ", " + coord.y + ", "+coord.dir);
				// mark tile as taken
				mazeRef.carveTile (x, y);
				if(coord.x < 0 || coord.y < 0) {
					print ("after: "+coord.x + ", " + coord.y + ", "+coord.dir);
				}
				x = coord.x;
				y = coord.y;

				return true;
			}
			return false;
		}
	}	

	private class Coordinate {
		public readonly int x;
		public readonly int y;
		public readonly int dir; // 0 = left, 1 =right, 2 = down, 3 = up

		public Coordinate(int x, int y, int dir) {
			this.x = x;
			this.y = y;
			this.dir = dir;
		}
	}
}