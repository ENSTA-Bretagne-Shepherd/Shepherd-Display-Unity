#include "unityapi.h"
#include <unistd.h>
#include <math.h>

int main(int argc, char const *argv[])
{
	DisplayAPI display(argv[1], 13000);

	Params params;
	params["name"] = "auv1";
	params["x"] = 10;
	params["y"] = 0;
	params["yaw"] = 0.0;

	display.sendParams(params);

	float t = 0;
	float tmax = 20;
	float dt = 0.1;

	while(t < tmax){
		params["x"] = 10*cos(2*M_PI*t/10);
		params["y"] = 10*sin(2*M_PI*t/10);
		params["yaw"] = 360*t/10;

		display.sendParams(params);

		t += dt;
		usleep(dt*1000000);
	}

	return 0;
}