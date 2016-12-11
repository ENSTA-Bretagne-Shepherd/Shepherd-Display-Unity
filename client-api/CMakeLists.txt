project(test)

set(CMAKE_CXX_FLAGS "-std=c++0x")
cmake_minimum_required(VERSION 2.8)

include_directories(src)
set(SOURCES src/unityapi.cpp src/main.cpp src/unityapi.h)

add_executable(${PROJECT_NAME} ${SOURCES})
