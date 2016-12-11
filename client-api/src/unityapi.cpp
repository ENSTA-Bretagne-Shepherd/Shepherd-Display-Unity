#include "unityapi.h"

#include <sstream>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <vector>
#include <map>

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

void DisplayAPI::setPosition(std::string auvname, double x, double y, double theta){

}

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