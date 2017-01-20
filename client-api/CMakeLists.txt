project(test)

set(CMAKE_CXX_FLAGS "-std=c++0x")
cmake_minimum_required(VERSION 2.8)

include_directories(src)
set(SOURCES src/unityapi.cpp src/mainTest.cpp src/unityapi.h src/sailboat.cpp src/world.cpp src/buoy.cpp)

add_executable(${PROJECT_NAME} ${SOURCES})
