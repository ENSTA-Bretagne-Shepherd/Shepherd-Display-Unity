#ifndef Buoy_H
#define Buoy_H

#include <math.h>

#pragma once

/**
 * Classe Buoy : la bouee
 */
class Buoy
{
    public:
        double x;//!State variables
        double y;//!State variables
        double z;//!State variables

        double m;//!Masse de Buoy
        double vol;//!Volume de Buoy

        double beta;//!Parameter for Lorentz
        double sigma;//!Parameter for Lorentz
        double rho;//!Parameter for Lorentz
        double k;//!Parameter for Lorentz

        int n;//!Id de Buoy

        double u;//!Commande
        double Xdot[3];//!Vecteur de vitesse de Buoy

        /**
         * Equations cinematiques pour les attracteurs de Lorentz
         */
        void lorenz(void);

        /**
         * Equations cinematiques simple pour une oscillation verticale
         */
        void sinLine(double simuTime);
        void pendulum(void);
        void stateEq(void);

        /**
         * Setter for the command ub
         * @param ub
         */
        void setCommand(double ub);

        /**
         * Getter for the Id of the Buoy
         * @return id of the Buoy
         */
        int getNumber(void);
        double* getPos(void);
        void clock(void);

        /**
         * Constructeur de la bouee
         * @param nb : id de la bouee
         * @param xb : position x en m
         * @param yb : position y en m
         * @param zb : profondeur z en m
         * @param ub : commande
         */
        Buoy(int nb, double xb, double yb, double zb, double ub);

    inline Buoy (){x=0;y=0;z=-10;Xdot[0]=0;Xdot[1]=0;Xdot[2]=0;}
        //~Buoy();
 };

#endif // Buoy_H
