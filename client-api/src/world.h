//
// Created by tag on 16/12/16.
//

#pragma once

#include <vector>
#include "sailboat.h"
#include "buoy.h"

class World {
public:
    /**
     * Create objects
     * @param sailboat
     * @param buoy
     */
    World(int sailboat,int buoy);

    /**
     * Set objectives for the boat
     */
    void initialize();
    void clock();


    std::vector<Sailboat> vec_sailboat;
    std::vector<Buoy> vec_buoy;
    double simuTime = 0.0;

private:

};
