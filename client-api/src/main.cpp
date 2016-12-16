#include "unityapi.h"
#include <unistd.h>
#include <math.h>

int main(int argc, char const *argv[])
{

	DisplayAPI display(argv[1], 13000);

	psi=M_PI;  //vent


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
	float yaw=0;

	while(t < tmax){
		
		double xw_ap=2*cos(psi-theta)-1; //
		double yw_ap=2*sin(psi-theta);   //
		double psi_ap=atan2(yw_ap,xw_ap); //
		
		params["x"] = 10*cos(2*M_PI*t/10);
		params["y"] = 10*sin(2*M_PI*t/10);

		theta = 360*t/10;
		params["yaw"]= theta; //
		
		deltavmax=0.5*M_PI*(0.5*(cos(psi-theta)+1)); //
		
		if (sin(-psi_ap)>0) params["sailYaw"]=deltavmax;   else params["sailYaw"]=-deltavmax; //
		display.sendParams(params);

		t += dt;
		usleep(dt*1000000);
	}

	return 0;
}