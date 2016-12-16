#include "sailboat.h"
#include "buoy.h"
#include <stdio.h>

extern double dt;

Buoy::Buoy(int nb, double xb, double yb, double zb, double ub)
{
    // id
    n = nb;

    // Position
    x = xb;
    y = yb;
    z = zb;

    // Caracteristiques physiques
    m = 1; //kg
    vol = 1; //m^-3

    // Command
    u = ub;

    // Lorentz variables
    sigma = 10.0;
    beta = 8.0/3.0;
    rho = 28;
    k = 0.0;
}

void Buoy::lorenz(void)
{
    Xdot[0] = sigma*(y-x)*dt;
    Xdot[1] = (x*(rho-z)-y)*dt;
    Xdot[2] = (k*(x*y-beta*z)+u);
}

void Buoy::sinLine(double simuTime)
{
    double depth = 40; // m
    double freq = 0.05; // Hz
    double speed = 10;  // m/s
    Xdot[0] = 0;      //X
    Xdot[1] = 0;      //Y
    Xdot[2] = speed*sin(2*M_PI*simuTime*freq); //Z
}

void Buoy::pendulum(void)
{
    Xdot[0] = y;
    Xdot[1] = -sin(x);
    Xdot[2] = u;
}

void Buoy::stateEq(void)
{
    Xdot[0] = sin(0.001*(y+0.9*z));
    Xdot[1] = -sin(0.001*(x+z));
    Xdot[2] = u;
    
}

void Buoy::setCommand(double ub)
{
    u = ub;
}

int Buoy::getNumber(void)
{
    return n;
}

double* Buoy::getPos(void)
{
    double* xd = new double[4];
    xd[0] = sqrt(pow(Xdot[0],2.0)+pow(Xdot[1],2.0)+pow(Xdot[2],2.0));
    xd[1] = x;
    xd[2] = y;
    xd[3] = z;
    return xd;
}

void Buoy::clock(void)  // The model is described in "L. Jaulin Modélisation et commande d'un bateau à voile, CIFA2004, Douz (Tunisie)"
{
    // On met à jour la position de la bouee
    // On travaille en dynamique donc pfd m*a = Somme(Forces)
    lorenz();
    x = x+dt*Xdot[0];
    y = y+dt*Xdot[1];
    z = z+dt*Xdot[2];

    printf("State %d : %f %f %f \n",n,x,y,z);
    fflush(stdout);
}

