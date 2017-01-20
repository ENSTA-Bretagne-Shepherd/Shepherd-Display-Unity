#include "unityapi.h"

#include <sstream>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <vector>
#include <map>

#ifndef WIN32

#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>

DisplayAPI::DisplayAPI(const char* peerHost, int peerPort)
{
	// Create
	sock = socket(AF_INET, SOCK_STREAM, 0);
    if (sock < 0) {
        perror("Cannot create a socket"); exit(1);
    }

    // Fill in the address of server
    struct sockaddr_in peeraddr;
    int peeraddr_len;
    memset(&peeraddr, 0, sizeof(peeraddr));
    

    // Resolve the server address (convert from symbolic name to IP number)
    struct hostent *host = gethostbyname(peerHost);
    if (host == NULL) {
        perror("Cannot define host address"); exit(1);
    }
    peeraddr.sin_family = AF_INET;
    peeraddr.sin_port = htons(peerPort);

    // Write resolved IP address of a server to the address structure
    memmove(&(peeraddr.sin_addr.s_addr), host->h_addr_list[0], 4);

    // Connect to a remote server
    int res = connect(sock, (struct sockaddr*) &peeraddr, sizeof(peeraddr));
    if (res < 0) {
        perror("Cannot connect"); exit(1);
    }
    printf("Connected. Reading a server message.\n");
}

DisplayAPI::~DisplayAPI(){
    close(sock);
}

void DisplayAPI::sendParams(Params params){
    std::string msg = Value(params).toJSONString().append("\n");
	
    int n = write(sock, msg.c_str(), msg.size());
    if(n < 0)
        printf("Error while writing to socket.\n");
}

void DisplayAPI::sendSailBoatState(std::string auvname, double x, double y, double theta){
    Params params;
    params["type"] = "sailboat";
    params["name"] = auvname;
    params["x"] = x;
    params["y"] = y;
    params["yaw"] = theta;
    sendParams(params);
}

void DisplayAPI::sendBuoyState(std::string auvname, double x, double y, double z){
    Params params;
    params["type"] = "buoy";
    params["name"] = auvname;
    params["x"] = x;
    params["y"] = y;
    params["z"] = z;
    sendParams(params);
}

void displaySegment(double x1, double y1, double x2, double y2){
    Params["type"] = "segment";
    params["x1"] = x1;
    params["y1"] = y1;
    params["x2"] = x2;
    params["y2"] = y2;
}

#else

DisplayAPI::DisplayAPI(const char* peerHost, int peerPort)
{
    WSADATA wsaData;
    struct addrinfo *result = NULL,
                    *ptr = NULL,
                    hints;

    // Initialize Winsock
    iResult = WSAStartup(MAKEWORD(2,2), &wsaData);
    if (iResult != 0) {
        printf("WSAStartup failed with error: %d\n", iResult);
        return 1;
    }

    ZeroMemory( &hints, sizeof(hints) );
    hints.ai_family = AF_UNSPEC;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    // Resolve the server address and port
    iResult = getaddrinfo(peerHost, peerPort, &hints, &result);
    if ( iResult != 0 ) {
        printf("getaddrinfo failed with error: %d\n", iResult);
        WSACleanup();
        return 1;
    }

    // Attempt to connect to an address until one succeeds
    for(ptr=result; ptr != NULL ;ptr=ptr->ai_next) {

        // Create a SOCKET for connecting to server
        sock = socket(ptr->ai_family, ptr->ai_socktype, 
            ptr->ai_protocol);
        if (sock == INVALID_SOCKET) {
            printf("socket failed with error: %ld\n", WSAGetLastError());
            WSACleanup();
            return 1;
        }

        // Connect to server.
        iResult = connect( sock, ptr->ai_addr, (int)ptr->ai_addrlen);
        if (iResult == SOCKET_ERROR) {
            closesocket(sock);
            sock = INVALID_SOCKET;
            continue;
        }
        break;
    }

    freeaddrinfo(result);

    if (sock == INVALID_SOCKET) {
        printf("Unable to connect to server!\n");
        WSACleanup();
        return 1;
    }
}

DisplayAPI::~DisplayAPI(){
    // cleanup
    closesocket(ConnectSocket);
    WSACleanup();
}

void DisplayAPI::sendParams(Params params){
    std::string msg = Value(params).toJSONString().append("\n");
    
    int iResult = send( sock, msg.c_str(), msg.size(), 0 );
    if (iResult == SOCKET_ERROR) {
        printf("send failed with error: %d\n", WSAGetLastError());
        closesocket(ConnectSocket);
        WSACleanup();
        return 1;
    }

}


#endif


// Utility JSON
std::string Value::toJSONString() const {
    std::ostringstream ss;
    switch (_type) {
    case vt_integer:
        ss<<_integer; break;
    case vt_decimal:
        ss<<_decimal; break;
    case vt_string:
        ss<<'"'<<_string<<'"'; break;
    case vt_array:
        ss << '[';
        for (std::vector<Value>::const_iterator it = _array.begin(); it != _array.end(); ++it) {
            if (it != _array.begin()) ss << ',';
            ss << it->toJSONString();
        }
        ss << ']';
        break;
    case vt_object:
        ss << '{' << _object->toJSON() << '}';
        break;
    case vt_none:
    default:
        break;
    }
    return ss.str();
}

std::string Params::toJSON() const {
    std::ostringstream ss;
    for(std::map<std::string, Value>::const_iterator it = _values.begin(); it != _values.end(); ++it)
        ss << (it==_values.begin()?"":", ") << "\"" << it->first << "\":" << it->second.toJSONString();
    return ss.str();
}

Value Params::pop(const std::string &key, const Value &value_not_found) {
    KeyValueMap::iterator it = _values.find(key);
    // Return empty value if not found
    if (it == _values.end())
        return value_not_found;
    // Otherwise, return corresponding value and remove it from map
    Value val = it->second;
    _values.erase(it);
    return val;
}