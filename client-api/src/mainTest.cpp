#include "unityapi.h"
#include <unistd.h>
#include <math.h>

#include "world.h"
#include "config.h"

double dt = DT;

int boucle(){



    return EXIT_SUCCESS;
}

int main(int argc, char const *argv[])
{
	printf("SIMULATION STARTED\n");
	 // Objects creation
    World env = World(1,0);
    env.initialize();
	
	DisplayAPI display(argv[1], 13000);
	
	float psi=M_PI;  //vent


	Params params;
	params["name"] = "auv1";
	params["x"] = 10;
	params["y"] = 0;
	params["yaw"] = 0.0;

	display.sendParams(params);

	float t = 0;
	float tmax = 20;
	float dt = 0.1;
	float deltavmax=0;
	float theta=0;

	while(t < tmax){
		//Fait avancer la simulation
        env.clock();

		
		params["x"] = env.vec_sailboat[0].x;
		params["y"] = env.vec_sailboat[0].y;

		params["yaw"]= env.vec_sailboat[0].theta; 
		
		params["sailYaw"]=env.vec_sailboat[0].deltav;
		display.sendParams(params);

		t += dt;
		usleep(dt*1000000);
	}

	return 0;
}