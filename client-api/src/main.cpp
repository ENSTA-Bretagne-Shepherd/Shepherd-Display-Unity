#include "unityapi.h"
#include <unistd.h>
#include <math.h>

int main(int argc, char const *argv[])
{

	DisplayAPI display(argv[1], 13000);

	float psi=M_PI;  //vent

	display.sendSailBoatState("auv1", 10, 0, 0.0);

	float t = 0;
	float tmax = 40;
	float dt = 0.1;
	float deltavmax=0;
	float theta=0;

	while(t < tmax){
		
		double xw_ap=2*cos(psi-theta)-1; //
		double yw_ap=2*sin(psi-theta);   //
		double psi_ap=atan2(yw_ap,xw_ap); //
		theta = 360*t/10;
		
		//deltavmax=0.5*M_PI*(0.5*(cos(psi-theta)+1));
		//if (sin(-psi_ap)>0) params["sailYaw"]=deltavmax;   else params["sailYaw"]=-deltavmax; //
		
		display.sendSailBoatState("auv1", 10*cos(2*M_PI*t/10), 10*sin(2*M_PI*t/10), theta);
		
		//display.sendParams(params);

		t += dt;
		usleep(dt*1000000);
	}

	return 0;
}